﻿using System;
using System.Globalization;
using Xamarin.Forms;

namespace SuperPopupSample
{
    public sealed class SizeTypeConverter : TypeConverter
    {
        public override object ConvertFromInvariantString(string value)
        {
            if (value != null)
            {
                double x, y;
                string[] xy = value.Split(',');
                if (xy.Length == 2 && double.TryParse(xy[0], NumberStyles.Number, CultureInfo.InvariantCulture, out x) 
                        && double.TryParse(xy[1], NumberStyles.Number, CultureInfo.InvariantCulture, out y))
                    return new Size(x, y);
            }

            throw new InvalidOperationException(string.Format("Cannot convert \"{0}\" into {1}", value, typeof(Size)));
        }
    }
}
