﻿// --------------------------------------------------------------------------------------------------------------------
// <copyright file="AndExpressionSideEffect.cs" company="RHEA System S.A.">
//   Copyright (c) 2017 RHEA System S.A.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace CDP4WebServices.API.Services.Operations.SideEffects
{
    using CDP4Common.DTO;

    /// <summary>
    /// The purpose of the <see cref="AndExpressionSideEffect"/> class is to execute additional logic before and after a specific operation is performed.
    /// </summary>
    public class AndExpressionSideEffect : BooleanExpressionSideEffect<AndExpression>
    {
    }
}