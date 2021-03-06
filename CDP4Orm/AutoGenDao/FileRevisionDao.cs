﻿// --------------------------------------------------------------------------------------------------------------------
// <copyright file="FileRevisionDao.cs" company="RHEA System S.A.">
//   Copyright (c) 2016 RHEA System S.A.
// </copyright>
// <summary>
//   This is an auto-generated class. Any manual changes on this file will be overwritten!
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace CDP4Orm.Dao
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
 
    using CDP4Common.DTO;

    using Npgsql;
    using NpgsqlTypes;
 
    /// <summary>
    /// The FileRevision Data Access Object which acts as an ORM layer to the SQL database.
    /// </summary>
    public partial class FileRevisionDao : ThingDao, IFileRevisionDao
    {
        /// <summary>
        /// Read the data from the database.
        /// </summary>
        /// <param name="transaction">
        /// The current transaction to the database.
        /// </param>
        /// <param name="partition">
        /// The database partition (schema) where the requested resource is stored.
        /// </param>
        /// <param name="ids">
        /// Ids to retrieve from the database.
        /// </param>
        /// <param name="isCachedDtoReadEnabledAndInstant">
        /// The value indicating whether to get cached last state of Dto from revision history.
        /// </param>
        /// <returns>
        /// List of instances of <see cref="CDP4Common.DTO.FileRevision"/>.
        /// </returns>
        public virtual IEnumerable<CDP4Common.DTO.FileRevision> Read(NpgsqlTransaction transaction, string partition, IEnumerable<Guid> ids = null, bool isCachedDtoReadEnabledAndInstant = false)
        {
            using (var command = new NpgsqlCommand())
            {
                var sqlBuilder = new System.Text.StringBuilder();

                if (isCachedDtoReadEnabledAndInstant)
                {
                    sqlBuilder.AppendFormat("SELECT \"Jsonb\" FROM \"{0}\".\"FileRevision_Cache\"", partition);

                    if (ids != null && ids.Any())
                    {
                        sqlBuilder.Append(" WHERE \"Iid\" = ANY(:ids)");
                        command.Parameters.Add("ids", NpgsqlDbType.Array | NpgsqlDbType.Uuid).Value = ids;
                    }

                    sqlBuilder.Append(";");

                    command.Connection = transaction.Connection;
                    command.Transaction = transaction;
                    command.CommandText = sqlBuilder.ToString();

                    // log the sql command 
                    this.LogCommand(command);

                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var thing = this.MapJsonbToDto(reader);
                            if (thing != null)
                            {
                                yield return thing as FileRevision;
                            }
                        }
                    }
                }
                else
                {
                    sqlBuilder.AppendFormat("SELECT * FROM \"{0}\".\"FileRevision_View\"", partition);

                    if (ids != null && ids.Any()) 
                    {
                        sqlBuilder.Append(" WHERE \"Iid\" = ANY(:ids)");
                        command.Parameters.Add("ids", NpgsqlDbType.Array | NpgsqlDbType.Uuid).Value = ids;
                    }
                    
                    sqlBuilder.Append(";");
                    
                    command.Connection = transaction.Connection;
                    command.Transaction = transaction;
                    command.CommandText = sqlBuilder.ToString();
                    
                    // log the sql command 
                    this.LogCommand(command);
                    
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            yield return this.MapToDto(reader);
                        }
                    }
                }
            }
        }
 
        /// <summary>
        /// The mapping from a database record to data transfer object.
        /// </summary>
        /// <param name="reader">
        /// An instance of the SQL reader.
        /// </param>
        /// <returns>
        /// A deserialized instance of <see cref="CDP4Common.DTO.FileRevision"/>.
        /// </returns>
        public virtual CDP4Common.DTO.FileRevision MapToDto(NpgsqlDataReader reader)
        {
            string tempModifiedOn;
            string tempContentHash;
            string tempCreatedOn;
            string tempName;
            
            var valueDict = (Dictionary<string, string>)reader["ValueTypeSet"];
            var iid = Guid.Parse(reader["Iid"].ToString());
            var revisionNumber = int.Parse(valueDict["RevisionNumber"]);
            
            var dto = new CDP4Common.DTO.FileRevision(iid, revisionNumber);
            dto.ExcludedPerson.AddRange(Array.ConvertAll((string[])reader["ExcludedPerson"], Guid.Parse));
            dto.ExcludedDomain.AddRange(Array.ConvertAll((string[])reader["ExcludedDomain"], Guid.Parse));
            dto.Creator = Guid.Parse(reader["Creator"].ToString());
            dto.ContainingFolder = reader["ContainingFolder"] is DBNull ? (Guid?)null : Guid.Parse(reader["ContainingFolder"].ToString());
            dto.FileType.AddRange(Utils.ParseOrderedList<Guid>(reader["FileType"] as string[,]));
            
            if (valueDict.TryGetValue("ModifiedOn", out tempModifiedOn))
            {
                dto.ModifiedOn = Utils.ParseUtcDate(tempModifiedOn);
            }
            
            if (valueDict.TryGetValue("ContentHash", out tempContentHash))
            {
                dto.ContentHash = tempContentHash.UnEscape();
            }
            
            if (valueDict.TryGetValue("CreatedOn", out tempCreatedOn))
            {
                dto.CreatedOn = Utils.ParseUtcDate(tempCreatedOn);
            }
            
            if (valueDict.TryGetValue("Name", out tempName))
            {
                dto.Name = tempName.UnEscape();
            }
            
            return dto;
        }
 
        /// <summary>
        /// Insert a new database record from the supplied data transfer object.
        /// </summary>
        /// <param name="transaction">
        /// The current transaction to the database.
        /// </param>
        /// <param name="partition">
        /// The database partition (schema) where the requested resource will be stored.
        /// </param>
        /// <param name="fileRevision">
        /// The fileRevision DTO that is to be persisted.
        /// </param> 
        /// <param name="container">
        /// The container of the DTO to be persisted.
        /// </param>
        /// <returns>
        /// True if the concept was successfully persisted.
        /// </returns>
        public virtual bool Write(NpgsqlTransaction transaction, string partition, CDP4Common.DTO.FileRevision fileRevision, CDP4Common.DTO.Thing container = null)
        {
            bool isHandled;
            var valueTypeDictionaryAdditions = new Dictionary<string, string>();
            var beforeWrite = this.BeforeWrite(transaction, partition, fileRevision, container, out isHandled, valueTypeDictionaryAdditions);
            if (!isHandled)
            {
                beforeWrite = beforeWrite && base.Write(transaction, partition, fileRevision, container);

                var valueTypeDictionaryContents = new Dictionary<string, string>
                        {
                            { "ContentHash", fileRevision.ContentHash.Escape() },
                            { "CreatedOn", !this.IsDerived(fileRevision, "CreatedOn") ? fileRevision.CreatedOn.ToString(Utils.DateTimeUtcSerializationFormat) : string.Empty },
                            { "Name", !this.IsDerived(fileRevision, "Name") ? fileRevision.Name.Escape() : string.Empty },
                        }.Concat(valueTypeDictionaryAdditions).ToDictionary(kvp => kvp.Key, kvp => kvp.Value);
                
                using (var command = new NpgsqlCommand())
                {
                    var sqlBuilder = new System.Text.StringBuilder();
                
                    sqlBuilder.AppendFormat("INSERT INTO \"{0}\".\"FileRevision\"", partition);
                    sqlBuilder.AppendFormat(" (\"Iid\", \"ValueTypeDictionary\", \"Container\", \"Creator\", \"ContainingFolder\")");
                    sqlBuilder.AppendFormat(" VALUES (:iid, :valueTypeDictionary, :container, :creator, :containingFolder);");
                    command.Parameters.Add("iid", NpgsqlDbType.Uuid).Value = fileRevision.Iid;
                    command.Parameters.Add("valueTypeDictionary", NpgsqlDbType.Hstore).Value = valueTypeDictionaryContents;
                    command.Parameters.Add("container", NpgsqlDbType.Uuid).Value = container.Iid;
                    command.Parameters.Add("creator", NpgsqlDbType.Uuid).Value = !this.IsDerived(fileRevision, "Creator") ? fileRevision.Creator : Utils.NullableValue(null);
                    command.Parameters.Add("containingFolder", NpgsqlDbType.Uuid).Value = !this.IsDerived(fileRevision, "ContainingFolder") ? Utils.NullableValue(fileRevision.ContainingFolder) : Utils.NullableValue(null);
                
                    command.CommandText = sqlBuilder.ToString();
                    command.Connection = transaction.Connection;
                    command.Transaction = transaction;
                    this.ExecuteAndLogCommand(command);
                }
                
                fileRevision.FileType.ForEach(x => this.AddFileType(transaction, partition, fileRevision.Iid, x));
            }

            return this.AfterWrite(beforeWrite, transaction, partition, fileRevision, container);
        }
 
        /// <summary>
        /// Add the supplied value collection to the association link table indicated by the supplied property name
        /// </summary>
        /// <param name="transaction">
        /// The current transaction to the database.
        /// </param>
        /// <param name="partition">
        /// The database partition (schema) where the requested resource will be stored.
        /// </param>
        /// <param name="propertyName">
        /// The association property name that will be persisted.
        /// </param>
        /// <param name="iid">
        /// The <see cref="CDP4Common.DTO.FileRevision"/> id that will be the source for each link table record.
        /// </param> 
        /// <param name="value">
        /// A value for which a link table record wil be created.
        /// </param>
        /// <returns>
        /// True if the value link was successfully created.
        /// </returns>
        public override bool AddToCollectionProperty(NpgsqlTransaction transaction, string partition, string propertyName, Guid iid, object value)
        {
            var isCreated = base.AddToCollectionProperty(transaction, partition, propertyName, iid, value);
 
            switch (propertyName)
            {
                case "FileType":
                    {
                        isCreated = this.AddFileType(transaction, partition, iid, (CDP4Common.Types.OrderedItem)value);
                        break;
                    }
 
                default:
                {
                    break;
                }
            }

            return isCreated;
        }
 
        /// <summary>
        /// Insert a new association record in the link table.
        /// </summary>
        /// <param name="transaction">
        /// The current transaction to the database.
        /// </param>
        /// <param name="partition">
        /// The database partition (schema) where the requested resource will be stored.
        /// </param>
        /// <param name="iid">
        /// The <see cref="CDP4Common.DTO.FileRevision"/> id that will be the source for each link table record.
        /// </param> 
        /// <param name="fileType">
        /// The value for which a link table record wil be created.
        /// </param>
        /// <returns>
        /// True if the value link was successfully created.
        /// </returns>
        public bool AddFileType(NpgsqlTransaction transaction, string partition, Guid iid, CDP4Common.Types.OrderedItem fileType)
        {
            using (var command = new NpgsqlCommand())
            {
                var sqlBuilder = new System.Text.StringBuilder();
            
                sqlBuilder.AppendFormat("INSERT INTO \"{0}\".\"FileRevision_FileType\"", partition);
                sqlBuilder.AppendFormat(" (\"FileRevision\", \"FileType\", \"Sequence\")");
                sqlBuilder.Append(" VALUES (:fileRevision, :fileType, :sequence);");
                command.Parameters.Add("fileRevision", NpgsqlDbType.Uuid).Value = iid;
                command.Parameters.Add("fileType", NpgsqlDbType.Uuid).Value = fileType.V;
                command.Parameters.Add("sequence", NpgsqlDbType.Bigint).Value = fileType.K;
            
                command.CommandText = sqlBuilder.ToString();
                command.Connection = transaction.Connection;
                command.Transaction = transaction;
                return this.ExecuteAndLogCommand(command) > 0;
            }
        }
 
        /// <summary>
        /// Update a database record from the supplied data transfer object.
        /// </summary>
        /// <param name="transaction">
        /// The current transaction to the database.
        /// </param>
        /// <param name="partition">
        /// The database partition (schema) where the requested resource will be updated.
        /// </param>
        /// <param name="fileRevision">
        /// The fileRevision DTO that is to be updated.
        /// </param>
        /// <param name="container">
        /// The container of the DTO to be updated.
        /// </param>
        /// <returns>
        /// True if the concept was successfully updated.
        /// </returns>
        public virtual bool Update(NpgsqlTransaction transaction, string partition, CDP4Common.DTO.FileRevision fileRevision, CDP4Common.DTO.Thing container = null)
        {
            bool isHandled;
            var valueTypeDictionaryAdditions = new Dictionary<string, string>();
            var beforeUpdate = this.BeforeUpdate(transaction, partition, fileRevision, container, out isHandled, valueTypeDictionaryAdditions);
            if (!isHandled)
            {
                beforeUpdate = beforeUpdate && base.Update(transaction, partition, fileRevision, container);
                
                var valueTypeDictionaryContents = new Dictionary<string, string>
                        {
                            { "ContentHash", fileRevision.ContentHash.Escape() },
                            { "CreatedOn", !this.IsDerived(fileRevision, "CreatedOn") ? fileRevision.CreatedOn.ToString(Utils.DateTimeUtcSerializationFormat) : string.Empty },
                            { "Name", !this.IsDerived(fileRevision, "Name") ? fileRevision.Name.Escape() : string.Empty },
                        }.Concat(valueTypeDictionaryAdditions).ToDictionary(kvp => kvp.Key, kvp => kvp.Value);
                
                using (var command = new NpgsqlCommand())
                {
                    var sqlBuilder = new System.Text.StringBuilder();
                
                    sqlBuilder.AppendFormat("UPDATE \"{0}\".\"FileRevision\"", partition);
                    sqlBuilder.AppendFormat(" SET (\"Container\", \"Creator\", \"ContainingFolder\", \"ValueTypeDictionary\")");
                    sqlBuilder.AppendFormat(" = (:container, :creator, :containingFolder, \"ValueTypeDictionary\" || :valueTypeDictionary)");
                    sqlBuilder.AppendFormat(" WHERE \"Iid\" = :iid;");
                    command.Parameters.Add("iid", NpgsqlDbType.Uuid).Value = fileRevision.Iid;
                    command.Parameters.Add("container", NpgsqlDbType.Uuid).Value = container.Iid;
                    command.Parameters.Add("creator", NpgsqlDbType.Uuid).Value = !this.IsDerived(fileRevision, "Creator") ? fileRevision.Creator : Utils.NullableValue(null);
                    command.Parameters.Add("containingFolder", NpgsqlDbType.Uuid).Value = !this.IsDerived(fileRevision, "ContainingFolder") ? Utils.NullableValue(fileRevision.ContainingFolder) : Utils.NullableValue(null);
                    command.Parameters.Add("valueTypeDictionary", NpgsqlDbType.Hstore).Value = valueTypeDictionaryContents;
                
                    command.CommandText = sqlBuilder.ToString();
                    command.Connection = transaction.Connection;
                    command.Transaction = transaction;
                    this.ExecuteAndLogCommand(command);
                }
            }

            return this.AfterUpdate(beforeUpdate, transaction, partition, fileRevision, container);
        }
 
        /// <summary>
        /// Reorder the supplied value collection of the association link table indicated by the supplied property name
        /// </summary>
        /// <param name="transaction">
        /// The current transaction to the database.
        /// </param>
        /// <param name="partition">
        /// The database partition (schema) where the requested resource order will be updated.
        /// </param>
        /// <param name="propertyName">
        /// The association property name that will be reordered.
        /// </param>
        /// <param name="iid">
        /// The <see cref="CDP4Common.DTO.FileRevision"/> id that is the source for the reordered link table record.
        /// </param> 
        /// <param name="orderUpdate">
        /// The order update information containing the new order key.
        /// </param>
        /// <returns>
        /// True if the value link was successfully reordered.
        /// </returns>
        public override bool ReorderCollectionProperty(NpgsqlTransaction transaction, string partition, string propertyName, Guid iid, CDP4Common.Types.OrderedItem orderUpdate)
        {
            var isReordered = base.ReorderCollectionProperty(transaction, partition, propertyName, iid, orderUpdate);
 
            switch (propertyName)
            {
                case "FileType":
                    {
                        isReordered = this.ReorderFileType(transaction, partition, iid, orderUpdate);
                        break;
                    }
 
                default:
                {
                    break;
                }
            }

            return isReordered;
        }
 
        /// <summary>
        /// Reorder an item in an association link table.
        /// </summary>
        /// <param name="transaction">
        /// The current transaction to the database.
        /// </param>
        /// <param name="partition">
        /// The database partition (schema) where the requested resource will be reordered.
        /// </param>
        /// <param name="iid">
        /// The <see cref="CDP4Common.DTO.FileRevision"/> id that will be the source for reordered link table record.
        /// </param> 
        /// <param name="fileType">
        /// The value for which a link table record wil be reordered.
        /// </param>
        /// <returns>
        /// True if the value link was successfully reordered.
        /// </returns>
        public bool ReorderFileType(NpgsqlTransaction transaction, string partition, Guid iid, CDP4Common.Types.OrderedItem fileType)
        {
            using (var command = new NpgsqlCommand())
            {
                var sqlBuilder = new System.Text.StringBuilder();
            
                sqlBuilder.AppendFormat("UPDATE \"{0}\".\"FileRevision_FileType\"", partition);
                sqlBuilder.AppendFormat(" SET (\"Sequence\")");
                sqlBuilder.Append(" = (:reorderSequence);");
                sqlBuilder.Append(" WHERE \"FileRevision\" = :fileRevision");
                sqlBuilder.Append(" AND \"FileType\" = :fileType");
                sqlBuilder.Append(" AND \"Sequence\" = :sequence;");
                command.Parameters.Add("fileRevision", NpgsqlDbType.Uuid).Value = iid;
                command.Parameters.Add("fileType", NpgsqlDbType.Uuid).Value = fileType.V;
                command.Parameters.Add("sequence", NpgsqlDbType.Bigint).Value = fileType.K;
                command.Parameters.Add("reorderSequence", NpgsqlDbType.Bigint).Value = fileType.M;
            
                command.CommandText = sqlBuilder.ToString();
                command.Connection = transaction.Connection;
                command.Transaction = transaction;
                return this.ExecuteAndLogCommand(command) > 0;
            }
        }
 
        /// <summary>
        /// Delete a database record from the supplied data transfer object.
        /// </summary>
        /// <param name="transaction">
        /// The current transaction to the database.
        /// </param>
        /// <param name="partition">
        /// The database partition (schema) where the requested resource will be deleted.
        /// </param>
        /// <param name="iid">
        /// The <see cref="CDP4Common.DTO.FileRevision"/> id that is to be deleted.
        /// </param>
        /// <returns>
        /// True if the concept was successfully deleted.
        /// </returns>
        public override bool Delete(NpgsqlTransaction transaction, string partition, Guid iid)
        {
            bool isHandled;
            var beforeDelete = this.BeforeDelete(transaction, partition, iid, out isHandled);
            if (!isHandled)
            {
                beforeDelete = beforeDelete && base.Delete(transaction, partition, iid);
            }

            return this.AfterDelete(beforeDelete, transaction, partition, iid);
        }
 
        /// <summary>
        /// Delete the supplied value from the association link table indicated by the supplied property name.
        /// </summary>
        /// <param name="transaction">
        /// The current transaction to the database.
        /// </param>
        /// <param name="partition">
        /// The database partition (schema) where the requested resource will be removed.
        /// </param>
        /// <param name="propertyName">
        /// The association property name from where the value is to be removed.
        /// </param>
        /// <param name="iid">
        /// The <see cref="CDP4Common.DTO.FileRevision"/> id that is the source of each link table record.
        /// </param> 
        /// <param name="value">
        /// A value for which a link table record wil be removed.
        /// </param>
        /// <returns>
        /// True if the value link was successfully removed.
        /// </returns>
        public override bool DeleteFromCollectionProperty(NpgsqlTransaction transaction, string partition, string propertyName, Guid iid, object value)
        {
            var isDeleted = base.DeleteFromCollectionProperty(transaction, partition, propertyName, iid, value);
 
            switch (propertyName)
            {
                case "FileType":
                    {
                        isDeleted = this.DeleteFileType(transaction, partition, iid, (CDP4Common.Types.OrderedItem)value);
                        break;
                    }
 
                default:
                {
                    break;
                }
            }

            return isDeleted;
        }
 
        /// <summary>
        /// Delete an association record in the link table.
        /// </summary>
        /// <param name="transaction">
        /// The current transaction to the database.
        /// </param>
        /// <param name="partition">
        /// The database partition (schema) where the requested resource will be deleted.
        /// </param>
        /// <param name="iid">
        /// The <see cref="CDP4Common.DTO.FileRevision"/> id that is the source for each link table record.
        /// </param> 
        /// <param name="fileType">
        /// A value for which a link table record wil be deleted.
        /// </param>
        /// <returns>
        /// True if the value link was successfully removed.
        /// </returns>
        public bool DeleteFileType(NpgsqlTransaction transaction, string partition, Guid iid, CDP4Common.Types.OrderedItem fileType)
        {
            using (var command = new NpgsqlCommand())
            {
                var sqlBuilder = new System.Text.StringBuilder();
            
                sqlBuilder.AppendFormat("DELETE FROM \"{0}\".\"FileRevision_FileType\"", partition);
                sqlBuilder.Append(" WHERE \"FileRevision\" = :fileRevision");
                sqlBuilder.Append(" AND \"FileType\" = :fileType");
                sqlBuilder.Append(" AND \"Sequence\" = :sequence;");
                command.Parameters.Add("fileRevision", NpgsqlDbType.Uuid).Value = iid;
                command.Parameters.Add("fileType", NpgsqlDbType.Uuid).Value = fileType.V;
                command.Parameters.Add("sequence", NpgsqlDbType.Bigint).Value = fileType.K;
            
                command.CommandText = sqlBuilder.ToString();
                command.Connection = transaction.Connection;
                command.Transaction = transaction;
                return this.ExecuteAndLogCommand(command) > 0;
            }
        }
    }
}
