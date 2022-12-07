namespace BPLISBN.Models
{
    public abstract class ClassBaseKey
    {
        public string? key { get; set; }
    }

    public abstract class ClassBaseDate
    {
        public string? type { get; set; }
        public DateTime value { get; set; }
    }

    public class Author : ClassBaseKey { }

    public class Language : ClassBaseKey { }

    public class Work : ClassBaseKey { }

    public class Type : ClassBaseKey { }

    public class Identifier
    {
        public string[]? goodreads { get; set; }
        public string[]? librarything { get; set; }
    }

    public class FirstSentence
    {
        public string? type { get; set; }
        public string? value { get; set; }
    }

    public class Created : ClassBaseDate { }

    public class LastModified : ClassBaseDate { }

    public class Book
    {
        public string? ISBN { get; set; }
        public DataRetrievalType? DataRetrievalType { get; set; }
        public string[]? publishers { get; set; }
        public int number_of_pages { get; set; }
        public string? subtitle { get; set; }
        public string[]? isbn_10 { get; set; }
        public int[]? covers { get; set; }
        public string? key { get; set; }
        public List<Author>? authors { get; set; }
        public string? ocaid { get; set; }
        public string[]? contributions { get; set; }
        public List<Language>? languages { get; set; }
        public string[]? classifications { get; set; }
        public string[]? source_records { get; set; }
        public string? title { get; set; }
        public List<Identifier>? identifiers { get; set; }
        public string[]? isbn_13 { get; set; }
        public string[]? local_id { get; set; }
        public string? publish_date { get; set; }
        public List<Work>? works { get; set; }
        public Type? type { get; set; }
        public FirstSentence? first_sentence { get; set; }
        public int latest_revision { get; set; }
        public int revision { get; set; }
        public Created? created { get; set; }
        public LastModified? last_modified { get; set; }
    }
}
