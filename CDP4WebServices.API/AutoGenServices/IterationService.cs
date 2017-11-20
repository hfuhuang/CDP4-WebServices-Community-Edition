﻿// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IterationService.cs" company="RHEA System S.A.">
//   Copyright (c) 2016 RHEA System S.A.
// </copyright>
// <summary>
//   This is an auto-generated class. Any manual changes on this file will be overwritten!
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace CDP4WebServices.API.Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
	using System.Security;

    using CDP4Common.DTO;
    
    using CDP4Orm.Dao;
 
    using CDP4WebServices.API.Services.Authorization;
 
    using Npgsql;
 
    /// <summary>
    /// The Iteration Service which uses the ORM layer to interact with the data model.
    /// </summary>
    public sealed partial class IterationService : ServiceBase, IIterationService
    {
        /// <summary>
        /// Gets or sets the option service.
        /// </summary>
        public IOptionService OptionService { get; set; }
 
        /// <summary>
        /// Gets or sets the publication service.
        /// </summary>
        public IPublicationService PublicationService { get; set; }
 
        /// <summary>
        /// Gets or sets the possibleFiniteStateList service.
        /// </summary>
        public IPossibleFiniteStateListService PossibleFiniteStateListService { get; set; }
 
        /// <summary>
        /// Gets or sets the elementDefinition service.
        /// </summary>
        public IElementDefinitionService ElementDefinitionService { get; set; }
 
        /// <summary>
        /// Gets or sets the relationship service.
        /// </summary>
        public IRelationshipService RelationshipService { get; set; }
 
        /// <summary>
        /// Gets or sets the externalIdentifierMap service.
        /// </summary>
        public IExternalIdentifierMapService ExternalIdentifierMapService { get; set; }
 
        /// <summary>
        /// Gets or sets the requirementsSpecification service.
        /// </summary>
        public IRequirementsSpecificationService RequirementsSpecificationService { get; set; }
 
        /// <summary>
        /// Gets or sets the domainFileStore service.
        /// </summary>
        public IDomainFileStoreService DomainFileStoreService { get; set; }
 
        /// <summary>
        /// Gets or sets the actualFiniteStateList service.
        /// </summary>
        public IActualFiniteStateListService ActualFiniteStateListService { get; set; }
 
        /// <summary>
        /// Gets or sets the ruleVerificationList service.
        /// </summary>
        public IRuleVerificationListService RuleVerificationListService { get; set; }
 
        /// <summary>
        /// Gets or sets the stakeholder service.
        /// </summary>
        public IStakeholderService StakeholderService { get; set; }
 
        /// <summary>
        /// Gets or sets the goal service.
        /// </summary>
        public IGoalService GoalService { get; set; }
 
        /// <summary>
        /// Gets or sets the valueGroup service.
        /// </summary>
        public IValueGroupService ValueGroupService { get; set; }
 
        /// <summary>
        /// Gets or sets the stakeholderValue service.
        /// </summary>
        public IStakeholderValueService StakeholderValueService { get; set; }
 
        /// <summary>
        /// Gets or sets the stakeHolderValueMap service.
        /// </summary>
        public IStakeHolderValueMapService StakeHolderValueMapService { get; set; }
 
        /// <summary>
        /// Gets or sets the sharedStyle service.
        /// </summary>
        public ISharedStyleService SharedStyleService { get; set; }
 
        /// <summary>
        /// Gets or sets the diagramCanvas service.
        /// </summary>
        public IDiagramCanvasService DiagramCanvasService { get; set; }
 
        /// <summary>
        /// Gets or sets the iteration dao.
        /// </summary>
        public IIterationDao IterationDao { get; set; }

        /// <summary>
        /// Get the requested data from the ORM layer.
        /// </summary>
        /// <param name="transaction">
        /// The transaction object.
        /// </param>
        /// <param name="partition">
        /// The database partition (schema) where the requested resource is stored.
        /// </param>
        /// <param name="ids">
        /// Ids to retrieve from the database.
        /// </param>
        /// <param name="containerSecurityContext">
        /// The security context of the container instance.
        /// </param>
        /// <returns>
        /// List of instances of <see cref="Iteration"/>.
        /// </returns>
        public IEnumerable<Thing> Get(NpgsqlTransaction transaction, string partition, IEnumerable<Guid> ids, ISecurityContext containerSecurityContext)
        {
            return this.RequestUtils.QueryParameters.ExtentDeep
                       ? this.GetDeep(transaction, partition, ids, containerSecurityContext)
                       : this.GetShallow(transaction, partition, ids, containerSecurityContext);
        }

        /// <summary>
        /// Add the supplied value to the association link table indicated by the supplied property name.
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
        /// The <see cref="Iteration"/> id that will be the source for each link table record.
        /// </param>
        /// <param name="value">
        /// A value for which a link table record will be created.
        /// </param>
        /// <returns>
        /// True if the link was created.
        /// </returns>
        public bool AddToCollectionProperty(NpgsqlTransaction transaction, string partition, string propertyName, Guid iid, object value)
        {
            return this.IterationDao.AddToCollectionProperty(transaction, partition, propertyName, iid, value);
        }

        /// <summary>
        /// Remove the supplied value from the association property as indicated by the supplied property name.
        /// </summary>
        /// <param name="transaction">
        /// The current transaction to the database.
        /// </param>
        /// <param name="partition">
        /// The database partition (schema) from where the requested resource will be removed.
        /// </param>
        /// <param name="propertyName">
        /// The association property from where the supplied value will be removed.
        /// </param>
        /// <param name="iid">
        /// The <see cref="Iteration"/> id that is the source of the link table records.
        /// </param>
        /// <param name="value">
        /// A value for which the link table record will be removed.
        /// </param>
        /// <returns>
        /// True if the link was removed.
        /// </returns>
        public bool DeleteFromCollectionProperty(NpgsqlTransaction transaction, string partition, string propertyName, Guid iid, object value)
        {
            return this.IterationDao.DeleteFromCollectionProperty(transaction, partition, propertyName, iid, value);
        }

        /// <summary>
        /// Reorder the supplied value collection of the association link table indicated by the supplied property name.
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
        /// The <see cref="Iteration"/> id that is the source for the reordered link table record.
        /// </param>
        /// <param name="orderUpdate">
        /// The order update information containing the new order key.
        /// </param>
        /// <returns>
        /// True if the link was created.
        /// </returns>
        public bool ReorderCollectionProperty(NpgsqlTransaction transaction, string partition, string propertyName, Guid iid, CDP4Common.Types.OrderedItem orderUpdate)
        {
            return this.IterationDao.ReorderCollectionProperty(transaction, partition, propertyName, iid, orderUpdate);
        }

        /// <summary>
        /// Update the containment order as indicated by the supplied orderedItem.
        /// </summary>
        /// <param name="transaction">
        /// The current transaction to the database.
        /// </param>
        /// <param name="partition">
        /// The database partition (schema) where the requested resource order will be updated.
        /// </param>
        /// <param name="orderedItem">
        /// The order update information containing the new order key.
        /// </param>
        /// <returns>
        /// True if the contained item was successfully reordered.
        /// </returns>
        public bool ReorderContainment(NpgsqlTransaction transaction, string partition, CDP4Common.Types.OrderedItem orderedItem)
        {
            throw new NotSupportedException();
        }

        /// <summary>
        /// Delete the supplied DTO instance.
        /// </summary>
        /// <param name="transaction">
        /// The transaction to the database.
        /// </param>
        /// <param name="partition">
        /// The database partition (schema) from where the requested resource will be removed.
        /// </param>
        /// <param name="thing">
        /// The <see cref="Iteration"/> to delete.
        /// </param>
        /// <param name="container">
        /// The container instance of the DTO to be removed.
        /// </param>
        /// <returns>
        /// True if the removal was successful.
        /// </returns>
        public bool DeleteConcept(NpgsqlTransaction transaction, string partition, Thing thing, Thing container = null)
        {
            if (!this.IsInstanceModifyAllowed(transaction, thing, partition, DeleteOperation))
            {
                throw new SecurityException("The person " + this.PermissionService.Credentials.Person.UserName + " does not have an appropriate delete permission for " + thing.GetType().Name + ".");
            }

            return this.IterationDao.Delete(transaction, partition, thing.Iid);
        }

        /// <summary>
        /// Update the supplied DTO instance.
        /// </summary>
        /// <param name="transaction">
        /// The transaction object.
        /// </param>
        /// <param name="partition">
        /// The database partition (schema) where the requested resource will be updated.
        /// </param>
        /// <param name="thing">
        /// The Thing to update.
        /// </param>
        /// <param name="container">
        /// The container instance of the DTO to be updated.
        /// </param>
        /// <returns>
        /// True if the update was successful.
        /// </returns>
        public bool UpdateConcept(NpgsqlTransaction transaction, string partition, Thing thing, Thing container)
        {
            if (!this.IsInstanceModifyAllowed(transaction, thing, partition, UpdateOperation))
            {
                throw new SecurityException("The person " + this.PermissionService.Credentials.Person.UserName + " does not have an appropriate update permission for " + thing.GetType().Name + ".");
            }

            var iteration = thing as Iteration;
            return this.IterationDao.Update(transaction, partition, iteration, container);
        }

        /// <summary>
        /// Persist the supplied DTO instance.
        /// </summary>
        /// <param name="transaction">
        /// The transaction object.
        /// </param>
        /// <param name="partition">
        /// The database partition (schema) where the requested resource will be stored.
        /// </param>
        /// <param name="thing">
        /// The Thing to create.
        /// </param>
        /// <param name="container">
        /// The container instance of the DTO to be persisted.
        /// </param>
        /// <param name="sequence">
        /// The order sequence used to persist this instance. Default is not used (-1).
        /// </param>
        /// <returns>
        /// True if the persistence was successful.
        /// </returns>
        public bool CreateConcept(NpgsqlTransaction transaction, string partition, Thing thing, Thing container, long sequence = -1)
        {
            if (!this.IsInstanceModifyAllowed(transaction, thing, partition, CreateOperation))
            {
                throw new SecurityException("The person " + this.PermissionService.Credentials.Person.UserName + " does not have an appropriate create permission for " + thing.GetType().Name + ".");
            }

            var iteration = thing as Iteration;
            var createSuccesful = this.IterationDao.Write(transaction, partition, iteration, container);
            return createSuccesful && this.CreateContainment(transaction, partition, iteration);
        }

        /// <summary>
        /// Get the requested data from the ORM layer.
        /// </summary>
        /// <param name="transaction">
        /// The transaction object.
        /// </param>
        /// <param name="partition">
        /// The database partition (schema) where the requested resource is stored.
        /// </param>
        /// <param name="ids">
        /// Ids to retrieve from the database.
        /// </param>
        /// <param name="containerSecurityContext">
        /// The security context of the container instance.
        /// </param>
        /// <returns>
        /// List of instances of <see cref="Iteration"/>.
        /// </returns>
        public IEnumerable<Thing> GetShallow(NpgsqlTransaction transaction, string partition, IEnumerable<Guid> ids, ISecurityContext containerSecurityContext)
        {
            var idFilter = ids == null ? null : ids.ToArray();
            var authorizedContext = this.AuthorizeReadRequest("Iteration", containerSecurityContext, partition);
            var isAllowed = authorizedContext.ContainerReadAllowed && this.BeforeGet(transaction, partition, idFilter);
            if (!isAllowed || (idFilter != null && !idFilter.Any()))
            {
                return Enumerable.Empty<Thing>();
            }

            var iterationColl = new List<Thing>(this.IterationDao.Read(transaction, partition, idFilter, this.TransactionManager.IsCachedDtoReadEnabled(transaction)));

            return this.AfterGet(iterationColl, transaction, partition, idFilter);
        }

        /// <summary>
        /// Get the requested data from the ORM layer by chaining the containment properties.
        /// </summary>
        /// <param name="transaction">
        /// The transaction object.
        /// </param>
        /// <param name="partition">
        /// The database partition (schema) where the requested resource is stored.
        /// </param>
        /// <param name="ids">
        /// Ids to retrieve from the database.
        /// </param>
        /// <param name="containerSecurityContext">
        /// The security context of the container instance.
        /// </param>
        /// <returns>
        /// List of instances of <see cref="Iteration"/>.
        /// </returns>
        public IEnumerable<Thing> GetDeep(NpgsqlTransaction transaction, string partition, IEnumerable<Guid> ids, ISecurityContext containerSecurityContext)
        {
            var idFilter = ids == null ? null : ids.ToArray();
            if (idFilter != null && !idFilter.Any())
            {
                return Enumerable.Empty<Thing>();
            }

            var results = new List<Thing>(this.GetShallow(transaction, partition, idFilter, containerSecurityContext));
            var iterationColl = results.Where(i => i.GetType() == typeof(Iteration)).Cast<Iteration>().ToList();

            var iterationPartition = partition.Replace("EngineeringModel", "Iteration");
            results.AddRange(this.OptionService.GetDeep(transaction, iterationPartition, iterationColl.SelectMany(x => x.Option).ToIdList(), containerSecurityContext));
            results.AddRange(this.PublicationService.GetDeep(transaction, iterationPartition, iterationColl.SelectMany(x => x.Publication), containerSecurityContext));
            results.AddRange(this.PossibleFiniteStateListService.GetDeep(transaction, iterationPartition, iterationColl.SelectMany(x => x.PossibleFiniteStateList), containerSecurityContext));
            results.AddRange(this.ElementDefinitionService.GetDeep(transaction, iterationPartition, iterationColl.SelectMany(x => x.Element), containerSecurityContext));
            results.AddRange(this.RelationshipService.GetDeep(transaction, iterationPartition, iterationColl.SelectMany(x => x.Relationship), containerSecurityContext));
            results.AddRange(this.ExternalIdentifierMapService.GetDeep(transaction, iterationPartition, iterationColl.SelectMany(x => x.ExternalIdentifierMap), containerSecurityContext));
            results.AddRange(this.RequirementsSpecificationService.GetDeep(transaction, iterationPartition, iterationColl.SelectMany(x => x.RequirementsSpecification), containerSecurityContext));
            results.AddRange(this.DomainFileStoreService.GetDeep(transaction, iterationPartition, iterationColl.SelectMany(x => x.DomainFileStore), containerSecurityContext));
            results.AddRange(this.ActualFiniteStateListService.GetDeep(transaction, iterationPartition, iterationColl.SelectMany(x => x.ActualFiniteStateList), containerSecurityContext));
            results.AddRange(this.RuleVerificationListService.GetDeep(transaction, iterationPartition, iterationColl.SelectMany(x => x.RuleVerificationList), containerSecurityContext));
            results.AddRange(this.StakeholderService.GetDeep(transaction, iterationPartition, iterationColl.SelectMany(x => x.Stakeholder), containerSecurityContext));
            results.AddRange(this.GoalService.GetDeep(transaction, iterationPartition, iterationColl.SelectMany(x => x.Goal), containerSecurityContext));
            results.AddRange(this.ValueGroupService.GetDeep(transaction, iterationPartition, iterationColl.SelectMany(x => x.ValueGroup), containerSecurityContext));
            results.AddRange(this.StakeholderValueService.GetDeep(transaction, iterationPartition, iterationColl.SelectMany(x => x.StakeholderValue), containerSecurityContext));
            results.AddRange(this.StakeHolderValueMapService.GetDeep(transaction, iterationPartition, iterationColl.SelectMany(x => x.StakeholderValueMap), containerSecurityContext));
            results.AddRange(this.SharedStyleService.GetDeep(transaction, iterationPartition, iterationColl.SelectMany(x => x.SharedDiagramStyle), containerSecurityContext));
            results.AddRange(this.DiagramCanvasService.GetDeep(transaction, iterationPartition, iterationColl.SelectMany(x => x.DiagramCanvas), containerSecurityContext));

            return results;
         }

        /// <summary>
        /// Execute additional logic after each get function call.
        /// </summary>
        /// <param name="resultCollection">
        /// An instance collection that was retrieved from the persistence layer.
        /// </param>
        /// <param name="transaction">
        /// The current transaction to the database.
        /// </param>
        /// <param name="partition">
        /// The database partition (schema) from which the requested resource is to be retrieved.
        /// </param>
        /// <param name="ids">
        /// Ids to retrieve from the database.
        /// </param>
        /// <param name="includeReferenceData">
        /// Control flag to indicate if reference library data should be retrieved extent=deep or extent=shallow.
        /// </param>
        /// <returns>
        /// A post filtered instance of the passed in resultCollection.
        /// </returns>
        public override IEnumerable<Thing> AfterGet(IEnumerable<Thing> resultCollection, NpgsqlTransaction transaction, string partition, IEnumerable<Guid> ids, bool includeReferenceData = false)
        {
            var filteredCollection = new List<Thing>();
            foreach (var thing in resultCollection)
            {
                if (this.IsInstanceReadAllowed(transaction, thing, partition))
                {
                    filteredCollection.Add(thing);
                }
                else
                {
                    Logger.Info("The person " + this.PermissionService.Credentials.Person.UserName + " does not have a read permission for " + thing.GetType().Name + ".");
                }
            }
            
            return filteredCollection;
        }


        /// <summary>
        /// Persist the DTO composition to the ORM layer.
        /// </summary>
        /// <param name="transaction">
        /// The transaction object.
        /// </param>
        /// <param name="partition">
        /// The database partition (schema) where the requested resource will be stored.
        /// </param>
        /// <param name="iteration">
        /// The iteration instance to persist.
        /// </param>
        /// <returns>
        /// True if the persistence was successful.
        /// </returns>
        private bool CreateContainment(NpgsqlTransaction transaction, string partition, Iteration iteration)
        {
            var results = new List<bool>();
            var iterationPartition = partition.Replace("EngineeringModel", "Iteration");

            foreach (var option in this.ResolveFromRequestCache(iteration.Option))
            {
               results.Add(this.OptionService.CreateConcept(transaction, iterationPartition, (Option)option.V, iteration, option.K));
            }

            foreach (var publication in this.ResolveFromRequestCache(iteration.Publication))
            {
               results.Add(this.PublicationService.CreateConcept(transaction, iterationPartition, publication, iteration));
            }

            foreach (var possibleFiniteStateList in this.ResolveFromRequestCache(iteration.PossibleFiniteStateList))
            {
               results.Add(this.PossibleFiniteStateListService.CreateConcept(transaction, iterationPartition, possibleFiniteStateList, iteration));
            }

            foreach (var element in this.ResolveFromRequestCache(iteration.Element))
            {
               results.Add(this.ElementDefinitionService.CreateConcept(transaction, iterationPartition, element, iteration));
            }

            foreach (var relationship in this.ResolveFromRequestCache(iteration.Relationship))
            {
               results.Add(this.RelationshipService.CreateConcept(transaction, iterationPartition, relationship, iteration));
            }

            foreach (var externalIdentifierMap in this.ResolveFromRequestCache(iteration.ExternalIdentifierMap))
            {
               results.Add(this.ExternalIdentifierMapService.CreateConcept(transaction, iterationPartition, externalIdentifierMap, iteration));
            }

            foreach (var requirementsSpecification in this.ResolveFromRequestCache(iteration.RequirementsSpecification))
            {
               results.Add(this.RequirementsSpecificationService.CreateConcept(transaction, iterationPartition, requirementsSpecification, iteration));
            }

            foreach (var domainFileStore in this.ResolveFromRequestCache(iteration.DomainFileStore))
            {
               results.Add(this.DomainFileStoreService.CreateConcept(transaction, iterationPartition, domainFileStore, iteration));
            }

            foreach (var actualFiniteStateList in this.ResolveFromRequestCache(iteration.ActualFiniteStateList))
            {
               results.Add(this.ActualFiniteStateListService.CreateConcept(transaction, iterationPartition, actualFiniteStateList, iteration));
            }

            foreach (var ruleVerificationList in this.ResolveFromRequestCache(iteration.RuleVerificationList))
            {
               results.Add(this.RuleVerificationListService.CreateConcept(transaction, iterationPartition, ruleVerificationList, iteration));
            }

            foreach (var stakeholder in this.ResolveFromRequestCache(iteration.Stakeholder))
            {
               results.Add(this.StakeholderService.CreateConcept(transaction, iterationPartition, stakeholder, iteration));
            }

            foreach (var goal in this.ResolveFromRequestCache(iteration.Goal))
            {
               results.Add(this.GoalService.CreateConcept(transaction, iterationPartition, goal, iteration));
            }

            foreach (var valueGroup in this.ResolveFromRequestCache(iteration.ValueGroup))
            {
               results.Add(this.ValueGroupService.CreateConcept(transaction, iterationPartition, valueGroup, iteration));
            }

            foreach (var stakeholderValue in this.ResolveFromRequestCache(iteration.StakeholderValue))
            {
               results.Add(this.StakeholderValueService.CreateConcept(transaction, iterationPartition, stakeholderValue, iteration));
            }

            foreach (var stakeholderValueMap in this.ResolveFromRequestCache(iteration.StakeholderValueMap))
            {
               results.Add(this.StakeHolderValueMapService.CreateConcept(transaction, iterationPartition, stakeholderValueMap, iteration));
            }

            foreach (var sharedDiagramStyle in this.ResolveFromRequestCache(iteration.SharedDiagramStyle))
            {
               results.Add(this.SharedStyleService.CreateConcept(transaction, iterationPartition, sharedDiagramStyle, iteration));
            }

            foreach (var diagramCanvas in this.ResolveFromRequestCache(iteration.DiagramCanvas))
            {
               results.Add(this.DiagramCanvasService.CreateConcept(transaction, iterationPartition, diagramCanvas, iteration));
            }

            return results.All(x => x);
        }
    }
}