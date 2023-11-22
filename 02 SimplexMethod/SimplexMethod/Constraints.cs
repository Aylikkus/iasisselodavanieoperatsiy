﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace SimplexMethod
{
    public class Constraints : INotifyPropertyChanged
    {
        private LinearExpression[] expressions;

        public event PropertyChangedEventHandler PropertyChanged;

        private void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public int Rows { get; private set; }
        public int Columns { get; private set; }

        public LinearExpression this[int i]
        {
            get
            {
                return expressions[i];
            }
            set
            {
                expressions[i] = value;
                NotifyPropertyChanged();
            }
        }

        public double this[int i, int j]
        {
            get
            {
                return expressions[i][j];
            }
            set
            {
                expressions[i][j] = value;
                NotifyPropertyChanged();
            }
        }

        public Constraints(int rows, int columns)
        {
            Rows = rows;
            Columns = columns;

            expressions = new LinearExpression[rows];

            for (int i = 0; i < rows; i++)
            {
                expressions[i] = new LinearExpression(columns);
            }
        }

        private Constraints() { }
    }
}