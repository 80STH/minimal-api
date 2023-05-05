using Microsoft.EntityFrameworkCore;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Data.Models
{
    public class Error
    {
        public int Id { get; set; }
        public string? module { get; set; }
        public int ecode { get; set; }
        public string? error { get; set; }
    }
    public class Files
    {
        public int Id { get; set; }
        public string? filename { get; set; }
        public bool result { get; set; }
        public List<Error>? errors { get; set; }
        public DateTime scantime { get; set; }
    }
    public class Datas
    {
        public int Id { get; set; }
        public Scan? scan { get; set; }
        public ICollection<Files>? files { get; set; } = new List<Files>();
    }
    public class Scan
    {
        public int Id { get; set; }
        public DateTime scanTime { get; set; }
        public string? db { get; set; }
        public string? server { get; set; }
        public int errorCount { get; set; }
    }

    public class ErrorDto
    {
        public string? module { get; set; }
        public int ecode { get; set; }
        public string? error { get; set; }
    }
}
