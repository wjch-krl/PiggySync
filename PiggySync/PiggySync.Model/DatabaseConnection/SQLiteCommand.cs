using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace PiggySync.Model.DatabaseConnection
{
    public class SQLiteCommand
    {
        internal static IntPtr NegativePointer = new IntPtr(-1);
        private readonly List<Binding> _bindings;
        private readonly SQLiteConnection _conn;

        internal SQLiteCommand(SQLiteConnection conn)
        {
            _conn = conn;
            _bindings = new List<Binding>();
            CommandText = "";
        }

        public string CommandText { get; set; }

        public int ExecuteNonQuery()
        {
            if (_conn.Trace)
            {
                Debug.WriteLine("Executing: " + this);
            }

            var r = SQLite3.Result.OK;
            var stmt = Prepare();
            r = SQLite3.Step(stmt);
            Finalize(stmt);
            if (r == SQLite3.Result.Done)
            {
                int rowsAffected = SQLite3.Changes(_conn.Handle);
                return rowsAffected;
            }
            if (r == SQLite3.Result.Error)
            {
                string msg = SQLite3.GetErrmsg(_conn.Handle);
                throw SQLiteException.New(r, msg);
            }
            if (r == SQLite3.Result.Constraint)
            {
                if (SQLite3.ExtendedErrCode(_conn.Handle) == SQLite3.ExtendedResult.ConstraintNotNull)
                {
                    throw NotNullConstraintViolationException.New(r, SQLite3.GetErrmsg(_conn.Handle));
                }
            }

            throw SQLiteException.New(r, r.ToString());
        }

        public IEnumerable<T> ExecuteDeferredQuery<T>()
        {
            return ExecuteDeferredQuery<T>(_conn.GetMapping(typeof (T)));
        }

        public List<T> ExecuteQuery<T>()
        {
            return ExecuteDeferredQuery<T>(_conn.GetMapping(typeof (T))).ToList();
        }

        public List<T> ExecuteQuery<T>(TableMapping map)
        {
            return ExecuteDeferredQuery<T>(map).ToList();
        }

        /// <summary>
        ///     Invoked every time an instance is loaded from the database.
        /// </summary>
        /// <param name='obj'>
        ///     The newly created object.
        /// </param>
        /// <remarks>
        ///     This can be overridden in combination with the <see cref="SQLiteConnection.NewCommand" />
        ///     method to hook into the life-cycle of objects.
        ///     Type safety is not possible because MonoTouch does not support virtual generic methods.
        /// </remarks>
        protected virtual void OnInstanceCreated(object obj)
        {
            // Can be overridden.
        }

        public IEnumerable<T> ExecuteDeferredQuery<T>(TableMapping map)
        {
            if (_conn.Trace)
            {
                Debug.WriteLine("Executing Query: " + this);
            }

            var stmt = Prepare();
            try
            {
                var cols = new TableMapping.Column[SQLite3.ColumnCount(stmt)];

                for (int i = 0; i < cols.Length; i++)
                {
                    var name = SQLite3.ColumnName16(stmt, i);
                    cols[i] = map.FindColumn(name);
                }

                while (SQLite3.Step(stmt) == SQLite3.Result.Row)
                {
                    var obj = Activator.CreateInstance(map.MappedType);
                    for (int i = 0; i < cols.Length; i++)
                    {
                        if (cols[i] == null)
                            continue;
                        var colType = SQLite3.ColumnType(stmt, i);
                        var val = ReadCol(stmt, i, colType, cols[i].ColumnType);
                        cols[i].SetValue(obj, val);
                    }
                    OnInstanceCreated(obj);
                    yield return (T) obj;
                }
            }
            finally
            {
                SQLite3.Finalize(stmt);
            }
        }

        public T ExecuteScalar<T>()
        {
            if (_conn.Trace)
            {
                Debug.WriteLine("Executing Query: " + this);
            }

            T val = default(T);

            var stmt = Prepare();

            try
            {
                var r = SQLite3.Step(stmt);
                if (r == SQLite3.Result.Row)
                {
                    var colType = SQLite3.ColumnType(stmt, 0);
                    val = (T) ReadCol(stmt, 0, colType, typeof (T));
                }
                else if (r == SQLite3.Result.Done)
                {
                }
                else
                {
                    throw SQLiteException.New(r, SQLite3.GetErrmsg(_conn.Handle));
                }
            }
            finally
            {
                Finalize(stmt);
            }

            return val;
        }

        public void Bind(string name, object val)
        {
            _bindings.Add(new Binding
            {
                Name = name,
                Value = val
            });
        }

        public void Bind(object val)
        {
            Bind(null, val);
        }

        public override string ToString()
        {
            var parts = new string[1 + _bindings.Count];
            parts[0] = CommandText;
            var i = 1;
            foreach (var b in _bindings)
            {
                parts[i] = string.Format("  {0}: {1}", i - 1, b.Value);
                i++;
            }
            return string.Join(Environment.NewLine, parts);
        }

        private IntPtr Prepare()
        {
            var stmt = SQLite3.Prepare2(_conn.Handle, CommandText);
            BindAll(stmt);
            return stmt;
        }

        private void Finalize(IntPtr stmt)
        {
            SQLite3.Finalize(stmt);
        }

        private void BindAll(IntPtr stmt)
        {
            int nextIdx = 1;
            foreach (var b in _bindings)
            {
                if (b.Name != null)
                {
                    b.Index = SQLite3.BindParameterIndex(stmt, b.Name);
                }
                else
                {
                    b.Index = nextIdx++;
                }

                BindParameter(stmt, b.Index, b.Value, _conn.StoreDateTimeAsTicks);
            }
        }

        internal static void BindParameter(IntPtr stmt, int index, object value, bool storeDateTimeAsTicks)
        {
            if (value == null)
            {
                SQLite3.BindNull(stmt, index);
            }
            else
            {
                if (value is Int32)
                {
                    SQLite3.BindInt(stmt, index, (int) value);
                }
                else if (value is String)
                {
                    SQLite3.BindText(stmt, index, (string) value, -1, NegativePointer);
                }
                else if (value is Byte || value is UInt16 || value is SByte || value is Int16)
                {
                    SQLite3.BindInt(stmt, index, Convert.ToInt32(value));
                }
                else if (value is Boolean)
                {
                    SQLite3.BindInt(stmt, index, (bool) value ? 1 : 0);
                }
                else if (value is UInt32 || value is Int64)
                {
                    SQLite3.BindInt64(stmt, index, Convert.ToInt64(value));
                }
                else if (value is Single || value is Double || value is Decimal)
                {
                    SQLite3.BindDouble(stmt, index, Convert.ToDouble(value));
                }
                else if (value is TimeSpan)
                {
                    SQLite3.BindInt64(stmt, index, ((TimeSpan) value).Ticks);
                }
                else if (value is DateTime)
                {
                    if (storeDateTimeAsTicks)
                    {
                        SQLite3.BindInt64(stmt, index, ((DateTime) value).Ticks);
                    }
                    else
                    {
                        SQLite3.BindText(stmt, index, ((DateTime) value).ToString("yyyy-MM-dd HH:mm:ss"), -1,
                            NegativePointer);
                    }
                }
                else if (value is DateTimeOffset)
                {
                    SQLite3.BindInt64(stmt, index, ((DateTimeOffset) value).UtcTicks);
#if !NETFX_CORE
                }
                else if (value.GetType().IsEnum)
                {
#else
				} else if (value.GetType().GetTypeInfo().IsEnum) {
#endif
                    SQLite3.BindInt(stmt, index, Convert.ToInt32(value));
                }
                else if (value is byte[])
                {
                    SQLite3.BindBlob(stmt, index, (byte[]) value, ((byte[]) value).Length, NegativePointer);
                }
                else if (value is Guid)
                {
                    SQLite3.BindText(stmt, index, ((Guid) value).ToString(), 72, NegativePointer);
                }
                else
                {
                    throw new NotSupportedException("Cannot store type: " + value.GetType());
                }
            }
        }

        private object ReadCol(IntPtr stmt, int index, SQLite3.ColType type, Type clrType)
        {
            if (type == SQLite3.ColType.Null)
            {
                return null;
            }
            if (clrType == typeof (String))
            {
                return SQLite3.ColumnString(stmt, index);
            }
            if (clrType == typeof (Int32))
            {
                return SQLite3.ColumnInt(stmt, index);
            }
            if (clrType == typeof (Boolean))
            {
                return SQLite3.ColumnInt(stmt, index) == 1;
            }
            if (clrType == typeof (double))
            {
                return SQLite3.ColumnDouble(stmt, index);
            }
            if (clrType == typeof (float))
            {
                return (float) SQLite3.ColumnDouble(stmt, index);
            }
            if (clrType == typeof (TimeSpan))
            {
                return new TimeSpan(SQLite3.ColumnInt64(stmt, index));
            }
            if (clrType == typeof (DateTime))
            {
                if (_conn.StoreDateTimeAsTicks)
                {
                    return new DateTime(SQLite3.ColumnInt64(stmt, index));
                }
                var text = SQLite3.ColumnString(stmt, index);
                return DateTime.Parse(text);
            }
            if (clrType == typeof (DateTimeOffset))
            {
                return new DateTimeOffset(SQLite3.ColumnInt64(stmt, index), TimeSpan.Zero);
#if !NETFX_CORE
            }
            if (clrType.IsEnum)
            {
#else
				} else if (clrType.GetTypeInfo().IsEnum) {
#endif
                return SQLite3.ColumnInt(stmt, index);
            }
            if (clrType == typeof (Int64))
            {
                return SQLite3.ColumnInt64(stmt, index);
            }
            if (clrType == typeof (UInt32))
            {
                return (uint) SQLite3.ColumnInt64(stmt, index);
            }
            if (clrType == typeof (decimal))
            {
                return (decimal) SQLite3.ColumnDouble(stmt, index);
            }
            if (clrType == typeof (Byte))
            {
                return (byte) SQLite3.ColumnInt(stmt, index);
            }
            if (clrType == typeof (UInt16))
            {
                return (ushort) SQLite3.ColumnInt(stmt, index);
            }
            if (clrType == typeof (Int16))
            {
                return (short) SQLite3.ColumnInt(stmt, index);
            }
            if (clrType == typeof (sbyte))
            {
                return (sbyte) SQLite3.ColumnInt(stmt, index);
            }
            if (clrType == typeof (byte[]))
            {
                return SQLite3.ColumnByteArray(stmt, index);
            }
            if (clrType == typeof (Guid))
            {
                var text = SQLite3.ColumnString(stmt, index);
                return new Guid(text);
            }
            throw new NotSupportedException("Don't know how to read " + clrType);
        }

        private class Binding
        {
            public string Name { get; set; }

            public object Value { get; set; }

            public int Index { get; set; }
        }
    }
}