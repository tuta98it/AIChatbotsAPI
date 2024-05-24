using MongoDB.Bson;
namespace Domain;

public abstract class BaseModel
{
    //[LoggingPrimaryKey]
    public long Id { get; set; }

    public string ObjId { get; set; } = ObjectId.GenerateNewId().ToString() ?? string.Empty;

    public DateTime? CreatedDate { get; set; }
    //[IgnoreLogging]

    public DateTime? UpdatedDate { get; set; }

    public bool? IsDeleted { get; set; }
    public void DeleteMe(DateTime now)
    {
        IsDeleted = true;
        UpdatedDate = now;
    }
}