namespace Common.Utilities
{
    public static class ChangeDetect
    {
        public const string DatimeFormat = "u";
        public static List<ChangeLogDto> GetChanges(object oldEntry, object newEntry, string updatedByUsername, string updatedByFullname)
        {
            List<ChangeLogDto> logs = new List<ChangeLogDto>();

            var oldType = oldEntry.GetType();
            var newType = newEntry.GetType();
            if (oldType != newType)
            {
                return logs; //Types don't match, cannot log changes
            }

            var oldProperties = oldType.GetProperties();
            var newProperties = newType.GetProperties();

            var dateChanged = DateTime.Now;
            var primaryKey = oldProperties.Where(x => Attribute.IsDefined(x, typeof(LoggingPrimaryKeyAttribute))).FirstOrDefault();
            var primaryKeyObj = (long)primaryKey.GetValue(oldEntry);
            var className = oldEntry.GetType().Name;

            foreach (var oldProperty in oldProperties)
            {
                var matchingProperty = newProperties.Where(x => !Attribute.IsDefined(x, typeof(IgnoreLoggingAttribute))
                                                                && x.Name == oldProperty.Name
                                                                && x.PropertyType == oldProperty.PropertyType)
                                                    .FirstOrDefault();
                if (matchingProperty == null)
                {
                    continue;
                }

                string oldValue = null;
                string newValue = null;

                var oldValueObj = oldProperty.GetValue(oldEntry);
                var newValueObj = matchingProperty.GetValue(newEntry);
                if (oldProperty.PropertyType.FullName == typeof(DateTime).FullName || oldProperty.PropertyType.FullName == typeof(DateTime?).FullName)
                {
                    oldValue = oldValueObj != null ? ((DateTime)oldValueObj).ToString(DatimeFormat) : null;
                    newValue = newValueObj != null ? ((DateTime)newValueObj).ToString(DatimeFormat) : null;
                }
                else
                {
                    oldValue = oldValueObj != null ? oldValueObj.ToString() : null;
                    newValue = newValueObj != null ? newValueObj.ToString() : null;
                }

                if (matchingProperty != null && oldValue != newValue)
                {
                    logs.Add(new ChangeLogDto()
                    {
                        PrimaryKey = primaryKeyObj,
                        DateChanged = dateChanged,
                        ClassName = className,
                        PropertyName = matchingProperty.Name,
                        OldValue = oldValue,
                        NewValue = newValue,
                        UpdatedByUsername = updatedByUsername,
                        UpdatedByFullname = updatedByFullname
                    });
                }
            }

            return logs;
        }

        public static List<ChangeLogDto> GetChanges(object oldEntry, object newEntry, UserDto? user, DateTime updateTime)
        {
            List<ChangeLogDto> logs = new List<ChangeLogDto>();

            var oldType = oldEntry.GetType();
            var newType = newEntry.GetType();
            if (oldType != newType)
            {
                return logs; //Types don't match, cannot log changes
            }

            var oldProperties = oldType.GetProperties();
            var newProperties = newType.GetProperties();

            var dateChanged = updateTime;
            var primaryKey = oldProperties.Where(x => Attribute.IsDefined(x, typeof(LoggingPrimaryKeyAttribute))).FirstOrDefault();
            var primaryKeyObj = (long)primaryKey.GetValue(oldEntry);
            var className = oldEntry.GetType().Name;

            foreach (var oldProperty in oldProperties)
            {
                var matchingProperty = newProperties.Where(x => !Attribute.IsDefined(x, typeof(IgnoreLoggingAttribute))
                                                                && x.Name == oldProperty.Name
                                                                && x.PropertyType == oldProperty.PropertyType)
                                                    .FirstOrDefault();
                if (matchingProperty == null)
                {
                    continue;
                }

                string oldValue = null;
                string newValue = null;

                var oldValueObj = oldProperty.GetValue(oldEntry);
                var newValueObj = matchingProperty.GetValue(newEntry);
                if (oldProperty.PropertyType.FullName == typeof(DateTime).FullName || oldProperty.PropertyType.FullName == typeof(DateTime?).FullName)
                {
                    oldValue = oldValueObj != null ? ((DateTime)oldValueObj).ToString(DatimeFormat) : null;
                    newValue = newValueObj != null ? ((DateTime)newValueObj).ToString(DatimeFormat) : null;
                }
                else
                {
                    oldValue = oldValueObj != null ? oldValueObj.ToString() : null;
                    newValue = newValueObj != null ? newValueObj.ToString() : null;
                }

                if (matchingProperty != null && oldValue != newValue)
                {
                    if (newValue == null) newValue = "";
                    if (oldValue == null) oldValue = "";
                    logs.Add(new ChangeLogDto()
                    {
                        PrimaryKey = primaryKeyObj,
                        DateChanged = dateChanged,
                        ClassName = className,
                        PropertyName = matchingProperty.Name,
                        OldValue = oldValue,
                        NewValue = newValue,
                        UpdatedByUsername = user?.Username ?? "",
                        UpdatedByFullname = user?.Name ?? ""
                    });
                }
            }

            return logs;
        }
    }

    public class ChangeLogDto
    {
        public string ClassName { get; set; }
        public string PropertyName { get; set; }
        public long PrimaryKey { get; set; }
        public string OldValue { get; set; }
        public string NewValue { get; set; }
        public DateTime DateChanged { get; set; }
        public string UpdatedByUsername { get; set; }
        public string UpdatedByFullname { get; set; }
        public string? ReferObject { get; set; }
    }

    public class LoggingPrimaryKeyAttribute : Attribute
    {
    }

    public class IgnoreLoggingAttribute : Attribute
    {

    }
}
