﻿// <auto-generated />
using System;
using System.Net.Http;
using System.Collections.Generic;
using Refit;
using System.Threading.Tasks;

/* ******** Hey You! *********
 *
 * This is a generated file, and gets rewritten every time you build the
 * project. If you want to edit it, you need to edit the mustache template
 * in the Refit package */

#pragma warning disable
namespace GenesisTest.Core.RefitInternalGenerated
{
    [global::System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
    [AttributeUsage (AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Enum | AttributeTargets.Constructor | AttributeTargets.Method | AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Event | AttributeTargets.Interface | AttributeTargets.Delegate)]
    sealed class PreserveAttribute : Attribute
    {

        //
        // Fields
        //
        public bool AllMembers;

        public bool Conditional;
    }
}
#pragma warning restore

namespace GenesisTest.Core.Api
{
    using GenesisTest.Core.RefitInternalGenerated;

    /// <inheritdoc />
    [global::System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
    [global::System.Diagnostics.DebuggerNonUserCode]
    [Preserve]
    [global::System.Reflection.Obfuscation(Exclude=true)]
    partial class AutoGeneratedIGithubRepositories : IGithubRepositories
    {
        /// <inheritdoc />
        public HttpClient Client { get; protected set; }
        readonly IRequestBuilder requestBuilder;

        /// <inheritdoc />
        public AutoGeneratedIGithubRepositories(HttpClient client, IRequestBuilder requestBuilder)
        {
            Client = client;
            this.requestBuilder = requestBuilder;
        }

        /// <inheritdoc />
        Task<RepositoryResult> IGithubRepositories.GetRepositories(int pageNumber)
        {
            var arguments = new object[] { pageNumber };
            var func = requestBuilder.BuildRestResultFuncForMethod("GetRepositories", new Type[] { typeof(int) });
            return (Task<RepositoryResult>)func(Client, arguments);
        }
    }
}
