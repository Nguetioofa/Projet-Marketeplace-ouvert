﻿using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;


namespace ModelsLibrary.Models;

public partial class PhotoL
{
    public int Id { get; set; }

    public string? NomPhoto { get; set; }

    public string? DescriptionPhoto { get; set; }

    public string UrlP { get; set; } = null!;

    public int Taille { get; set; }

    public string Format { get; set; } = null!;

    public DateTime DatePublication { get; set; }

    public int? Messages { get; set; }

    public int? Jouet { get; set; }

    public int? Profil { get; set; }


}
