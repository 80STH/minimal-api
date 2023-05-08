using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Reflection;


namespace Data.Models
{
    public class Error
    {
        public int Id { get; set; }
        public string? Module { get; set; }
        public int Ecode { get; set; }
        public string? error { get; set; }
    }
    public class Files
    {
        public int Id { get; set; }
        public string? Filename { get; set; }
        public bool Result { get; set; }
        public ICollection<Error>? errors { get ; set; }
        public DateTime Scantime { get; set; }
    }
    public class FilesErrorsDTO
    {
        public string? Filename { get; set; }
        public ICollection<Error>? Errors { get; set; }

        public FilesErrorsDTO() { }  
        public FilesErrorsDTO(Files files) => (Filename, Errors) = (files.Filename, files.errors);
    }
    public class Datas
    {
        public int Id { get; set; }
        public Scan? scan { get; set; }
        public ICollection<Files>? Files { get; set; }
    }
    public class Scan
    {
        public int Id { get; set; }
        public DateTime ScanTime { get; set; }
        public string? Db { get; set; }
        public string? Server { get; set; }
        public int ErrorCount { get; set; }
    }

    public class FilenameCheckDTO
    {
        public int Total { get; set; }
        public int Correct { get; set; }
        public int Errors_count { get; set; }
        public string[]? Filenames { get; set; }


    }
    public class ServiceInfoDto
    {
        private string? _appName;
        private string? _version;
        private DateTime? _dateUtc;
        static readonly Assembly thisAssem = typeof(ServiceInfoDto).Assembly;
        static readonly AssemblyName thisAssemName = thisAssem.GetName();

        static readonly Version? ver = thisAssemName.Version;

        public string? AppName { get { return thisAssemName.ToString(); } set { _appName = value; } }
        public string? Version { get { return ver?.ToString(); } set { _version = value; } }
        public DateTime DateUtc { get { return DateTime.Now; } set { _dateUtc = value; } }

    }

    //чтобы скрыть id

    public class DatasDTO
    {
        public Scan? Scan { get; set; }
        public ICollection<Files>? files { get; set; }
    }

    public class ScanDTO
    {
        public DateTime ScanTime { get; set; }
        public string? Db { get; set; }
        public string? Server { get; set; }
        public int ErrorCount { get; set; }
    }

    public class FilesDTO
    {
        public string? Filename { get; set; }
        public bool Result { get; set; }
        public ICollection<Error>? errors { get; set; }
        public DateTime Scantime { get; set; }
    }

    public class ErrorDto
    {
        public string? Module { get; set; }
        public int Ecode { get; set; }
        public string? Error { get; set; }
    }
}
