window.BENCHMARK_DATA = {
  "lastUpdate": 1769975445335,
  "repoUrl": "https://github.com/jasonmcboyd/Arborist",
  "entries": {
    "Traversal Benchmarks": [
      {
        "commit": {
          "author": {
            "email": "jason.boyd.ce@gmail.com",
            "name": "Jason Boyd",
            "username": "jasonmcboyd"
          },
          "committer": {
            "email": "noreply@github.com",
            "name": "GitHub",
            "username": "web-flow"
          },
          "distinct": true,
          "id": "87495d479356376af86c640133c60ba9554a0463",
          "message": "Merge pull request #13 from jasonmcboyd/continuous-benchmark\n\nAdd skip-fetch-gh-pages to subsequent benchmark steps",
          "timestamp": "2026-02-01T11:32:13-08:00",
          "tree_id": "0695c436854481b2e3514228c9f79baa1419b3ae",
          "url": "https://github.com/jasonmcboyd/Arborist/commit/87495d479356376af86c640133c60ba9554a0463"
        },
        "date": 1769975445089,
        "tool": "benchmarkdotnet",
        "benches": [
          {
            "name": "Arborist.Benchmarks.BreadthFirstTreenumerator.TriangleTree_2896",
            "value": 276613673.5416667,
            "unit": "ns",
            "range": "± 561984.6983442804"
          },
          {
            "name": "Arborist.Benchmarks.BreadthFirstTreenumerator.CompleteBinaryTree_21",
            "value": 301286275.3214286,
            "unit": "ns",
            "range": "± 801799.8304286955"
          },
          {
            "name": "Arborist.Benchmarks.BreadthFirstTreenumerator.TrivialForest_4M",
            "value": 16122057.453125,
            "unit": "ns",
            "range": "± 30242.796426572888"
          },
          {
            "name": "Arborist.Benchmarks.BreadthFirstTreenumerator.DegenerateTree_4M",
            "value": 88146252.01538461,
            "unit": "ns",
            "range": "± 118232.96129406396"
          },
          {
            "name": "Arborist.Benchmarks.DepthFirstTreenumerator.TriangleTree_2896",
            "value": 204994060.09523806,
            "unit": "ns",
            "range": "± 1565714.0337122604"
          },
          {
            "name": "Arborist.Benchmarks.DepthFirstTreenumerator.CompleteBinaryTree_21",
            "value": 227167228.0666667,
            "unit": "ns",
            "range": "± 2673219.0660257475"
          },
          {
            "name": "Arborist.Benchmarks.DepthFirstTreenumerator.TrivialForest_4M",
            "value": 13986547.785714285,
            "unit": "ns",
            "range": "± 31759.790398919075"
          },
          {
            "name": "Arborist.Benchmarks.DepthFirstTreenumerator.DegenerateTree_4M",
            "value": 64428204.95,
            "unit": "ns",
            "range": "± 139671.06322530145"
          },
          {
            "name": "Arborist.Benchmarks.LevelOrderTraversal.DeepTree",
            "value": 49817652.892857134,
            "unit": "ns",
            "range": "± 411092.9328108678"
          },
          {
            "name": "Arborist.Benchmarks.LevelOrderTraversal.TriangleTree_PruneAfter_1447",
            "value": 74084294.84761904,
            "unit": "ns",
            "range": "± 336603.5470359144"
          },
          {
            "name": "Arborist.Benchmarks.LevelOrderTraversal.CompleteBinaryTree_PruneBefore_20",
            "value": 171534704.61904764,
            "unit": "ns",
            "range": "± 1486407.886855548"
          },
          {
            "name": "Arborist.Benchmarks.LevelOrderTraversal.CompleteBinaryTree_PruneAfter_19",
            "value": 85059587.03333333,
            "unit": "ns",
            "range": "± 417158.30144472735"
          },
          {
            "name": "Arborist.Benchmarks.PostOrderTraversal.DeepTree",
            "value": 33919149.738095246,
            "unit": "ns",
            "range": "± 123521.60371975786"
          },
          {
            "name": "Arborist.Benchmarks.PostOrderTraversal.TriangleTree_PruneAfter_1447",
            "value": 38515039.004761904,
            "unit": "ns",
            "range": "± 376909.1316085572"
          },
          {
            "name": "Arborist.Benchmarks.PostOrderTraversal.CompleteBinaryTree_PruneBefore_20",
            "value": 142468022.89285713,
            "unit": "ns",
            "range": "± 456031.2014246975"
          },
          {
            "name": "Arborist.Benchmarks.PostOrderTraversal.CompleteBinaryTree_PruneAfter_19",
            "value": 57802240.76190476,
            "unit": "ns",
            "range": "± 140066.49881144648"
          },
          {
            "name": "Arborist.Benchmarks.PreorderTraversal.DeepTree",
            "value": 21541709.47544643,
            "unit": "ns",
            "range": "± 197137.80836781012"
          },
          {
            "name": "Arborist.Benchmarks.PreorderTraversal.TriangleTree_PruneAfter_1447",
            "value": 34153140.93777778,
            "unit": "ns",
            "range": "± 144691.90363253176"
          },
          {
            "name": "Arborist.Benchmarks.PreorderTraversal.CompleteBinaryTree_PruneBefore_20",
            "value": 134475716.80357143,
            "unit": "ns",
            "range": "± 1090596.9969061106"
          },
          {
            "name": "Arborist.Benchmarks.PreorderTraversal.CompleteBinaryTree_PruneAfterDepth_19",
            "value": 49207347.42424243,
            "unit": "ns",
            "range": "± 118854.13545424554"
          }
        ]
      }
    ]
  }
}