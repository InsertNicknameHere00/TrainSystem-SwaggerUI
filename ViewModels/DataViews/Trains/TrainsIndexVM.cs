﻿using TrainSystem.Entities;

namespace TrainSystem.ViewModels.DataViews.Trains
{
    public class TrainsIndexVM
    {
        public List<Train> Items { get; set; }
        public int TrainNum { get; set; }
        public string TrainType { get; set; }
        public int Capacity { get; set; }
    }
}
