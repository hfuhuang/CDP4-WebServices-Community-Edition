﻿// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ParameterSubscriptionSideEffectTestFixture.cs" company="RHEA System S.A.">
//   Copyright (c) 2017 RHEA System S.A.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace CDP4WebServices.API.Tests.SideEffects
{
    using System;
    using System.Collections.Generic;

    using CDP4Common;
    using CDP4Common.DTO;
    using CDP4WebServices.API.Services;
    using CDP4WebServices.API.Services.Authorization;
    using CDP4WebServices.API.Services.Operations.SideEffects;
    using Moq;
    using Npgsql;
    using NUnit.Framework;

    /// <summary>
    /// Suite of tests for the <see cref="ParameterSubscriptionSideEffect"/>
    /// </summary>
    [TestFixture]
    public class ParameterSubscriptionSideEffectTestFixture
    {
        private Mock<ISecurityContext> securityContext;        
        private Mock<IParameterSubscriptionValueSetService> parameterSubscriptionValueSetService;

        private NpgsqlTransaction npgsqlTransaction;
        private ParameterSubscriptionSideEffect sideEffect;

        [SetUp]
        public void Setup()
        {
            this.securityContext = new Mock<ISecurityContext>();
            this.npgsqlTransaction = null;
            
            this.parameterSubscriptionValueSetService = new Mock<IParameterSubscriptionValueSetService>();
            this.parameterSubscriptionValueSetService.Setup(
                x => x.CreateConcept(
                    It.IsAny<NpgsqlTransaction>(),
                    It.IsAny<string>(),
                    It.IsAny<ParameterSubscriptionValueSet>(),
                    It.IsAny<ParameterSubscription>(),
                    It.IsAny<long>())).Returns(true);
            
            this.sideEffect = new ParameterSubscriptionSideEffect()
                                  {
                                      ParameterSubscriptionValueSetService = this.parameterSubscriptionValueSetService.Object
                                  };            
        }

        [Test]
        public void VerifyThatTheWhenOwnerOfTheParameterAndSubscriptionAreEqualExceptionIsThrown()
        {
            var owner = Guid.NewGuid();
            var parameterSubscription = new ParameterSubscription(Guid.NewGuid(), 1) {Owner = owner};
            
            var parameter = new Parameter(Guid.NewGuid(), 1) {Owner = owner};
            parameter.ValueSet.Add(Guid.NewGuid());
            
            Assert.Throws<Cdp4ModelValidationException>(() => this.sideEffect.BeforeCreate(parameterSubscription, parameter, this.npgsqlTransaction, "partition", this.securityContext.Object));

            Assert.Throws<Cdp4ModelValidationException>(() => this.sideEffect.BeforeUpdate(parameterSubscription, parameter, this.npgsqlTransaction, "partition", this.securityContext.Object, null));
        }

        [Test]
        public void VerifyThatWhenAParameterSubscriptionIsPostedValueSetsAreCreated()
        {
            var parameterSubscription = new ParameterSubscription(Guid.NewGuid(), 1) { Owner = Guid.NewGuid() } ;            
            var originalparameterSubscription = new ParameterSubscription(parameterSubscription.Iid, 1);

            var parameter = new Parameter(Guid.NewGuid(), 1) { Owner = Guid.NewGuid() } ;
            parameter.ValueSet = new List<Guid>() { Guid.NewGuid() };

            this.sideEffect.AfterCreate(parameterSubscription, parameter, originalparameterSubscription, this.npgsqlTransaction, "partition", this.securityContext.Object);
            
            this.parameterSubscriptionValueSetService.Verify(x => x.CreateConcept(this.npgsqlTransaction, "partition", It.IsAny<ParameterSubscriptionValueSet>(), It.IsAny<ParameterSubscription>(), It.IsAny<long>()), Times.Once);
        }
    }
}