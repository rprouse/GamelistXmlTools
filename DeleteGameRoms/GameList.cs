using System.Xml.Serialization;

namespace GameListUtils;

#nullable disable warnings

[Serializable]
[XmlType(AnonymousType = true)]
[XmlRoot(ElementName = "gameList", Namespace = "", IsNullable = false)]
public partial class GameList
{
    [XmlElement("provider")]
    public Provider Provider { get; set; }

    [XmlElement("game")]
    public Game[] Games { get; set; }

    [XmlElement("folder")]
    public Folder[] Folders { get; set; }
}

/// <remarks/>
[Serializable]
[XmlType(AnonymousType = true)]
public partial class Provider
{
    public string System { get; set; }

    [XmlElement("software")]
    public string Software { get; set; }

    [XmlElement("database")]
    public string Database { get; set; }

    [XmlElement("web")]
    public string Web { get; set; }
}

/// <remarks/>
[Serializable]
[XmlType(AnonymousType = true)]
public partial class Game
{
    [XmlElement("path")]
    public string Path { get; set; }

    [XmlElement("name")]
    public string Name { get; set; }

    [XmlElement("desc")]
    public string Description { get; set; }

    [XmlElement("rating")]
    public decimal Rating { get; set; }

    [XmlIgnore()]
    [XmlElement("ratingSpecified")]
    public bool RatingSpecified { get; set; }

    [XmlElement("releasedate")]
    public string Releasedate { get; set; }

    [XmlElement("developer")]
    public string Developer { get; set; }

    [XmlElement("publisher")]
    public string Publisher { get; set; }

    [XmlElement("genre")]
    public string Genre { get; set; }

    [XmlElement("players")]
    public string Players { get; set; }

    [XmlElement("hash")]
    public string Hash { get; set; }

    [XmlElement("image")]
    public string Image { get; set; }

    [XmlElement("adult")]
    public bool Adult { get; set; }

    [XmlIgnore()]
    [XmlElement("adultSpecified")]
    public bool AdultSpecified { get; set; }

    [XmlElement("genreid")]
    public ushort Genreid { get; set; }

    [XmlIgnore()]
    [XmlElement("genreidSpecified")]
    public bool GenreidSpecified { get; set; }

    [XmlAttribute("id")]
    public uint Id { get; set; }

    [XmlAttribute("source")]
    public string Source { get; set; }
}

/// <remarks/>
[Serializable]
[XmlType(AnonymousType = true)]
public partial class Folder
{
    [XmlElement("path")]
    public string Path { get; set; }

    [XmlElement("name")]
    public string Name { get; set; }

    [XmlElement("desc")]
    public string Description { get; set; }

    [XmlElement("image")]
    public string Image { get; set; }

    [XmlAttribute("id")]
    public byte Id { get; set; }

    [XmlAttribute("source")]
    public string Source { get; set; }
}

#nullable restore warnings