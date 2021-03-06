﻿using System.Globalization;

namespace Serenity.Data
{
    public static class RowExtensions
    {
        /// <summary>
        ///   <see cref="Row"/> türevi nesnenin bir kopyasını çıkarır</summary>
        /// <remarks>
        ///   Bu fonksiyon bir extension metodu olduğundan direk olarak <c>row.Clone()</c> şeklinde
        ///   kullanılabilir. <see cref="Row.CloneRow()"/> metodu da bir row un kopyasını çıkarır, fakat
        ///   sonuç Row türündendir, tekrar asıl Row türevine typecast yapmak gerekir. Bu yardımcı
        ///   fonksiyonla explicit olarak typecast yapmaya gerek kalmaz.</remarks>
        /// <typeparam name="TRow">
        ///   Row'dan türemiş herhangi bir Row sınıfı</typeparam>
        /// <param name="row">
        ///   Kopyalanacak Row türevi nesne</param>
        /// <returns>
        ///   Fonksiyonun uygulandığı Row türevinin, bir kopyası (sadece alan değerleri)</returns>
        public static TRow Clone<TRow>(this TRow row) where TRow : Row
        {
            return (TRow)(row.CloneRow());
        }

        public static void ApplyDefaultValues(this Row row)
        {
            foreach (var field in row.GetFields())
            {
                var value = field.DefaultValue;
                if (value != null)
                    field.AsObject(row, field.ConvertValue(value, CultureInfo.InvariantCulture));
            }
        }

        public static TRowFields Init<TRowFields>(this TRowFields rowFields) 
            where TRowFields : RowFieldsBase
        {
            rowFields.Initialize();
            return rowFields;
        }
    }
}