﻿
using TrainSystem.Entities;

namespace TrainSystem.ViewModels.DataViews.Stations
{
    public class StationsIndexVM
    {
        public List<Station> Items { get; set; }  
        public int Id { get; set; }
        public string Location { get; set; }
    }
}
