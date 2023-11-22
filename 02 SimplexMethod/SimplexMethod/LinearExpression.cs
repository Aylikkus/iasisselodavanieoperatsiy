﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace SimplexMethod
{
    public class LinearExpression : INotifyPropertyChanged
    {
        private double[] coefficients;
        private string sign;
        private double b;

        public event PropertyChangedEventHandler PropertyChanged;

        private void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public string Sign
        {
            get
            {
                return sign;
            }
            set
            {
                if (value != "=" && value != "<=" && value != ">=")
                {
                    sign = "=";
                    NotifyPropertyChanged();
                    return;
                }

                sign = value;
                NotifyPropertyChanged();
            }
        }

        public double B
        {
            get
            {
                return b;
            }
            set
            {
                b = value;
                NotifyPropertyChanged();
            }
        }

        public double this[int i]
        {
            get
            {
                return coefficients[i];
            }
            set
            {
                coefficients[i] = value;
                NotifyPropertyChanged();
            }
        }
        
        public LinearExpression(int columns)
        {
            sign = "=";
            coefficients = new double[columns];
        }

        private LinearExpression() { }
    }
}