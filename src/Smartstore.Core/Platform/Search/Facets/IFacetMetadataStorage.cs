﻿namespace Smartstore.Core.Search.Facets
{
    public class FacetMetadataContext
    {
        public bool Cache { get; init; }
        public string FieldName { get; init; }
        public string DocumentType { get; init; }

        public ISearchQuery Query { get; init; } = new SearchQuery();
        public string[] ApplyOriginalFilters { get; init; } = Array.Empty<string>();

        public FacetDescriptor Descriptor { get; init; }
        public FacetMetadataContext ParentContext { get; init; }
        public Func<ISearchHit, MetadataGeneratorContext, FacetMetadata> MetadataGenerator { get; init; }
    }

    public class MetadataGeneratorContext
    {
        public ISearchEngine SearchEngine { get; init; }
        public FacetMetadataContext Context { get; init; }
        public IDictionary<object, FacetMetadata> ParentData { get; init; }
    }

    // TODO: (mg) (core) add storing of facet metadata to IMetadataStorage.
    /// <summary>
    /// Loading of facet metadata. Metadata is stored during indexing and loaded when searching with facets.
    /// </summary>
    public interface IFacetMetadataStorage
    {
        /// <summary>
        /// Loads facet metadata from a medium like a file-based search index.
        /// </summary>
        /// <param name="searchEngine">Search engine instance.</param>
        /// <param name="context">Context for loading facet metadata.</param>
        /// <returns>Dictionary of <see cref="FacetValue.Value"/> to <see cref="FacetMetadata"/>.</returns>
        Task<IDictionary<object, FacetMetadata>> LoadAsync(ISearchEngine searchEngine, FacetMetadataContext context);
    }
}
