using System;
using System.Collections.Generic;
using MiTutorBEN.Models;

namespace MiTutorBEN.DTOs.Requests
{
    public class TutoringSessionRequest
    {
        public string Place { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public string Description { get; set; }
        public double Price { get; set; }
        public List<int> Topics { get; set; } = new List<int>();
    }
}