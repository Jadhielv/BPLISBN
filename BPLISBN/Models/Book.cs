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

    public class Identifiers
    {
        public List<string> goodreads { get; set; }
        public List<string> wikidata { get; set; }
        public List<string> librarything { get; set; }
    }

    public class Notes
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
        public List<string> publishers { get; set; }
        public int number_of_pages { get; set; }
        public string? subtitle { get; set; }
        public List<string> isbn_10 { get; set; }
        public List<int> covers { get; set; }
        public List<string> lc_classifications { get; set; }
        public string? key { get; set; }
        public List<Author>? authors { get; set; }
        public string? ocaid { get; set; }
        public List<string> publish_places { get; set; }
        public List<string> contributions { get; set; }
        public List<string> subjects { get; set; }
        public List<Language>? languages { get; set; }
        public string pagination { get; set; }
        public List<string> classifications { get; set; }
        public List<string> source_records { get; set; }
        public string? title { get; set; }
        public List<string> dewey_decimal_class { get; set; }
        public Notes Notes { get; set; }
        public Identifiers identifiers { get; set; }
        public List<string> isbn_13 { get; set; }
        public List<string> local_id { get; set; }
        public string? publish_date { get; set; }
        public string? publish_country { get; set; }
        public List<Work>? works { get; set; }
        public Type? type { get; set; }
        public int latest_revision { get; set; }
        public int revision { get; set; }
        public Created? created { get; set; }
        public string edition_name { get; set; }
        public List<string> lccn { get; set; }
        public LastModified? last_modified { get; set; }
        public string? by_statement { get; set; }
    }
}
