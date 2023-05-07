using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Reflection;
using System.Runtime.Serialization;


namespace Data.Models
{
    public class Error
    {
        [IgnoreDataMember]
        public int Id { get; set; }
        public string? module { get; set; }
        public int ecode { get; set; }
        public string? error { get; set; }
    }
    public class Files
    {
        [IgnoreDataMember]
        public int Id { get; set; }
        public string? filename { get; set; }
        public bool result { get; set; }
        public ICollection<Error>? errors { get ; set; }
        public DateTime scantime { get; set; }
    }
    public class FilesErrorsDTO
    {
        public string? filename { get; set; }
        public ICollection<Error>? errors { get; set; }

        public FilesErrorsDTO() { }
        public FilesErrorsDTO(Files files) => (filename, errors) = (files.filename, files.errors);
    }
    public class Datas
    {
        [IgnoreDataMember]
        public int Id { get; set; }
        public Scan? scan { get; set; }
        public ICollection<Files>? files { get; set; }
    }
    public class Scan
    {
        [IgnoreDataMember]
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

    public class FilenameCheckDTO
    {
        public int total { get; set; }
        public int correct { get; set; }
        public int errors_count { get; set; }
        public string? filename { get; set; }
        public string[]? filenames { get; set; }

        public FilenameCheckDTO(Datas datas) => (total, correct, errors_count, filenames) = (
            datas.files.Count(f => f.filename.Contains("query_")),
            datas.files.Count(f => f.filename.Contains("query_") && f.result == true),
            datas.files.Count(f => f.filename.Contains("query_") && f.result == false),
            datas.files.Where(f => f.filename.Contains("query_") && f.result == false).Select(f => f.filename).ToArray()
            );
    }
    public class ServiceInfoDto
    {
        static Assembly thisAssem = typeof(ServiceInfoDto).Assembly;
        static AssemblyName thisAssemName = thisAssem.GetName();

        static Version? ver = thisAssemName.Version;

        public string? AppName { get { return thisAssemName.ToString(); } set { AppName = value; } }
        public string? Version { get { return ver.ToString(); } set { Version = value; } }
        public DateTime DateUtc { get { return DateTime.Now; } set { DateUtc = value; } }

    }
}
