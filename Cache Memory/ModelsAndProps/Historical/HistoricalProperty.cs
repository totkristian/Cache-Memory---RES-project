﻿using ModelsAndProps.ValueStructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelsAndProps.Historical
{
    public class HistoricalProperty
    {
        private Codes codes;
        private Value historicalValue;
        private DateTime time;

        public Codes Codes { get => codes; set => codes = value; }
        public Value HistoricalValue { get => historicalValue; set => historicalValue = value; }
        public DateTime Time { get => time; set => time = value; }


        public HistoricalProperty ()
        {

        }

        public HistoricalProperty(Codes codes, Value value)
        {
            this.codes = codes;
            this.historicalValue = value;
        }
    }
}
