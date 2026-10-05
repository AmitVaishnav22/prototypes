# Vector Databases: Storing and Searching Meaning

How vector databases solve the storage and search problem for embeddings, and how they power semantic search.

## Mind map

```mermaid
mindmap
  root((Vector Databases))
    The problem
      Millions of documents
      Each converted to an embedding
      Traditional databases can't search this well
    The solution
      Vector database
        Stores vectors efficiently
        Enables high-speed similarity search
    Semantic vs exact match
      Traditional search
        Exact keyword matching
        Fails on different wording
        remote work misses work from home
      Semantic search
        Focuses on meaning
        Similar concepts map to similar coordinates
        Finds relevance without exact words
    How it works
      Query converted to a vector
      Database finds close vectors
      Closeness means higher relevance
    Key distinctions
      Embeddings vs storage
        Embedding models generate vectors
        Vector DBs store and index them
      Example tools
        Pinecone
        Weaviate
        Milvus
    Connecting to RAG
      Vector DB retrieves relevant data
      LLM generates the natural language answer
      Foundation of RAG
```

## How a query flows through a vector database

```mermaid
flowchart TD
    A["Documents<br/>Millions of text documents"] --> B["Embedding model<br/>e.g. OpenAI embeddings"]
    B --> C["Embeddings<br/>Vectors of floating-point numbers"]
    C --> D["Vector database<br/>Pinecone, Weaviate, Milvus"]
    Q["User query"] --> E["Embedding model<br/>Converts query to a vector"]
    E --> F["Query vector"]
    F --> G["Similarity search<br/>Finds vectors mathematically close to the query"]
    D --> G
    G --> H["Relevant documents retrieved"]
    H --> I["LLM<br/>Generates a natural language response"]
    I --> J["Foundation of RAG<br/>Retrieval-Augmented Generation"]
```

## Exact match vs semantic search

```mermaid
flowchart LR
    subgraph Exact["Traditional: exact keyword match"]
        q1["Search: remote work"] -.->|"no match"| d1["Document: work from home"]
    end
    subgraph Semantic["Semantic: meaning-based match"]
        q2["Search: remote work"] -->|"close in vector space"| d2["Document: work from home"]
    end
```

## Summary

### The storage problem

AI models convert text into embeddings: mathematical coordinates made up of floating-point numbers. With millions of documents, storing and efficiently searching these embeddings is more than traditional databases are built to handle.

### What a vector database is

A vector database is purpose-built to store vectors efficiently and perform high-speed similarity searches across them.

### Semantic search vs exact match

Traditional databases rely on exact keyword matching, so a search for "remote work" can fail to find a document that says "work from home." Semantic search instead looks at meaning: because similar concepts map to similar coordinates in vector space, the system can surface relevant results even when the exact wording differs.

### How it works

When a query comes in, the system converts it into a vector. The database then finds documents whose vectors sit mathematically close to the query vector, since closeness signals higher semantic relevance.

### Embeddings vs storage

Vector databases store and index embeddings, but they don't generate them. That's the job of separate embedding models, such as those from OpenAI. Popular vector database tools include Pinecone, Weaviate, and Milvus.

### Connecting to RAG

A vector database retrieves relevant information, but it still needs to be handed to an LLM to generate a natural language response. This retrieval-then-generation pattern is the foundation of RAG (Retrieval-Augmented Generation), covered next in the series.

## Key takeaways

| Concept | In one line |
| --- | --- |
| Embedding | A document or query represented as a vector of floating-point numbers |
| Storage problem | Traditional databases can't efficiently store or search millions of embeddings |
| Vector database | Purpose-built to store vectors and run high-speed similarity search |
| Semantic search | Matches by meaning, not exact keywords |
| Exact match limitation | Fails when wording differs even if meaning is the same |
| Embedding models vs vector DBs | Models generate embeddings; vector DBs store and index them |
| Example tools | Pinecone, Weaviate, Milvus |
| RAG connection | Vector DB retrieves data; the LLM generates the final response |