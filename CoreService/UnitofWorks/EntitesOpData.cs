namespace DALCore.UnitofWorks
{

    public static class EntitesOpData
    {

        private static void TrySetProperty<T>(T entity, string propertyName, object value)
        {
            var prop = entity?.GetType().GetProperty(propertyName);
            if (prop != null && prop.CanWrite)
            {
                prop.SetValue(entity, value);
            }
        }

        public static object TryGetProperty<T>(this T entity, string propertyName)
        {
            var prop = entity?.GetType().GetProperty(propertyName);
            if (prop != null && prop.CanRead)
            {
                return prop.GetValue(entity);
            }
            return null;
        }
        public static void GetIDProperty<T>(this T entity, out Guid? Id)
        {
            Id = null;
            var prop = entity?.GetType().GetProperty("Id");
            if (prop != null && prop.CanRead)
            {
                Id = Guid.Parse(prop.GetValue(entity).ToString());
            }
        }
        public static bool GetIsDeleted<T>(this T entity)
        {
            var prop = entity?.GetType().GetProperty("IsDeleted");
            if (prop != null && prop.CanRead)
            {
                try
                {
                    return (bool)prop.GetValue(entity);
                }
                catch (Exception ex)
                {
                    return false;
                }
            }
            return false;
        }

        public static void SetIDProperty<T>(this T entity, Guid? id)
        {
            var prop = entity?.GetType().GetProperty("Id");
            if (prop != null && prop.CanWrite)
            {
                prop.SetValue(entity, id);
            }
        }

        public static void SetCreationData<T>(this T entity, string userId)
        {
            TrySetProperty(entity, "CreatorId", userId);
            TrySetProperty(entity, "CreationDate", DateTime.Now);
        }

        public static void SetUpdateData<T>(this T entity, string userId)
        {
            TrySetProperty(entity, "LastEditorId", userId);
            TrySetProperty(entity, "LastEditDate", DateTime.Now);
        }

        public static void SetDeleteData<T>(this T entity, string userId)
        {
            TrySetProperty(entity, "LastEditorId", userId);
            TrySetProperty(entity, "LastEditDate", DateTime.Now);
            TrySetProperty(entity, "IsDeleted", true);
        }

        public static void SetUpdatorAndReturnAfterDeleteDate<T>(this T entity, string userId)
        {
            TrySetProperty(entity, "LastEditorId", userId);
            TrySetProperty(entity, "LastEditDate", DateTime.Now);
            TrySetProperty(entity, "IsDeleted", false);
        }
    }
}
