﻿// --------------------------------------------------------------------------------------------------------------------
// <copyright file="StakeHolderValueMapDao.cs" company="RHEA System S.A.">
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
    /// The StakeHolderValueMap Data Access Object which acts as an ORM layer to the SQL database.
    /// </summary>
    public partial class StakeHolderValueMapDao : DefinedThingDao, IStakeHolderValueMapDao
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
        /// List of instances of <see cref="CDP4Common.DTO.StakeHolderValueMap"/>.
        /// </returns>
        public virtual IEnumerable<CDP4Common.DTO.StakeHolderValueMap> Read(NpgsqlTransaction transaction, string partition, IEnumerable<Guid> ids = null, bool isCachedDtoReadEnabledAndInstant = false)
        {
            using (var command = new NpgsqlCommand())
            {
                var sqlBuilder = new System.Text.StringBuilder();

                if (isCachedDtoReadEnabledAndInstant)
                {
                    sqlBuilder.AppendFormat("SELECT \"Jsonb\" FROM \"{0}\".\"StakeHolderValueMap_Cache\"", partition);

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
                                yield return thing as StakeHolderValueMap;
                            }
                        }
                    }
                }
                else
                {
                    sqlBuilder.AppendFormat("SELECT * FROM \"{0}\".\"StakeHolderValueMap_View\"", partition);

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
        /// A deserialized instance of <see cref="CDP4Common.DTO.StakeHolderValueMap"/>.
        /// </returns>
        public virtual CDP4Common.DTO.StakeHolderValueMap MapToDto(NpgsqlDataReader reader)
        {
            string tempModifiedOn;
            string tempName;
            string tempShortName;
            
            var valueDict = (Dictionary<string, string>)reader["ValueTypeSet"];
            var iid = Guid.Parse(reader["Iid"].ToString());
            var revisionNumber = int.Parse(valueDict["RevisionNumber"]);
            
            var dto = new CDP4Common.DTO.StakeHolderValueMap(iid, revisionNumber);
            dto.ExcludedPerson.AddRange(Array.ConvertAll((string[])reader["ExcludedPerson"], Guid.Parse));
            dto.ExcludedDomain.AddRange(Array.ConvertAll((string[])reader["ExcludedDomain"], Guid.Parse));
            dto.Alias.AddRange(Array.ConvertAll((string[])reader["Alias"], Guid.Parse));
            dto.Definition.AddRange(Array.ConvertAll((string[])reader["Definition"], Guid.Parse));
            dto.HyperLink.AddRange(Array.ConvertAll((string[])reader["HyperLink"], Guid.Parse));
            dto.Goal.AddRange(Array.ConvertAll((string[])reader["Goal"], Guid.Parse));
            dto.ValueGroup.AddRange(Array.ConvertAll((string[])reader["ValueGroup"], Guid.Parse));
            dto.StakeholderValue.AddRange(Array.ConvertAll((string[])reader["StakeholderValue"], Guid.Parse));
            dto.Settings.AddRange(Array.ConvertAll((string[])reader["Settings"], Guid.Parse));
            dto.Requirement.AddRange(Array.ConvertAll((string[])reader["Requirement"], Guid.Parse));
            dto.Category.AddRange(Array.ConvertAll((string[])reader["Category"], Guid.Parse));
            
            if (valueDict.TryGetValue("ModifiedOn", out tempModifiedOn))
            {
                dto.ModifiedOn = Utils.ParseUtcDate(tempModifiedOn);
            }
            
            if (valueDict.TryGetValue("Name", out tempName))
            {
                dto.Name = tempName.UnEscape();
            }
            
            if (valueDict.TryGetValue("ShortName", out tempShortName))
            {
                dto.ShortName = tempShortName.UnEscape();
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
        /// <param name="stakeHolderValueMap">
        /// The stakeHolderValueMap DTO that is to be persisted.
        /// </param> 
        /// <param name="container">
        /// The container of the DTO to be persisted.
        /// </param>
        /// <returns>
        /// True if the concept was successfully persisted.
        /// </returns>
        public virtual bool Write(NpgsqlTransaction transaction, string partition, CDP4Common.DTO.StakeHolderValueMap stakeHolderValueMap, CDP4Common.DTO.Thing container = null)
        {
            bool isHandled;
            var valueTypeDictionaryAdditions = new Dictionary<string, string>();
            var beforeWrite = this.BeforeWrite(transaction, partition, stakeHolderValueMap, container, out isHandled, valueTypeDictionaryAdditions);
            if (!isHandled)
            {
                beforeWrite = beforeWrite && base.Write(transaction, partition, stakeHolderValueMap, container);

                using (var command = new NpgsqlCommand())
                {
                    var sqlBuilder = new System.Text.StringBuilder();
                
                    sqlBuilder.AppendFormat("INSERT INTO \"{0}\".\"StakeHolderValueMap\"", partition);
                    sqlBuilder.AppendFormat(" (\"Iid\", \"Container\")");
                    sqlBuilder.AppendFormat(" VALUES (:iid, :container);");
                    command.Parameters.Add("iid", NpgsqlDbType.Uuid).Value = stakeHolderValueMap.Iid;
                    command.Parameters.Add("container", NpgsqlDbType.Uuid).Value = container.Iid;
                
                    command.CommandText = sqlBuilder.ToString();
                    command.Connection = transaction.Connection;
                    command.Transaction = transaction;
                    this.ExecuteAndLogCommand(command);
                }
                
                stakeHolderValueMap.Goal.ForEach(x => this.AddGoal(transaction, partition, stakeHolderValueMap.Iid, x));
                stakeHolderValueMap.ValueGroup.ForEach(x => this.AddValueGroup(transaction, partition, stakeHolderValueMap.Iid, x));
                stakeHolderValueMap.StakeholderValue.ForEach(x => this.AddStakeholderValue(transaction, partition, stakeHolderValueMap.Iid, x));
                stakeHolderValueMap.Requirement.ForEach(x => this.AddRequirement(transaction, partition, stakeHolderValueMap.Iid, x));
                stakeHolderValueMap.Category.ForEach(x => this.AddCategory(transaction, partition, stakeHolderValueMap.Iid, x));
            }

            return this.AfterWrite(beforeWrite, transaction, partition, stakeHolderValueMap, container);
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
        /// The <see cref="CDP4Common.DTO.StakeHolderValueMap"/> id that will be the source for each link table record.
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
                case "Goal":
                    {
                        isCreated = this.AddGoal(transaction, partition, iid, (Guid)value);
                        break;
                    }
 
                case "ValueGroup":
                    {
                        isCreated = this.AddValueGroup(transaction, partition, iid, (Guid)value);
                        break;
                    }
 
                case "StakeholderValue":
                    {
                        isCreated = this.AddStakeholderValue(transaction, partition, iid, (Guid)value);
                        break;
                    }
 
                case "Requirement":
                    {
                        isCreated = this.AddRequirement(transaction, partition, iid, (Guid)value);
                        break;
                    }
 
                case "Category":
                    {
                        isCreated = this.AddCategory(transaction, partition, iid, (Guid)value);
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
        /// The <see cref="CDP4Common.DTO.StakeHolderValueMap"/> id that will be the source for each link table record.
        /// </param> 
        /// <param name="goal">
        /// The value for which a link table record wil be created.
        /// </param>
        /// <returns>
        /// True if the value link was successfully created.
        /// </returns>
        public bool AddGoal(NpgsqlTransaction transaction, string partition, Guid iid, Guid goal)
        {
            using (var command = new NpgsqlCommand())
            {
                var sqlBuilder = new System.Text.StringBuilder();
            
                sqlBuilder.AppendFormat("INSERT INTO \"{0}\".\"StakeHolderValueMap_Goal\"", partition);
                sqlBuilder.AppendFormat(" (\"StakeHolderValueMap\", \"Goal\")");
                sqlBuilder.Append(" VALUES (:stakeHolderValueMap, :goal);");
                command.Parameters.Add("stakeHolderValueMap", NpgsqlDbType.Uuid).Value = iid;
                command.Parameters.Add("goal", NpgsqlDbType.Uuid).Value = goal;
            
                command.CommandText = sqlBuilder.ToString();
                command.Connection = transaction.Connection;
                command.Transaction = transaction;
                return this.ExecuteAndLogCommand(command) > 0;
            }
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
        /// The <see cref="CDP4Common.DTO.StakeHolderValueMap"/> id that will be the source for each link table record.
        /// </param> 
        /// <param name="valueGroup">
        /// The value for which a link table record wil be created.
        /// </param>
        /// <returns>
        /// True if the value link was successfully created.
        /// </returns>
        public bool AddValueGroup(NpgsqlTransaction transaction, string partition, Guid iid, Guid valueGroup)
        {
            using (var command = new NpgsqlCommand())
            {
                var sqlBuilder = new System.Text.StringBuilder();
            
                sqlBuilder.AppendFormat("INSERT INTO \"{0}\".\"StakeHolderValueMap_ValueGroup\"", partition);
                sqlBuilder.AppendFormat(" (\"StakeHolderValueMap\", \"ValueGroup\")");
                sqlBuilder.Append(" VALUES (:stakeHolderValueMap, :valueGroup);");
                command.Parameters.Add("stakeHolderValueMap", NpgsqlDbType.Uuid).Value = iid;
                command.Parameters.Add("valueGroup", NpgsqlDbType.Uuid).Value = valueGroup;
            
                command.CommandText = sqlBuilder.ToString();
                command.Connection = transaction.Connection;
                command.Transaction = transaction;
                return this.ExecuteAndLogCommand(command) > 0;
            }
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
        /// The <see cref="CDP4Common.DTO.StakeHolderValueMap"/> id that will be the source for each link table record.
        /// </param> 
        /// <param name="stakeholderValue">
        /// The value for which a link table record wil be created.
        /// </param>
        /// <returns>
        /// True if the value link was successfully created.
        /// </returns>
        public bool AddStakeholderValue(NpgsqlTransaction transaction, string partition, Guid iid, Guid stakeholderValue)
        {
            using (var command = new NpgsqlCommand())
            {
                var sqlBuilder = new System.Text.StringBuilder();
            
                sqlBuilder.AppendFormat("INSERT INTO \"{0}\".\"StakeHolderValueMap_StakeholderValue\"", partition);
                sqlBuilder.AppendFormat(" (\"StakeHolderValueMap\", \"StakeholderValue\")");
                sqlBuilder.Append(" VALUES (:stakeHolderValueMap, :stakeholderValue);");
                command.Parameters.Add("stakeHolderValueMap", NpgsqlDbType.Uuid).Value = iid;
                command.Parameters.Add("stakeholderValue", NpgsqlDbType.Uuid).Value = stakeholderValue;
            
                command.CommandText = sqlBuilder.ToString();
                command.Connection = transaction.Connection;
                command.Transaction = transaction;
                return this.ExecuteAndLogCommand(command) > 0;
            }
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
        /// The <see cref="CDP4Common.DTO.StakeHolderValueMap"/> id that will be the source for each link table record.
        /// </param> 
        /// <param name="requirement">
        /// The value for which a link table record wil be created.
        /// </param>
        /// <returns>
        /// True if the value link was successfully created.
        /// </returns>
        public bool AddRequirement(NpgsqlTransaction transaction, string partition, Guid iid, Guid requirement)
        {
            using (var command = new NpgsqlCommand())
            {
                var sqlBuilder = new System.Text.StringBuilder();
            
                sqlBuilder.AppendFormat("INSERT INTO \"{0}\".\"StakeHolderValueMap_Requirement\"", partition);
                sqlBuilder.AppendFormat(" (\"StakeHolderValueMap\", \"Requirement\")");
                sqlBuilder.Append(" VALUES (:stakeHolderValueMap, :requirement);");
                command.Parameters.Add("stakeHolderValueMap", NpgsqlDbType.Uuid).Value = iid;
                command.Parameters.Add("requirement", NpgsqlDbType.Uuid).Value = requirement;
            
                command.CommandText = sqlBuilder.ToString();
                command.Connection = transaction.Connection;
                command.Transaction = transaction;
                return this.ExecuteAndLogCommand(command) > 0;
            }
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
        /// The <see cref="CDP4Common.DTO.StakeHolderValueMap"/> id that will be the source for each link table record.
        /// </param> 
        /// <param name="category">
        /// The value for which a link table record wil be created.
        /// </param>
        /// <returns>
        /// True if the value link was successfully created.
        /// </returns>
        public bool AddCategory(NpgsqlTransaction transaction, string partition, Guid iid, Guid category)
        {
            using (var command = new NpgsqlCommand())
            {
                var sqlBuilder = new System.Text.StringBuilder();
            
                sqlBuilder.AppendFormat("INSERT INTO \"{0}\".\"StakeHolderValueMap_Category\"", partition);
                sqlBuilder.AppendFormat(" (\"StakeHolderValueMap\", \"Category\")");
                sqlBuilder.Append(" VALUES (:stakeHolderValueMap, :category);");
                command.Parameters.Add("stakeHolderValueMap", NpgsqlDbType.Uuid).Value = iid;
                command.Parameters.Add("category", NpgsqlDbType.Uuid).Value = category;
            
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
        /// <param name="stakeHolderValueMap">
        /// The stakeHolderValueMap DTO that is to be updated.
        /// </param>
        /// <param name="container">
        /// The container of the DTO to be updated.
        /// </param>
        /// <returns>
        /// True if the concept was successfully updated.
        /// </returns>
        public virtual bool Update(NpgsqlTransaction transaction, string partition, CDP4Common.DTO.StakeHolderValueMap stakeHolderValueMap, CDP4Common.DTO.Thing container = null)
        {
            bool isHandled;
            var valueTypeDictionaryAdditions = new Dictionary<string, string>();
            var beforeUpdate = this.BeforeUpdate(transaction, partition, stakeHolderValueMap, container, out isHandled, valueTypeDictionaryAdditions);
            if (!isHandled)
            {
                beforeUpdate = beforeUpdate && base.Update(transaction, partition, stakeHolderValueMap, container);
                
                using (var command = new NpgsqlCommand())
                {
                    var sqlBuilder = new System.Text.StringBuilder();
                
                    sqlBuilder.AppendFormat("UPDATE \"{0}\".\"StakeHolderValueMap\"", partition);
                    sqlBuilder.AppendFormat(" SET (\"Container\")");
                    sqlBuilder.AppendFormat(" = (:container)");
                    sqlBuilder.AppendFormat(" WHERE \"Iid\" = :iid;");
                    command.Parameters.Add("iid", NpgsqlDbType.Uuid).Value = stakeHolderValueMap.Iid;
                    command.Parameters.Add("container", NpgsqlDbType.Uuid).Value = container.Iid;
                
                    command.CommandText = sqlBuilder.ToString();
                    command.Connection = transaction.Connection;
                    command.Transaction = transaction;
                    this.ExecuteAndLogCommand(command);
                }
            }

            return this.AfterUpdate(beforeUpdate, transaction, partition, stakeHolderValueMap, container);
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
        /// The <see cref="CDP4Common.DTO.StakeHolderValueMap"/> id that is to be deleted.
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
        /// The <see cref="CDP4Common.DTO.StakeHolderValueMap"/> id that is the source of each link table record.
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
                case "Goal":
                    {
                        isDeleted = this.DeleteGoal(transaction, partition, iid, (Guid)value);
                        break;
                    }
 
                case "ValueGroup":
                    {
                        isDeleted = this.DeleteValueGroup(transaction, partition, iid, (Guid)value);
                        break;
                    }
 
                case "StakeholderValue":
                    {
                        isDeleted = this.DeleteStakeholderValue(transaction, partition, iid, (Guid)value);
                        break;
                    }
 
                case "Requirement":
                    {
                        isDeleted = this.DeleteRequirement(transaction, partition, iid, (Guid)value);
                        break;
                    }
 
                case "Category":
                    {
                        isDeleted = this.DeleteCategory(transaction, partition, iid, (Guid)value);
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
        /// The <see cref="CDP4Common.DTO.StakeHolderValueMap"/> id that is the source for each link table record.
        /// </param> 
        /// <param name="goal">
        /// A value for which a link table record wil be deleted.
        /// </param>
        /// <returns>
        /// True if the value link was successfully removed.
        /// </returns>
        public bool DeleteGoal(NpgsqlTransaction transaction, string partition, Guid iid, Guid goal)
        {
            using (var command = new NpgsqlCommand())
            {
                var sqlBuilder = new System.Text.StringBuilder();
            
                sqlBuilder.AppendFormat("DELETE FROM \"{0}\".\"StakeHolderValueMap_Goal\"", partition);
                sqlBuilder.Append(" WHERE \"StakeHolderValueMap\" = :stakeHolderValueMap");
                sqlBuilder.Append(" AND \"Goal\" = :goal;");
                command.Parameters.Add("stakeHolderValueMap", NpgsqlDbType.Uuid).Value = iid;
                command.Parameters.Add("goal", NpgsqlDbType.Uuid).Value = goal;
            
                command.CommandText = sqlBuilder.ToString();
                command.Connection = transaction.Connection;
                command.Transaction = transaction;
                return this.ExecuteAndLogCommand(command) > 0;
            }
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
        /// The <see cref="CDP4Common.DTO.StakeHolderValueMap"/> id that is the source for each link table record.
        /// </param> 
        /// <param name="valueGroup">
        /// A value for which a link table record wil be deleted.
        /// </param>
        /// <returns>
        /// True if the value link was successfully removed.
        /// </returns>
        public bool DeleteValueGroup(NpgsqlTransaction transaction, string partition, Guid iid, Guid valueGroup)
        {
            using (var command = new NpgsqlCommand())
            {
                var sqlBuilder = new System.Text.StringBuilder();
            
                sqlBuilder.AppendFormat("DELETE FROM \"{0}\".\"StakeHolderValueMap_ValueGroup\"", partition);
                sqlBuilder.Append(" WHERE \"StakeHolderValueMap\" = :stakeHolderValueMap");
                sqlBuilder.Append(" AND \"ValueGroup\" = :valueGroup;");
                command.Parameters.Add("stakeHolderValueMap", NpgsqlDbType.Uuid).Value = iid;
                command.Parameters.Add("valueGroup", NpgsqlDbType.Uuid).Value = valueGroup;
            
                command.CommandText = sqlBuilder.ToString();
                command.Connection = transaction.Connection;
                command.Transaction = transaction;
                return this.ExecuteAndLogCommand(command) > 0;
            }
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
        /// The <see cref="CDP4Common.DTO.StakeHolderValueMap"/> id that is the source for each link table record.
        /// </param> 
        /// <param name="stakeholderValue">
        /// A value for which a link table record wil be deleted.
        /// </param>
        /// <returns>
        /// True if the value link was successfully removed.
        /// </returns>
        public bool DeleteStakeholderValue(NpgsqlTransaction transaction, string partition, Guid iid, Guid stakeholderValue)
        {
            using (var command = new NpgsqlCommand())
            {
                var sqlBuilder = new System.Text.StringBuilder();
            
                sqlBuilder.AppendFormat("DELETE FROM \"{0}\".\"StakeHolderValueMap_StakeholderValue\"", partition);
                sqlBuilder.Append(" WHERE \"StakeHolderValueMap\" = :stakeHolderValueMap");
                sqlBuilder.Append(" AND \"StakeholderValue\" = :stakeholderValue;");
                command.Parameters.Add("stakeHolderValueMap", NpgsqlDbType.Uuid).Value = iid;
                command.Parameters.Add("stakeholderValue", NpgsqlDbType.Uuid).Value = stakeholderValue;
            
                command.CommandText = sqlBuilder.ToString();
                command.Connection = transaction.Connection;
                command.Transaction = transaction;
                return this.ExecuteAndLogCommand(command) > 0;
            }
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
        /// The <see cref="CDP4Common.DTO.StakeHolderValueMap"/> id that is the source for each link table record.
        /// </param> 
        /// <param name="requirement">
        /// A value for which a link table record wil be deleted.
        /// </param>
        /// <returns>
        /// True if the value link was successfully removed.
        /// </returns>
        public bool DeleteRequirement(NpgsqlTransaction transaction, string partition, Guid iid, Guid requirement)
        {
            using (var command = new NpgsqlCommand())
            {
                var sqlBuilder = new System.Text.StringBuilder();
            
                sqlBuilder.AppendFormat("DELETE FROM \"{0}\".\"StakeHolderValueMap_Requirement\"", partition);
                sqlBuilder.Append(" WHERE \"StakeHolderValueMap\" = :stakeHolderValueMap");
                sqlBuilder.Append(" AND \"Requirement\" = :requirement;");
                command.Parameters.Add("stakeHolderValueMap", NpgsqlDbType.Uuid).Value = iid;
                command.Parameters.Add("requirement", NpgsqlDbType.Uuid).Value = requirement;
            
                command.CommandText = sqlBuilder.ToString();
                command.Connection = transaction.Connection;
                command.Transaction = transaction;
                return this.ExecuteAndLogCommand(command) > 0;
            }
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
        /// The <see cref="CDP4Common.DTO.StakeHolderValueMap"/> id that is the source for each link table record.
        /// </param> 
        /// <param name="category">
        /// A value for which a link table record wil be deleted.
        /// </param>
        /// <returns>
        /// True if the value link was successfully removed.
        /// </returns>
        public bool DeleteCategory(NpgsqlTransaction transaction, string partition, Guid iid, Guid category)
        {
            using (var command = new NpgsqlCommand())
            {
                var sqlBuilder = new System.Text.StringBuilder();
            
                sqlBuilder.AppendFormat("DELETE FROM \"{0}\".\"StakeHolderValueMap_Category\"", partition);
                sqlBuilder.Append(" WHERE \"StakeHolderValueMap\" = :stakeHolderValueMap");
                sqlBuilder.Append(" AND \"Category\" = :category;");
                command.Parameters.Add("stakeHolderValueMap", NpgsqlDbType.Uuid).Value = iid;
                command.Parameters.Add("category", NpgsqlDbType.Uuid).Value = category;
            
                command.CommandText = sqlBuilder.ToString();
                command.Connection = transaction.Connection;
                command.Transaction = transaction;
                return this.ExecuteAndLogCommand(command) > 0;
            }
        }
    }
}
