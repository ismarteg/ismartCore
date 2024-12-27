namespace DALCore.UnitofWorks
{

    public static class SetEntitesOpData
    {
        public static void SetCreationData<T>(this T entity, string UserId)
        {
            // Get the current user ID
            var currentUserId = UserId; // You need to implement this method

            // Set CreatorId property if the entity has it
            var creatorIdProperty = entity.GetType().GetProperty("CreatorId");
            if (creatorIdProperty != null && creatorIdProperty.CanWrite)
            {
                creatorIdProperty.SetValue(entity, currentUserId);
            }

            // Set CreationDate property if the entity has it
            var creationDateProperty = entity.GetType().GetProperty("CreationDate");
            if (creationDateProperty != null && creationDateProperty.CanWrite)
            {
                creationDateProperty.SetValue(entity, DateTime.Now);
            }
        }
        public static void SetUpdateData<T>(this T entity, string UserId)
        {
            // Get the current user ID
            var currentUserId = UserId; // You need to implement this method

            // Set CreatorId property if the entity has it
            var updatatorIdProperty = entity.GetType().GetProperty("LastEditorId");
            if (updatatorIdProperty != null && updatatorIdProperty.CanWrite)
            {
                updatatorIdProperty.SetValue(entity, currentUserId);
            }

            // Set CreationDate property if the entity has it
            var updateDateProperty = entity.GetType().GetProperty("LastEditDate");
            if (updateDateProperty != null && updateDateProperty.CanWrite)
            {
                updateDateProperty.SetValue(entity, DateTime.Now);
            }
        }
        public static void SetDeleteData<T>(this T entity, string UserId)
        {
            // Get the current user ID
            var currentUserId = UserId; // You need to implement this method

            // Set CreatorId property if the entity has it
            var deletorIdProperty = entity.GetType().GetProperty("LastEditorId");
            if (deletorIdProperty != null && deletorIdProperty.CanWrite)
            {
                deletorIdProperty.SetValue(entity, currentUserId);
            }

            // Set CreationDate property if the entity has it
            var deleteDateProperty = entity.GetType().GetProperty("LastEditDate");
            if (deleteDateProperty != null && deleteDateProperty.CanWrite)
            {
                deleteDateProperty.SetValue(entity, DateTime.Now);
            }
            var isDeleteProperty = entity.GetType().GetProperty("IsDeleted");
            if (isDeleteProperty != null && isDeleteProperty.CanWrite)
            {
                isDeleteProperty.SetValue(entity, true);
            }
        }
        public static void SetUpdatorAndReturnAfterDeleteDate<T>(this T entity, string UserId)
        {
            // Get the current user ID
            var currentUserId = UserId; // You need to implement this method

            // Set CreatorId property if the entity has it
            var updatatorIdProperty = entity.GetType().GetProperty("LastEditorId");
            if (updatatorIdProperty != null && updatatorIdProperty.CanWrite)
            {
                updatatorIdProperty.SetValue(entity, currentUserId);
            }

            // Set CreationDate property if the entity has it
            var updateDateProperty = entity.GetType().GetProperty("LastEditDate");
            if (updateDateProperty != null && updateDateProperty.CanWrite)
            {
                updateDateProperty.SetValue(entity, DateTime.Now);
            }
            var isDeleteProperty = entity.GetType().GetProperty("IsDeleted");
            if (isDeleteProperty != null && isDeleteProperty.CanWrite)
            {
                isDeleteProperty.SetValue(entity, false);
            }
        }
    }
}
