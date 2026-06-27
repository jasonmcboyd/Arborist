window.BENCHMARK_DATA = {
  "lastUpdate": 1782534426250,
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
      },
      {
        "commit": {
          "author": {
            "email": "jason.boyd.ce@gmail.com",
            "name": "Jason Boyd",
            "username": "jasonmcboyd"
          },
          "committer": {
            "email": "jason.boyd.ce@gmail.com",
            "name": "Jason Boyd",
            "username": "jasonmcboyd"
          },
          "distinct": true,
          "id": "5993e5762d3c0ffa1bbacbb40b35307cff9a3f87",
          "message": "Docs: document BFT Where O(N) prefix-carry design\n\nRewrite the WhereBreadthFirstTreenumerator section to match the implementation (queue + incremental _PredSkipPrefix, predicate-skip vs consumer-SkipNode, the real parent-visit fields, front-anchored sibling index). Update the DFT-vs-BFT table (BFT now O(N)) and fix stale _ExtraParentVisitsEmitted/_LastRemovedSkippedNodePosition/_SkippedStack references.\n\nCo-Authored-By: Claude Opus 4.8 (1M context) <noreply@anthropic.com>\nClaude-Session: https://claude.ai/code/session_01Wg3xArL4FATQaXQMBvhXdg",
          "timestamp": "2026-06-23T04:33:54Z",
          "tree_id": "a2615b3ce8e91df5baffd5f8f08e3c6ac9df2e3b",
          "url": "https://github.com/jasonmcboyd/Arborist/commit/5993e5762d3c0ffa1bbacbb40b35307cff9a3f87"
        },
        "date": 1782190983201,
        "tool": "benchmarkdotnet",
        "benches": [
          {
            "name": "Arborist.Benchmarks.BreadthFirstTreenumerator.TriangleTree_2896",
            "value": 277581951.9230769,
            "unit": "ns",
            "range": "± 1419097.9551760138"
          },
          {
            "name": "Arborist.Benchmarks.BreadthFirstTreenumerator.CompleteBinaryTree_21",
            "value": 309830230.1923077,
            "unit": "ns",
            "range": "± 1402313.0186655833"
          },
          {
            "name": "Arborist.Benchmarks.BreadthFirstTreenumerator.TrivialForest_4M",
            "value": 16168190.194711538,
            "unit": "ns",
            "range": "± 18159.972549440823"
          },
          {
            "name": "Arborist.Benchmarks.BreadthFirstTreenumerator.DegenerateTree_4M",
            "value": 103637297.46666668,
            "unit": "ns",
            "range": "± 1788712.8378982984"
          },
          {
            "name": "Arborist.Benchmarks.DepthFirstTreenumerator.TriangleTree_2896",
            "value": 243145997.59523812,
            "unit": "ns",
            "range": "± 1297393.8684765722"
          },
          {
            "name": "Arborist.Benchmarks.DepthFirstTreenumerator.CompleteBinaryTree_21",
            "value": 235046784.10256407,
            "unit": "ns",
            "range": "± 1702046.736174873"
          },
          {
            "name": "Arborist.Benchmarks.DepthFirstTreenumerator.TrivialForest_4M",
            "value": 14959969.957589285,
            "unit": "ns",
            "range": "± 10999.335903507434"
          },
          {
            "name": "Arborist.Benchmarks.DepthFirstTreenumerator.DegenerateTree_4M",
            "value": 63137056.821428575,
            "unit": "ns",
            "range": "± 116354.33984789404"
          },
          {
            "name": "Arborist.Benchmarks.LevelOrderTraversal.DeepTree",
            "value": 54250399.559999995,
            "unit": "ns",
            "range": "± 142856.9507033027"
          },
          {
            "name": "Arborist.Benchmarks.LevelOrderTraversal.TriangleTree_PruneAfter_1447",
            "value": 79744638.36263736,
            "unit": "ns",
            "range": "± 131300.4243475589"
          },
          {
            "name": "Arborist.Benchmarks.LevelOrderTraversal.CompleteBinaryTree_PruneBefore_20",
            "value": 178871379.1190476,
            "unit": "ns",
            "range": "± 703562.9512240898"
          },
          {
            "name": "Arborist.Benchmarks.LevelOrderTraversal.CompleteBinaryTree_PruneAfter_19",
            "value": 88475073.28571428,
            "unit": "ns",
            "range": "± 151656.8634303335"
          },
          {
            "name": "Arborist.Benchmarks.PostOrderTraversal.DeepTree",
            "value": 34059758.85714286,
            "unit": "ns",
            "range": "± 205660.04440459682"
          },
          {
            "name": "Arborist.Benchmarks.PostOrderTraversal.TriangleTree_PruneAfter_1447",
            "value": 39680656.887573965,
            "unit": "ns",
            "range": "± 96675.8967889945"
          },
          {
            "name": "Arborist.Benchmarks.PostOrderTraversal.CompleteBinaryTree_PruneBefore_20",
            "value": 120860364.06153846,
            "unit": "ns",
            "range": "± 220658.04822823775"
          },
          {
            "name": "Arborist.Benchmarks.PostOrderTraversal.CompleteBinaryTree_PruneAfter_19",
            "value": 47557293.90909091,
            "unit": "ns",
            "range": "± 254519.06295964532"
          },
          {
            "name": "Arborist.Benchmarks.PreorderTraversal.DeepTree",
            "value": 21842990.875,
            "unit": "ns",
            "range": "± 101278.22887590519"
          },
          {
            "name": "Arborist.Benchmarks.PreorderTraversal.TriangleTree_PruneAfter_1447",
            "value": 34207983.537777774,
            "unit": "ns",
            "range": "± 96666.6204360401"
          },
          {
            "name": "Arborist.Benchmarks.PreorderTraversal.CompleteBinaryTree_PruneBefore_20",
            "value": 113307868.15384616,
            "unit": "ns",
            "range": "± 288774.3801000415"
          },
          {
            "name": "Arborist.Benchmarks.PreorderTraversal.CompleteBinaryTree_PruneAfterDepth_19",
            "value": 39564358.39102564,
            "unit": "ns",
            "range": "± 65221.002871289274"
          }
        ]
      },
      {
        "commit": {
          "author": {
            "email": "jason.boyd.ce@gmail.com",
            "name": "Jason Boyd",
            "username": "jasonmcboyd"
          },
          "committer": {
            "email": "jason.boyd.ce@gmail.com",
            "name": "Jason Boyd",
            "username": "jasonmcboyd"
          },
          "distinct": true,
          "id": "c3ab5ef05fc7c7304bb98cd14e97bed3a8f91e62",
          "message": "Align LeaffixAggregate with LeaffixScan (flat, lazy, zero per-node alloc)\n\nLeaffixAggregate now uses the same flat forward DFS + no-copy ChildAccumulations view as LeaffixScan, yielding each root's accumulated value. Accumulator changes TAccumulate[] -> ChildAccumulations<TAccumulate>. Per-root lazy: a root is emitted the moment its subtree completes and the buffers are reused for the next root, so peak memory is the largest root subtree (not the whole forest) and early-terminating consumers traverse fewer roots -- matching the previous LeaffixAggregator behavior. Zero per-node heap allocation. Adds LeaffixAggregateTests (it previously had none and no callers).\n\nLeaffixAggregator is retained -- still used by Invert (TreeInverter); it'll be removed when Invert is redesigned onto a flat structure.\n\nCo-Authored-By: Claude Opus 4.8 (1M context) <noreply@anthropic.com>\nClaude-Session: https://claude.ai/code/session_01Wg3xArL4FATQaXQMBvhXdg",
          "timestamp": "2026-06-23T21:57:19Z",
          "tree_id": "7dacc032f0faefb81f1e004514e835a60b5bf81a",
          "url": "https://github.com/jasonmcboyd/Arborist/commit/c3ab5ef05fc7c7304bb98cd14e97bed3a8f91e62"
        },
        "date": 1782253172738,
        "tool": "benchmarkdotnet",
        "benches": [
          {
            "name": "Arborist.Benchmarks.BreadthFirstTreenumerator.TriangleTree_2896",
            "value": 275716665.60714287,
            "unit": "ns",
            "range": "± 630345.875078857"
          },
          {
            "name": "Arborist.Benchmarks.BreadthFirstTreenumerator.CompleteBinaryTree_21",
            "value": 321945188.8,
            "unit": "ns",
            "range": "± 1720731.504533234"
          },
          {
            "name": "Arborist.Benchmarks.BreadthFirstTreenumerator.TrivialForest_4M",
            "value": 16169237.411458334,
            "unit": "ns",
            "range": "± 17432.18659512301"
          },
          {
            "name": "Arborist.Benchmarks.BreadthFirstTreenumerator.DegenerateTree_4M",
            "value": 104140420.54666667,
            "unit": "ns",
            "range": "± 1426100.4468362874"
          },
          {
            "name": "Arborist.Benchmarks.DepthFirstTreenumerator.TriangleTree_2896",
            "value": 212512800.74358973,
            "unit": "ns",
            "range": "± 667215.2156359986"
          },
          {
            "name": "Arborist.Benchmarks.DepthFirstTreenumerator.CompleteBinaryTree_21",
            "value": 232234396.20000002,
            "unit": "ns",
            "range": "± 3257514.5855634348"
          },
          {
            "name": "Arborist.Benchmarks.DepthFirstTreenumerator.TrivialForest_4M",
            "value": 14972711.158482144,
            "unit": "ns",
            "range": "± 29302.046557888898"
          },
          {
            "name": "Arborist.Benchmarks.DepthFirstTreenumerator.DegenerateTree_4M",
            "value": 62655531.2755102,
            "unit": "ns",
            "range": "± 166153.6564954233"
          },
          {
            "name": "Arborist.Benchmarks.LevelOrderTraversal.DeepTree",
            "value": 54567763.21428572,
            "unit": "ns",
            "range": "± 153131.84784097693"
          },
          {
            "name": "Arborist.Benchmarks.LevelOrderTraversal.TriangleTree_PruneAfter_1447",
            "value": 78948744.02380951,
            "unit": "ns",
            "range": "± 33595.81883334198"
          },
          {
            "name": "Arborist.Benchmarks.LevelOrderTraversal.CompleteBinaryTree_PruneBefore_20",
            "value": 179254929.42222223,
            "unit": "ns",
            "range": "± 1356234.9291923414"
          },
          {
            "name": "Arborist.Benchmarks.LevelOrderTraversal.CompleteBinaryTree_PruneAfter_19",
            "value": 86525562.4404762,
            "unit": "ns",
            "range": "± 468730.29628517095"
          },
          {
            "name": "Arborist.Benchmarks.PostOrderTraversal.DeepTree",
            "value": 34367732.68888889,
            "unit": "ns",
            "range": "± 197350.4448459786"
          },
          {
            "name": "Arborist.Benchmarks.PostOrderTraversal.TriangleTree_PruneAfter_1447",
            "value": 38707149.06190477,
            "unit": "ns",
            "range": "± 114288.25217164274"
          },
          {
            "name": "Arborist.Benchmarks.PostOrderTraversal.CompleteBinaryTree_PruneBefore_20",
            "value": 121082668.07692307,
            "unit": "ns",
            "range": "± 133512.07088885346"
          },
          {
            "name": "Arborist.Benchmarks.PostOrderTraversal.CompleteBinaryTree_PruneAfter_19",
            "value": 48387392.9020979,
            "unit": "ns",
            "range": "± 99492.73455636557"
          },
          {
            "name": "Arborist.Benchmarks.PreorderTraversal.DeepTree",
            "value": 22105386.291666668,
            "unit": "ns",
            "range": "± 175917.87168081495"
          },
          {
            "name": "Arborist.Benchmarks.PreorderTraversal.TriangleTree_PruneAfter_1447",
            "value": 33124303.91517857,
            "unit": "ns",
            "range": "± 56894.16394438056"
          },
          {
            "name": "Arborist.Benchmarks.PreorderTraversal.CompleteBinaryTree_PruneBefore_20",
            "value": 114264567.61666667,
            "unit": "ns",
            "range": "± 95272.86783715466"
          },
          {
            "name": "Arborist.Benchmarks.PreorderTraversal.CompleteBinaryTree_PruneAfterDepth_19",
            "value": 40995647.90384616,
            "unit": "ns",
            "range": "± 61940.83923361044"
          }
        ]
      },
      {
        "commit": {
          "author": {
            "email": "jason.boyd.ce@gmail.com",
            "name": "Jason Boyd",
            "username": "jasonmcboyd"
          },
          "committer": {
            "email": "jason.boyd.ce@gmail.com",
            "name": "Jason Boyd",
            "username": "jasonmcboyd"
          },
          "distinct": true,
          "id": "96f3cc44e2208f47258c2887c2a011530d708864",
          "message": "Rewrite Invert flat; retire SimpleNode and LeaffixAggregator\n\nInvert now materializes the source into flat pre-order arrays and emits the mirror with a stack -- pushing roots/children in forward order so they pop in reverse (the mirror's pre-order). Subtree sizes are invariant under mirroring; result is a PreorderTree. O(N), zero per-node allocation (was a SimpleNode object per node via LeaffixAggregator).\n\nInvert was the last user of both SimpleNode and LeaffixAggregator, so this deletes five files: TreeInverter, LeaffixAggregator (+NodeContextAndResult), SimpleNode, SimpleNodeChildEnumerator, SimpleNodeTreenumerable. The SimpleNode retirement is complete, clearing the runway for the nodeToValueMap elimination. (A level-order/LOUDS mirror -- reverse each child-run in place -- remains a future refinement if a LevelOrderTree is ever built.)\n\nCo-Authored-By: Claude Opus 4.8 (1M context) <noreply@anthropic.com>\nClaude-Session: https://claude.ai/code/session_01Wg3xArL4FATQaXQMBvhXdg",
          "timestamp": "2026-06-23T22:21:16Z",
          "tree_id": "aafd024b9cc4740d5e6c4fe63bc7cf95158cbb5e",
          "url": "https://github.com/jasonmcboyd/Arborist/commit/96f3cc44e2208f47258c2887c2a011530d708864"
        },
        "date": 1782254667294,
        "tool": "benchmarkdotnet",
        "benches": [
          {
            "name": "Arborist.Benchmarks.BreadthFirstTreenumerator.TriangleTree_2896",
            "value": 264752708.65384614,
            "unit": "ns",
            "range": "± 1653539.6801972704"
          },
          {
            "name": "Arborist.Benchmarks.BreadthFirstTreenumerator.CompleteBinaryTree_21",
            "value": 298234283.3,
            "unit": "ns",
            "range": "± 1462671.0828985807"
          },
          {
            "name": "Arborist.Benchmarks.BreadthFirstTreenumerator.TrivialForest_4M",
            "value": 14517467.276785715,
            "unit": "ns",
            "range": "± 31900.122414845995"
          },
          {
            "name": "Arborist.Benchmarks.BreadthFirstTreenumerator.DegenerateTree_4M",
            "value": 87537861.41666667,
            "unit": "ns",
            "range": "± 84174.97289897247"
          },
          {
            "name": "Arborist.Benchmarks.DepthFirstTreenumerator.TriangleTree_2896",
            "value": 199648341.0438597,
            "unit": "ns",
            "range": "± 6770150.112353437"
          },
          {
            "name": "Arborist.Benchmarks.DepthFirstTreenumerator.CompleteBinaryTree_21",
            "value": 213260946.59420288,
            "unit": "ns",
            "range": "± 5393573.355889313"
          },
          {
            "name": "Arborist.Benchmarks.DepthFirstTreenumerator.TrivialForest_4M",
            "value": 15693230.933333334,
            "unit": "ns",
            "range": "± 167157.48399641944"
          },
          {
            "name": "Arborist.Benchmarks.DepthFirstTreenumerator.DegenerateTree_4M",
            "value": 62527139.41071428,
            "unit": "ns",
            "range": "± 248184.4778186848"
          },
          {
            "name": "Arborist.Benchmarks.LevelOrderTraversal.DeepTree",
            "value": 54293733.093333334,
            "unit": "ns",
            "range": "± 364501.7178486263"
          },
          {
            "name": "Arborist.Benchmarks.LevelOrderTraversal.TriangleTree_PruneAfter_1447",
            "value": 76819125.70238096,
            "unit": "ns",
            "range": "± 51421.501918268106"
          },
          {
            "name": "Arborist.Benchmarks.LevelOrderTraversal.CompleteBinaryTree_PruneBefore_20",
            "value": 169599721.57777777,
            "unit": "ns",
            "range": "± 1485466.2604154092"
          },
          {
            "name": "Arborist.Benchmarks.LevelOrderTraversal.CompleteBinaryTree_PruneAfter_19",
            "value": 83789379.10714285,
            "unit": "ns",
            "range": "± 134474.77557991145"
          },
          {
            "name": "Arborist.Benchmarks.PostOrderTraversal.DeepTree",
            "value": 32970921.3875,
            "unit": "ns",
            "range": "± 80521.78620973558"
          },
          {
            "name": "Arborist.Benchmarks.PostOrderTraversal.TriangleTree_PruneAfter_1447",
            "value": 35344879.14666667,
            "unit": "ns",
            "range": "± 196660.39393777467"
          },
          {
            "name": "Arborist.Benchmarks.PostOrderTraversal.CompleteBinaryTree_PruneBefore_20",
            "value": 108135891.44615382,
            "unit": "ns",
            "range": "± 334698.9475543762"
          },
          {
            "name": "Arborist.Benchmarks.PostOrderTraversal.CompleteBinaryTree_PruneAfter_19",
            "value": 40921416.55128205,
            "unit": "ns",
            "range": "± 53428.04577954547"
          },
          {
            "name": "Arborist.Benchmarks.PreorderTraversal.DeepTree",
            "value": 21057160.59375,
            "unit": "ns",
            "range": "± 34608.519661232654"
          },
          {
            "name": "Arborist.Benchmarks.PreorderTraversal.TriangleTree_PruneAfter_1447",
            "value": 30839529.014583334,
            "unit": "ns",
            "range": "± 57024.76647952244"
          },
          {
            "name": "Arborist.Benchmarks.PreorderTraversal.CompleteBinaryTree_PruneBefore_20",
            "value": 103087753.7,
            "unit": "ns",
            "range": "± 459122.1770294921"
          },
          {
            "name": "Arborist.Benchmarks.PreorderTraversal.CompleteBinaryTree_PruneAfterDepth_19",
            "value": 33984610.51555555,
            "unit": "ns",
            "range": "± 65027.69047797112"
          }
        ]
      },
      {
        "commit": {
          "author": {
            "email": "jason.boyd.ce@gmail.com",
            "name": "Jason Boyd",
            "username": "jasonmcboyd"
          },
          "committer": {
            "email": "jason.boyd.ce@gmail.com",
            "name": "Jason Boyd",
            "username": "jasonmcboyd"
          },
          "distinct": true,
          "id": "7c795cd25f5b93fbac1993cf6a71485ef8c2de74",
          "message": "Add MemoryDiagnoser benchmark for Invert\n\nCovers TriangleTree depth-1448 (~1M) and a 1M-deep DegenerateTree, tagged LINQ for the dashboard. Completes benchmark coverage of the now-flat transform/aggregation ops (Materialize, LeaffixScan, LeaffixAggregate, Invert).\n\nCo-Authored-By: Claude Opus 4.8 (1M context) <noreply@anthropic.com>\nClaude-Session: https://claude.ai/code/session_01Wg3xArL4FATQaXQMBvhXdg",
          "timestamp": "2026-06-23T22:22:47Z",
          "tree_id": "547984a2584ebd974bf3f4a1790b68fc9b328cfc",
          "url": "https://github.com/jasonmcboyd/Arborist/commit/7c795cd25f5b93fbac1993cf6a71485ef8c2de74"
        },
        "date": 1782256078084,
        "tool": "benchmarkdotnet",
        "benches": [
          {
            "name": "Arborist.Benchmarks.BreadthFirstTreenumerator.TriangleTree_2896",
            "value": 291495011.35714287,
            "unit": "ns",
            "range": "± 1067125.8001722058"
          },
          {
            "name": "Arborist.Benchmarks.BreadthFirstTreenumerator.CompleteBinaryTree_21",
            "value": 330402836.1,
            "unit": "ns",
            "range": "± 4188847.9498712253"
          },
          {
            "name": "Arborist.Benchmarks.BreadthFirstTreenumerator.TrivialForest_4M",
            "value": 15867920.901785715,
            "unit": "ns",
            "range": "± 91308.18040693719"
          },
          {
            "name": "Arborist.Benchmarks.BreadthFirstTreenumerator.DegenerateTree_4M",
            "value": 108283896.73846152,
            "unit": "ns",
            "range": "± 201275.78452472237"
          },
          {
            "name": "Arborist.Benchmarks.DepthFirstTreenumerator.TriangleTree_2896",
            "value": 234825983.6666667,
            "unit": "ns",
            "range": "± 1074839.3170083312"
          },
          {
            "name": "Arborist.Benchmarks.DepthFirstTreenumerator.CompleteBinaryTree_21",
            "value": 248016743.05555555,
            "unit": "ns",
            "range": "± 4757453.750627021"
          },
          {
            "name": "Arborist.Benchmarks.DepthFirstTreenumerator.TrivialForest_4M",
            "value": 15790453.728365384,
            "unit": "ns",
            "range": "± 22888.203949514427"
          },
          {
            "name": "Arborist.Benchmarks.DepthFirstTreenumerator.DegenerateTree_4M",
            "value": 62434195.625,
            "unit": "ns",
            "range": "± 94006.48551420345"
          },
          {
            "name": "Arborist.Benchmarks.LevelOrderTraversal.DeepTree",
            "value": 57762220.37777778,
            "unit": "ns",
            "range": "± 196620.57011502679"
          },
          {
            "name": "Arborist.Benchmarks.LevelOrderTraversal.TriangleTree_PruneAfter_1447",
            "value": 84195067.5,
            "unit": "ns",
            "range": "± 57851.56323362437"
          },
          {
            "name": "Arborist.Benchmarks.LevelOrderTraversal.CompleteBinaryTree_PruneBefore_20",
            "value": 191455690.4358974,
            "unit": "ns",
            "range": "± 1015119.4878448008"
          },
          {
            "name": "Arborist.Benchmarks.LevelOrderTraversal.CompleteBinaryTree_PruneAfter_19",
            "value": 90263625.42857143,
            "unit": "ns",
            "range": "± 202193.14862767656"
          },
          {
            "name": "Arborist.Benchmarks.PostOrderTraversal.DeepTree",
            "value": 37591709.442857146,
            "unit": "ns",
            "range": "± 150916.96402513472"
          },
          {
            "name": "Arborist.Benchmarks.PostOrderTraversal.TriangleTree_PruneAfter_1447",
            "value": 40745255.88757397,
            "unit": "ns",
            "range": "± 63132.40039122792"
          },
          {
            "name": "Arborist.Benchmarks.PostOrderTraversal.CompleteBinaryTree_PruneBefore_20",
            "value": 127935661.89285715,
            "unit": "ns",
            "range": "± 1691160.1475769577"
          },
          {
            "name": "Arborist.Benchmarks.PostOrderTraversal.CompleteBinaryTree_PruneAfter_19",
            "value": 47893110.25874126,
            "unit": "ns",
            "range": "± 428160.25348819536"
          },
          {
            "name": "Arborist.Benchmarks.PreorderTraversal.DeepTree",
            "value": 24002339.08482143,
            "unit": "ns",
            "range": "± 145841.70756866015"
          },
          {
            "name": "Arborist.Benchmarks.PreorderTraversal.TriangleTree_PruneAfter_1447",
            "value": 35728918.329670325,
            "unit": "ns",
            "range": "± 47256.415737630814"
          },
          {
            "name": "Arborist.Benchmarks.PreorderTraversal.CompleteBinaryTree_PruneBefore_20",
            "value": 120457791.90666668,
            "unit": "ns",
            "range": "± 609989.3094869129"
          },
          {
            "name": "Arborist.Benchmarks.PreorderTraversal.CompleteBinaryTree_PruneAfterDepth_19",
            "value": 41706462.64285715,
            "unit": "ns",
            "range": "± 142143.84660282405"
          }
        ]
      },
      {
        "commit": {
          "author": {
            "email": "jason.boyd.ce@gmail.com",
            "name": "Jason Boyd",
            "username": "jasonmcboyd"
          },
          "committer": {
            "email": "jason.boyd.ce@gmail.com",
            "name": "Jason Boyd",
            "username": "jasonmcboyd"
          },
          "distinct": true,
          "id": "cf2c06864e8fdc1e6c24df1f1437f0dae1da26b0",
          "message": "Add 2-param Treenumerable base; drop redundant identity maps (map cleanup stage 1)\n\nThe sample trees whose node IS their surfaced value (TriangleTree, CollatzTree, CompleteBinaryTree, NDecrementTree, DeepTree) were each passing a redundant 'node => node' nodeToValueMap and carrying a duplicate type parameter. Add a 2-param Treenumerable<TNode, TChildEnumerator> convenience base (identity map) and migrate them to it. Public surfaces unchanged. The 3-param base + map remain for PreorderTree's genuine index->value dereference.\n\nCo-Authored-By: Claude Opus 4.8 (1M context) <noreply@anthropic.com>\nClaude-Session: https://claude.ai/code/session_01Wg3xArL4FATQaXQMBvhXdg",
          "timestamp": "2026-06-23T22:35:56Z",
          "tree_id": "005c51f9d7f00116b4e04d1cdb2a5fd0680fc86e",
          "url": "https://github.com/jasonmcboyd/Arborist/commit/cf2c06864e8fdc1e6c24df1f1437f0dae1da26b0"
        },
        "date": 1782257436839,
        "tool": "benchmarkdotnet",
        "benches": [
          {
            "name": "Arborist.Benchmarks.BreadthFirstTreenumerator.TriangleTree_2896",
            "value": 260298710.16666666,
            "unit": "ns",
            "range": "± 1437430.5443236537"
          },
          {
            "name": "Arborist.Benchmarks.BreadthFirstTreenumerator.CompleteBinaryTree_21",
            "value": 297469121.96666664,
            "unit": "ns",
            "range": "± 2069763.9877702193"
          },
          {
            "name": "Arborist.Benchmarks.BreadthFirstTreenumerator.TrivialForest_4M",
            "value": 14031879.25625,
            "unit": "ns",
            "range": "± 57197.517357675446"
          },
          {
            "name": "Arborist.Benchmarks.BreadthFirstTreenumerator.DegenerateTree_4M",
            "value": 89182595.15277776,
            "unit": "ns",
            "range": "± 186191.98548825338"
          },
          {
            "name": "Arborist.Benchmarks.DepthFirstTreenumerator.TriangleTree_2896",
            "value": 201078688.57142857,
            "unit": "ns",
            "range": "± 3241885.673320803"
          },
          {
            "name": "Arborist.Benchmarks.DepthFirstTreenumerator.CompleteBinaryTree_21",
            "value": 222089994.46666667,
            "unit": "ns",
            "range": "± 1725133.8059388106"
          },
          {
            "name": "Arborist.Benchmarks.DepthFirstTreenumerator.TrivialForest_4M",
            "value": 15673987.010416666,
            "unit": "ns",
            "range": "± 85004.92138104876"
          },
          {
            "name": "Arborist.Benchmarks.DepthFirstTreenumerator.DegenerateTree_4M",
            "value": 62817554.991071425,
            "unit": "ns",
            "range": "± 191153.0702415736"
          },
          {
            "name": "Arborist.Benchmarks.LevelOrderTraversal.DeepTree",
            "value": 53630322.20714285,
            "unit": "ns",
            "range": "± 99380.99435529317"
          },
          {
            "name": "Arborist.Benchmarks.LevelOrderTraversal.TriangleTree_PruneAfter_1447",
            "value": 77389710.2244898,
            "unit": "ns",
            "range": "± 75642.32968913403"
          },
          {
            "name": "Arborist.Benchmarks.LevelOrderTraversal.CompleteBinaryTree_PruneBefore_20",
            "value": 167870408.57142857,
            "unit": "ns",
            "range": "± 545382.3631197923"
          },
          {
            "name": "Arborist.Benchmarks.LevelOrderTraversal.CompleteBinaryTree_PruneAfter_19",
            "value": 84077363.34615384,
            "unit": "ns",
            "range": "± 110689.12133548051"
          },
          {
            "name": "Arborist.Benchmarks.PostOrderTraversal.DeepTree",
            "value": 33038139.75,
            "unit": "ns",
            "range": "± 109697.24868683069"
          },
          {
            "name": "Arborist.Benchmarks.PostOrderTraversal.TriangleTree_PruneAfter_1447",
            "value": 36485591.48095238,
            "unit": "ns",
            "range": "± 73854.42836372995"
          },
          {
            "name": "Arborist.Benchmarks.PostOrderTraversal.CompleteBinaryTree_PruneBefore_20",
            "value": 108883464.60000001,
            "unit": "ns",
            "range": "± 165786.02571152264"
          },
          {
            "name": "Arborist.Benchmarks.PostOrderTraversal.CompleteBinaryTree_PruneAfter_19",
            "value": 40455490.23076923,
            "unit": "ns",
            "range": "± 136823.7080618592"
          },
          {
            "name": "Arborist.Benchmarks.PreorderTraversal.DeepTree",
            "value": 21239709.735416666,
            "unit": "ns",
            "range": "± 204525.34485715194"
          },
          {
            "name": "Arborist.Benchmarks.PreorderTraversal.TriangleTree_PruneAfter_1447",
            "value": 31115303.25,
            "unit": "ns",
            "range": "± 62924.42809775175"
          },
          {
            "name": "Arborist.Benchmarks.PreorderTraversal.CompleteBinaryTree_PruneBefore_20",
            "value": 101656641.82666665,
            "unit": "ns",
            "range": "± 961882.6010162283"
          },
          {
            "name": "Arborist.Benchmarks.PreorderTraversal.CompleteBinaryTree_PruneAfterDepth_19",
            "value": 35207895.547619045,
            "unit": "ns",
            "range": "± 81523.0809554784"
          }
        ]
      },
      {
        "commit": {
          "author": {
            "email": "jason.boyd.ce@gmail.com",
            "name": "Jason Boyd",
            "username": "jasonmcboyd"
          },
          "committer": {
            "email": "jason.boyd.ce@gmail.com",
            "name": "Jason Boyd",
            "username": "jasonmcboyd"
          },
          "distinct": true,
          "id": "ca567e0f439d6d1f0f5c2143add771a6c13fd41c",
          "message": "Fix BFT Where O(depth) memory regression: tail-carry skip prefix\n\nf7eae61's O(N)-time prefix carry stored _PredSkipPrefix as a List<int>\nindexed by absolute inner depth, grown to the current depth on every\nscheduled node -- so a 1M-deep degenerate Where(_=>true) chain allocated\n~8.39 MB even though nothing is predicate-skipped (every entry 0).\n\nReplace it with a tail-carry: the prefix is monotonic non-decreasing in\ndepth and constant (= total skips on the path) beyond the deepest skipped\nancestor, so store only up to the deepest skip and serve reads past the\nend from a scalar tail. New _PrefixStored + _PrefixStoredCount +\n_PrefixTail; PrefixRead/PrefixWriteScheduled (no-op when value == tail, so\nzero allocation in the accepted region); PrefixAnchor truncates the stored\ncount to frontDepth-1 on front-advance.\n\nWhereAll 8.39 MB -> ~1.9 KB (O(1) in depth); WhereNone byte-identical\n(inherent O(depth) preserved). Visit stream byte-identical: validated\nagainst Where2InProcessScan (full c..i, 891k cases) + WhereTests (218/0),\nnet48 + net8.0 clean. Add WhereBreadthFirstAllocationTests as a hard\nmemory-bound regression guard (the gh-pages benchmark only soft-alerts).\n\nCo-Authored-By: Claude Opus 4.8 (1M context) <noreply@anthropic.com>\nClaude-Session: https://claude.ai/code/session_01Wg3xArL4FATQaXQMBvhXdg",
          "timestamp": "2026-06-25T18:13:44Z",
          "tree_id": "6d5c2256d2c8574537a4127f73a8de04fe0b4011",
          "url": "https://github.com/jasonmcboyd/Arborist/commit/ca567e0f439d6d1f0f5c2143add771a6c13fd41c"
        },
        "date": 1782413419115,
        "tool": "benchmarkdotnet",
        "benches": [
          {
            "name": "Arborist.Benchmarks.BreadthFirstTreenumerator.TriangleTree_2896",
            "value": 281576209.0714286,
            "unit": "ns",
            "range": "± 570638.3246913009"
          },
          {
            "name": "Arborist.Benchmarks.BreadthFirstTreenumerator.CompleteBinaryTree_21",
            "value": 308885759.85714287,
            "unit": "ns",
            "range": "± 1280558.8672271396"
          },
          {
            "name": "Arborist.Benchmarks.BreadthFirstTreenumerator.TrivialForest_4M",
            "value": 16163510.75,
            "unit": "ns",
            "range": "± 16430.950819090584"
          },
          {
            "name": "Arborist.Benchmarks.BreadthFirstTreenumerator.DegenerateTree_4M",
            "value": 104267242.7857143,
            "unit": "ns",
            "range": "± 1386836.9406393964"
          },
          {
            "name": "Arborist.Benchmarks.DepthFirstTreenumerator.TriangleTree_2896",
            "value": 214192245.28888884,
            "unit": "ns",
            "range": "± 3515564.428219205"
          },
          {
            "name": "Arborist.Benchmarks.DepthFirstTreenumerator.CompleteBinaryTree_21",
            "value": 233815871.68888894,
            "unit": "ns",
            "range": "± 2472346.0081294784"
          },
          {
            "name": "Arborist.Benchmarks.DepthFirstTreenumerator.TrivialForest_4M",
            "value": 15128837.524038462,
            "unit": "ns",
            "range": "± 58953.89767080388"
          },
          {
            "name": "Arborist.Benchmarks.DepthFirstTreenumerator.DegenerateTree_4M",
            "value": 65144943.48351649,
            "unit": "ns",
            "range": "± 300453.4519278812"
          },
          {
            "name": "Arborist.Benchmarks.LevelOrderTraversal.DeepTree",
            "value": 54755619.90714286,
            "unit": "ns",
            "range": "± 106777.01603128493"
          },
          {
            "name": "Arborist.Benchmarks.LevelOrderTraversal.TriangleTree_PruneAfter_1447",
            "value": 82892706.57692307,
            "unit": "ns",
            "range": "± 91830.44354529047"
          },
          {
            "name": "Arborist.Benchmarks.LevelOrderTraversal.CompleteBinaryTree_PruneBefore_20",
            "value": 182020912.2820513,
            "unit": "ns",
            "range": "± 838668.6322879082"
          },
          {
            "name": "Arborist.Benchmarks.LevelOrderTraversal.CompleteBinaryTree_PruneAfter_19",
            "value": 88718237.33333333,
            "unit": "ns",
            "range": "± 271188.086831166"
          },
          {
            "name": "Arborist.Benchmarks.PostOrderTraversal.DeepTree",
            "value": 34295953.1511111,
            "unit": "ns",
            "range": "± 183118.63555106628"
          },
          {
            "name": "Arborist.Benchmarks.PostOrderTraversal.TriangleTree_PruneAfter_1447",
            "value": 38745621.81428572,
            "unit": "ns",
            "range": "± 279995.7403197724"
          },
          {
            "name": "Arborist.Benchmarks.PostOrderTraversal.CompleteBinaryTree_PruneBefore_20",
            "value": 123171702.5142857,
            "unit": "ns",
            "range": "± 2065797.468994172"
          },
          {
            "name": "Arborist.Benchmarks.PostOrderTraversal.CompleteBinaryTree_PruneAfter_19",
            "value": 47443470.64242425,
            "unit": "ns",
            "range": "± 266082.10917821847"
          },
          {
            "name": "Arborist.Benchmarks.PreorderTraversal.DeepTree",
            "value": 22247011.525,
            "unit": "ns",
            "range": "± 156801.38730371752"
          },
          {
            "name": "Arborist.Benchmarks.PreorderTraversal.TriangleTree_PruneAfter_1447",
            "value": 33964884.8,
            "unit": "ns",
            "range": "± 64105.1058210221"
          },
          {
            "name": "Arborist.Benchmarks.PreorderTraversal.CompleteBinaryTree_PruneBefore_20",
            "value": 113262809.27142856,
            "unit": "ns",
            "range": "± 447026.195522051"
          },
          {
            "name": "Arborist.Benchmarks.PreorderTraversal.CompleteBinaryTree_PruneAfterDepth_19",
            "value": 39455033.01098902,
            "unit": "ns",
            "range": "± 136623.02268392176"
          }
        ]
      },
      {
        "commit": {
          "author": {
            "email": "jason.boyd.ce@gmail.com",
            "name": "Jason Boyd",
            "username": "jasonmcboyd"
          },
          "committer": {
            "email": "jason.boyd.ce@gmail.com",
            "name": "Jason Boyd",
            "username": "jasonmcboyd"
          },
          "distinct": true,
          "id": "6a5d01f5e6181c1c89a0993a6360bab3a4b0bac7",
          "message": "Rewrite BreadthFirstTreenumerator with a structural visit cadence\n\nThe base BFT engine emitted parent visits reactively (when a child was\naccepted), which broke whenever a child was filtered and forced a tangle\nof deferred parent-visit bookkeeping to recover the swallowed visits.\n\nReplace it with a structural cadence: a single FIFO _VisitQueue whose\nfront is the active parent, plus a LIFO _ScheduleStack for the SkipNode\npromotion descent. A parent is visited once when it reaches the front,\nthen once after every child slot that enqueues at least one accepted\nnode -- a single bool, _CurrentSlotEnqueuedNode. Roots are scheduled\nfirst as the children of an implicit no-visit forest sentinel.\n\nThis deletes _OwesPromotedParentVisit, _HasDeferredScheduledChild,\n_DepthOfLastActedOnNode, PayOwedParentVisitAndDeferChild, and the\nOnScheduling/OnVisiting/PromoteChildren/SkipSubtree/Backtrack web in\nfavor of Advance/ApplyStrategy/SkipRemainingSiblings. The now-unused\nOwesPromotedParentVisit field comes off the shared InternalNodeVisitState\nstruct, shrinking every deque slot.\n\nNo behavior change: same (Mode, Node, VisitCount, Position) visit stream.\n\nValidation:\n- Exhaustive BFT-vs-DFT oracle, up to 6 concurrent skips x 27 trees\n- Where2InProcessScan: 891,056 Where-wrapper-vs-oracle cases (groups c..i)\n- Curated exact-order traversal + 14,759 Where/allocation tests\n- Benchmarks: allocations -12% to -14%, time within ShortRun noise\n\nCo-Authored-By: Claude Opus 4.8 (1M context) <noreply@anthropic.com>\nClaude-Session: https://claude.ai/code/session_01Wg3xArL4FATQaXQMBvhXdg",
          "timestamp": "2026-06-26T00:47:42Z",
          "tree_id": "39f01ca28e49bf3bbc704caca59e8d4bb815b334",
          "url": "https://github.com/jasonmcboyd/Arborist/commit/6a5d01f5e6181c1c89a0993a6360bab3a4b0bac7"
        },
        "date": 1782436869727,
        "tool": "benchmarkdotnet",
        "benches": [
          {
            "name": "Arborist.Benchmarks.BreadthFirstTreenumerator.TriangleTree_2896",
            "value": 283551383.1923077,
            "unit": "ns",
            "range": "± 605426.0804008383"
          },
          {
            "name": "Arborist.Benchmarks.BreadthFirstTreenumerator.CompleteBinaryTree_21",
            "value": 314366974.53333336,
            "unit": "ns",
            "range": "± 852422.9343583849"
          },
          {
            "name": "Arborist.Benchmarks.BreadthFirstTreenumerator.TrivialForest_4M",
            "value": 16144051.177884616,
            "unit": "ns",
            "range": "± 36727.519203489275"
          },
          {
            "name": "Arborist.Benchmarks.BreadthFirstTreenumerator.DegenerateTree_4M",
            "value": 104069740.45,
            "unit": "ns",
            "range": "± 1212882.4675340513"
          },
          {
            "name": "Arborist.Benchmarks.DepthFirstTreenumerator.TriangleTree_2896",
            "value": 211331341.53333333,
            "unit": "ns",
            "range": "± 2344351.241696425"
          },
          {
            "name": "Arborist.Benchmarks.DepthFirstTreenumerator.CompleteBinaryTree_21",
            "value": 232684080.63888893,
            "unit": "ns",
            "range": "± 1265544.2462752322"
          },
          {
            "name": "Arborist.Benchmarks.DepthFirstTreenumerator.TrivialForest_4M",
            "value": 14967913.714285715,
            "unit": "ns",
            "range": "± 8320.819171850424"
          },
          {
            "name": "Arborist.Benchmarks.DepthFirstTreenumerator.DegenerateTree_4M",
            "value": 63331532.8791209,
            "unit": "ns",
            "range": "± 113576.484979202"
          },
          {
            "name": "Arborist.Benchmarks.LevelOrderTraversal.DeepTree",
            "value": 56858505.47008547,
            "unit": "ns",
            "range": "± 92352.15921473617"
          },
          {
            "name": "Arborist.Benchmarks.LevelOrderTraversal.TriangleTree_PruneAfter_1447",
            "value": 79315415.15238096,
            "unit": "ns",
            "range": "± 377638.1504749211"
          },
          {
            "name": "Arborist.Benchmarks.LevelOrderTraversal.CompleteBinaryTree_PruneBefore_20",
            "value": 177882204.2916667,
            "unit": "ns",
            "range": "± 1733545.3774447504"
          },
          {
            "name": "Arborist.Benchmarks.LevelOrderTraversal.CompleteBinaryTree_PruneAfter_19",
            "value": 88861764.76923077,
            "unit": "ns",
            "range": "± 145399.09653693475"
          },
          {
            "name": "Arborist.Benchmarks.PostOrderTraversal.DeepTree",
            "value": 34486635.04102564,
            "unit": "ns",
            "range": "± 95417.44005868623"
          },
          {
            "name": "Arborist.Benchmarks.PostOrderTraversal.TriangleTree_PruneAfter_1447",
            "value": 39667644.36263736,
            "unit": "ns",
            "range": "± 86251.15911320281"
          },
          {
            "name": "Arborist.Benchmarks.PostOrderTraversal.CompleteBinaryTree_PruneBefore_20",
            "value": 121329190,
            "unit": "ns",
            "range": "± 942103.1720457197"
          },
          {
            "name": "Arborist.Benchmarks.PostOrderTraversal.CompleteBinaryTree_PruneAfter_19",
            "value": 47368469.36363637,
            "unit": "ns",
            "range": "± 161661.78161568913"
          },
          {
            "name": "Arborist.Benchmarks.PreorderTraversal.DeepTree",
            "value": 21370255.872916665,
            "unit": "ns",
            "range": "± 59606.39953265803"
          },
          {
            "name": "Arborist.Benchmarks.PreorderTraversal.TriangleTree_PruneAfter_1447",
            "value": 33982507.306666665,
            "unit": "ns",
            "range": "± 261116.43422662452"
          },
          {
            "name": "Arborist.Benchmarks.PreorderTraversal.CompleteBinaryTree_PruneBefore_20",
            "value": 112668334.60000001,
            "unit": "ns",
            "range": "± 388614.44296563434"
          },
          {
            "name": "Arborist.Benchmarks.PreorderTraversal.CompleteBinaryTree_PruneAfterDepth_19",
            "value": 39709859.28402367,
            "unit": "ns",
            "range": "± 141280.80044112343"
          }
        ]
      },
      {
        "commit": {
          "author": {
            "email": "jason.boyd.ce@gmail.com",
            "name": "Jason Boyd",
            "username": "jasonmcboyd"
          },
          "committer": {
            "email": "jason.boyd.ce@gmail.com",
            "name": "Jason Boyd",
            "username": "jasonmcboyd"
          },
          "distinct": true,
          "id": "ce22f8e1055cf2b7bf6486f1da78d2058d8c69aa",
          "message": "Cap RefSemiDeque partition size to bound LOH allocation and overshoot\n\nGeometric partition growth sized each new partition to the running total\nCapacity, so the largest partition reached ~half the deque's peak element\ncount -- a multi-MB Large Object Heap allocation on wide/deep trees, plus\nup to ~2x peak over-allocation at power-of-two boundaries. Cap partition\nsize at 4096 elements (Math.Min(Capacity, MaxPartitionSize)) to bound both.\n\nBFT CompleteBinaryTree_21 (peak frontier ~2^21, the worst-case boundary):\n96 MB -> 48 MB allocated per traversal, throughput unchanged.\n\nFixed element count rather than a byte budget that would force partitions\nsub-LOH: forcing a 64 B node's partitions sub-LOH measured ~40% slower with\n~7x the Gen0 collections, because large long-lived blocks belong on the LOH.\n\nAdd RefSemiDeque regression tests crossing the cap (heterogeneous-partition\nordering, out-of-order recycling, GetFromBack/RemoveLast across boundaries)\nand an Add_Block64_1M benchmark. Remove unused InitialCapacity.\n\nCo-Authored-By: Claude Opus 4.8 (1M context) <noreply@anthropic.com>",
          "timestamp": "2026-06-26T04:13:38Z",
          "tree_id": "e35c9314063b1f6eda6dfccd1d7349907af73e32",
          "url": "https://github.com/jasonmcboyd/Arborist/commit/ce22f8e1055cf2b7bf6486f1da78d2058d8c69aa"
        },
        "date": 1782449420098,
        "tool": "benchmarkdotnet",
        "benches": [
          {
            "name": "Arborist.Benchmarks.BreadthFirstTreenumerator.TriangleTree_2896",
            "value": 283196955.0769231,
            "unit": "ns",
            "range": "± 786006.3353943105"
          },
          {
            "name": "Arborist.Benchmarks.BreadthFirstTreenumerator.CompleteBinaryTree_21",
            "value": 320795052.46666664,
            "unit": "ns",
            "range": "± 2187860.3361303178"
          },
          {
            "name": "Arborist.Benchmarks.BreadthFirstTreenumerator.TrivialForest_4M",
            "value": 16168190.19110577,
            "unit": "ns",
            "range": "± 19110.074569129378"
          },
          {
            "name": "Arborist.Benchmarks.BreadthFirstTreenumerator.DegenerateTree_4M",
            "value": 99606223.78461538,
            "unit": "ns",
            "range": "± 96931.54983112088"
          },
          {
            "name": "Arborist.Benchmarks.DepthFirstTreenumerator.TriangleTree_2896",
            "value": 254822459.57142857,
            "unit": "ns",
            "range": "± 413905.7068218398"
          },
          {
            "name": "Arborist.Benchmarks.DepthFirstTreenumerator.CompleteBinaryTree_21",
            "value": 239046055.95555553,
            "unit": "ns",
            "range": "± 2871622.1791101955"
          },
          {
            "name": "Arborist.Benchmarks.DepthFirstTreenumerator.TrivialForest_4M",
            "value": 14977622.498798076,
            "unit": "ns",
            "range": "± 19969.099087375682"
          },
          {
            "name": "Arborist.Benchmarks.DepthFirstTreenumerator.DegenerateTree_4M",
            "value": 64022183.67032966,
            "unit": "ns",
            "range": "± 113193.06326585745"
          },
          {
            "name": "Arborist.Benchmarks.LevelOrderTraversal.DeepTree",
            "value": 57610800.99999999,
            "unit": "ns",
            "range": "± 256322.94529520965"
          },
          {
            "name": "Arborist.Benchmarks.LevelOrderTraversal.TriangleTree_PruneAfter_1447",
            "value": 75025814.42857142,
            "unit": "ns",
            "range": "± 176267.7739034788"
          },
          {
            "name": "Arborist.Benchmarks.LevelOrderTraversal.CompleteBinaryTree_PruneBefore_20",
            "value": 181902156.1777778,
            "unit": "ns",
            "range": "± 973406.3721587104"
          },
          {
            "name": "Arborist.Benchmarks.LevelOrderTraversal.CompleteBinaryTree_PruneAfter_19",
            "value": 88295311.70238094,
            "unit": "ns",
            "range": "± 172755.6593178673"
          },
          {
            "name": "Arborist.Benchmarks.PostOrderTraversal.DeepTree",
            "value": 32731945.239583332,
            "unit": "ns",
            "range": "± 26529.454560391903"
          },
          {
            "name": "Arborist.Benchmarks.PostOrderTraversal.TriangleTree_PruneAfter_1447",
            "value": 38255216.698979594,
            "unit": "ns",
            "range": "± 80304.43853513255"
          },
          {
            "name": "Arborist.Benchmarks.PostOrderTraversal.CompleteBinaryTree_PruneBefore_20",
            "value": 124526860.07142857,
            "unit": "ns",
            "range": "± 597511.2012556143"
          },
          {
            "name": "Arborist.Benchmarks.PostOrderTraversal.CompleteBinaryTree_PruneAfter_19",
            "value": 47613218.76223775,
            "unit": "ns",
            "range": "± 84975.13639009013"
          },
          {
            "name": "Arborist.Benchmarks.PreorderTraversal.DeepTree",
            "value": 22673109.21875,
            "unit": "ns",
            "range": "± 23071.171503741258"
          },
          {
            "name": "Arborist.Benchmarks.PreorderTraversal.TriangleTree_PruneAfter_1447",
            "value": 32619792.114583332,
            "unit": "ns",
            "range": "± 40726.6100165596"
          },
          {
            "name": "Arborist.Benchmarks.PreorderTraversal.CompleteBinaryTree_PruneBefore_20",
            "value": 112932102.53333335,
            "unit": "ns",
            "range": "± 118125.77606787873"
          },
          {
            "name": "Arborist.Benchmarks.PreorderTraversal.CompleteBinaryTree_PruneAfterDepth_19",
            "value": 39668995.80512821,
            "unit": "ns",
            "range": "± 110103.32245317588"
          }
        ]
      },
      {
        "commit": {
          "author": {
            "email": "jason.boyd.ce@gmail.com",
            "name": "Jason Boyd",
            "username": "jasonmcboyd"
          },
          "committer": {
            "email": "jason.boyd.ce@gmail.com",
            "name": "Jason Boyd",
            "username": "jasonmcboyd"
          },
          "distinct": true,
          "id": "81b46c42702e71ad313d08c636eb3e3c3c35b140",
          "message": "Cohere BreadthFirstTreenumerator's four deques into one frame stack\n\nThe BFT kept each node's visit state and its child enumerator in FOUR parallel\nRefSemiDeques -- a state deque and an enumerator deque for each of the visit\nqueue and the schedule stack -- relying on keeping each pair in lockstep.\n\nFold each pair into one RefSemiDeque<Frame>, where Frame bundles\n{Node, Position, VisitCount, ChildEnumerator}, driven only by ref so the\nenumerator is never copied. Accepting a node becomes a single whole-frame move\nfrom the schedule stack to the visit queue, which structurally prevents a node\nand its enumerator from desynchronizing. The algorithm and visit cadence are\nunchanged. Mirrors the depth-first engine's frame stack, and removes the\nnow-unreferenced shared InternalNodeVisitState struct.\n\nNo behavior change: same (Mode, Node, VisitCount, Position) visit stream;\ndisposal (including the deliberate idempotent double-dispose on\nSkipDescendants/SkipSiblings) is at exact parity with the original.\n\nValidation:\n- Exact-order traversal + exhaustive DFT-vs-BFT multiset scan (438/0)\n- Full Arborist.Linq suite incl. Where (14,759/0)\n- Benchmarks (Release/Job.Default, clean tree): TriangleTree 289->255ms (-12%),\n  CompleteBinaryTree 337->281ms (-16%), TrivialForest/DegenerateTree 4M within\n  noise; allocation neutral, still zero per-node heap allocation. Timing\n  variance also dropped (StdDev roughly halved on the dense trees).\n\nCo-Authored-By: Claude Opus 4.8 (1M context) <noreply@anthropic.com>\nClaude-Session: https://claude.ai/code/session_01Wg3xArL4FATQaXQMBvhXdg",
          "timestamp": "2026-06-26T21:37:21Z",
          "tree_id": "05aa22f3461078c05e96518d52da4d56655c250f",
          "url": "https://github.com/jasonmcboyd/Arborist/commit/81b46c42702e71ad313d08c636eb3e3c3c35b140"
        },
        "date": 1782511425434,
        "tool": "benchmarkdotnet",
        "benches": [
          {
            "name": "Arborist.Benchmarks.BreadthFirstTreenumerator.TriangleTree_2896",
            "value": 239395657.2857143,
            "unit": "ns",
            "range": "± 2489599.1211660025"
          },
          {
            "name": "Arborist.Benchmarks.BreadthFirstTreenumerator.CompleteBinaryTree_21",
            "value": 280732521.6785714,
            "unit": "ns",
            "range": "± 570250.3842647546"
          },
          {
            "name": "Arborist.Benchmarks.BreadthFirstTreenumerator.TrivialForest_4M",
            "value": 14980734.104166666,
            "unit": "ns",
            "range": "± 22820.38420321254"
          },
          {
            "name": "Arborist.Benchmarks.BreadthFirstTreenumerator.DegenerateTree_4M",
            "value": 105195217.53846152,
            "unit": "ns",
            "range": "± 869199.2722229767"
          },
          {
            "name": "Arborist.Benchmarks.DepthFirstTreenumerator.TriangleTree_2896",
            "value": 197845387.99999997,
            "unit": "ns",
            "range": "± 2929857.231038568"
          },
          {
            "name": "Arborist.Benchmarks.DepthFirstTreenumerator.CompleteBinaryTree_21",
            "value": 226729143.88888887,
            "unit": "ns",
            "range": "± 1622879.1218307083"
          },
          {
            "name": "Arborist.Benchmarks.DepthFirstTreenumerator.TrivialForest_4M",
            "value": 14991622.33482143,
            "unit": "ns",
            "range": "± 32977.27353500662"
          },
          {
            "name": "Arborist.Benchmarks.DepthFirstTreenumerator.DegenerateTree_4M",
            "value": 66925023.120879106,
            "unit": "ns",
            "range": "± 185758.69977336883"
          },
          {
            "name": "Arborist.Benchmarks.LevelOrderTraversal.DeepTree",
            "value": 45260576.95804196,
            "unit": "ns",
            "range": "± 72077.25250800587"
          },
          {
            "name": "Arborist.Benchmarks.LevelOrderTraversal.TriangleTree_PruneAfter_1447",
            "value": 68198333.78333333,
            "unit": "ns",
            "range": "± 111983.90577035448"
          },
          {
            "name": "Arborist.Benchmarks.LevelOrderTraversal.CompleteBinaryTree_PruneBefore_20",
            "value": 164837264.51785713,
            "unit": "ns",
            "range": "± 482011.07122248027"
          },
          {
            "name": "Arborist.Benchmarks.LevelOrderTraversal.CompleteBinaryTree_PruneAfter_19",
            "value": 76189569.99047619,
            "unit": "ns",
            "range": "± 293437.1949964932"
          },
          {
            "name": "Arborist.Benchmarks.PostOrderTraversal.DeepTree",
            "value": 36840534.15238095,
            "unit": "ns",
            "range": "± 630474.6830943131"
          },
          {
            "name": "Arborist.Benchmarks.PostOrderTraversal.TriangleTree_PruneAfter_1447",
            "value": 35112342.906666666,
            "unit": "ns",
            "range": "± 149706.61511853358"
          },
          {
            "name": "Arborist.Benchmarks.PostOrderTraversal.CompleteBinaryTree_PruneBefore_20",
            "value": 115356357.67142856,
            "unit": "ns",
            "range": "± 465874.63118344685"
          },
          {
            "name": "Arborist.Benchmarks.PostOrderTraversal.CompleteBinaryTree_PruneAfter_19",
            "value": 45626106.86013986,
            "unit": "ns",
            "range": "± 153338.28812489702"
          },
          {
            "name": "Arborist.Benchmarks.PreorderTraversal.DeepTree",
            "value": 22544923.066964287,
            "unit": "ns",
            "range": "± 463061.52534142655"
          },
          {
            "name": "Arborist.Benchmarks.PreorderTraversal.TriangleTree_PruneAfter_1447",
            "value": 29205110.883333333,
            "unit": "ns",
            "range": "± 53428.9044223257"
          },
          {
            "name": "Arborist.Benchmarks.PreorderTraversal.CompleteBinaryTree_PruneBefore_20",
            "value": 106561026.66666667,
            "unit": "ns",
            "range": "± 820111.0344900138"
          },
          {
            "name": "Arborist.Benchmarks.PreorderTraversal.CompleteBinaryTree_PruneAfterDepth_19",
            "value": 37436644.43877552,
            "unit": "ns",
            "range": "± 51753.536123196616"
          }
        ]
      },
      {
        "commit": {
          "author": {
            "email": "jason.boyd.ce@gmail.com",
            "name": "Jason Boyd",
            "username": "jasonmcboyd"
          },
          "committer": {
            "email": "jason.boyd.ce@gmail.com",
            "name": "Jason Boyd",
            "username": "jasonmcboyd"
          },
          "distinct": true,
          "id": "ab94b3983058299be15f1b4d38b05b21509cb8ad",
          "message": "Encapsulate BFT state in BreadthFirstPath; mirror the DFT driver/state split\n\nMove the breadth-first engine's state -- the visit queue, the schedule stack,\nthe owed-return-visit carry, and the root sibling counter -- into a new\nBreadthFirstPath, leaving BreadthFirstTreenumerator a thin driver, mirroring the\ndepth-first DepthFirstPath split. The cohesive Frame (visit state + child\nenumerator) is kept -- the BFT keeps full state for every resident node anyway,\nso it costs no memory -- so allocation is unchanged from the cohesion engine.\n\nLike DepthFirstPath, BreadthFirstPath is \"sans-I/O\": it never pulls a child; it\nexposes the two active enumerators (the schedule-stack top and the visit-queue\nfront) by ref for the driver to advance. That isolates the engine's asynchronous\noperations to those seams, so a future async BFT can share this class and differ\nonly there. It is a mutable struct embedded as the driver's single _Path field\n(never copied; refs it returns point into the heap deques), keeping dense\ntraversal at the cohesion engine's speed. The two child-pull sites collapse into\none TryScheduleNextChildOf(ref parent).\n\nNo behavior change: same (Mode, Node, VisitCount, Position) visit stream.\n\nValidation:\n- Exact-order traversal + exhaustive DFT-vs-BFT multiset scan (438/0)\n- Full Arborist.Linq suite incl. Where (14,759/0)\n- Benchmarks (Release/Job.Default, same session): TriangleTree 194.5 vs cohesion\n  193.9 ms; CompleteBinaryTree 208.1 vs 213.0 ms (parity). Allocation identical\n  to the cohesion engine (same Frame).\n\nCo-Authored-By: Claude Opus 4.8 (1M context) <noreply@anthropic.com>\nClaude-Session: https://claude.ai/code/session_01Wg3xArL4FATQaXQMBvhXdg",
          "timestamp": "2026-06-27T00:09:54Z",
          "tree_id": "8703f7198030df75b83a7026b75dc74f78b17190",
          "url": "https://github.com/jasonmcboyd/Arborist/commit/ab94b3983058299be15f1b4d38b05b21509cb8ad"
        },
        "date": 1782521633932,
        "tool": "benchmarkdotnet",
        "benches": [
          {
            "name": "Arborist.Benchmarks.BreadthFirstTreenumerator.TriangleTree_2896",
            "value": 241693677.7142857,
            "unit": "ns",
            "range": "± 799152.9614224165"
          },
          {
            "name": "Arborist.Benchmarks.BreadthFirstTreenumerator.CompleteBinaryTree_21",
            "value": 277165730.8333333,
            "unit": "ns",
            "range": "± 811370.9752618021"
          },
          {
            "name": "Arborist.Benchmarks.BreadthFirstTreenumerator.TrivialForest_4M",
            "value": 14027810.334134616,
            "unit": "ns",
            "range": "± 25310.360737917064"
          },
          {
            "name": "Arborist.Benchmarks.BreadthFirstTreenumerator.DegenerateTree_4M",
            "value": 99655476.9076923,
            "unit": "ns",
            "range": "± 69641.38074203911"
          },
          {
            "name": "Arborist.Benchmarks.DepthFirstTreenumerator.TriangleTree_2896",
            "value": 217758471.5333333,
            "unit": "ns",
            "range": "± 2283178.922581809"
          },
          {
            "name": "Arborist.Benchmarks.DepthFirstTreenumerator.CompleteBinaryTree_21",
            "value": 235309379.0444444,
            "unit": "ns",
            "range": "± 4330093.607033592"
          },
          {
            "name": "Arborist.Benchmarks.DepthFirstTreenumerator.TrivialForest_4M",
            "value": 14005912.260416666,
            "unit": "ns",
            "range": "± 25221.33095757315"
          },
          {
            "name": "Arborist.Benchmarks.DepthFirstTreenumerator.DegenerateTree_4M",
            "value": 61448815.30612244,
            "unit": "ns",
            "range": "± 214498.93192710195"
          },
          {
            "name": "Arborist.Benchmarks.LevelOrderTraversal.DeepTree",
            "value": 46666440.779220775,
            "unit": "ns",
            "range": "± 246098.29546213336"
          },
          {
            "name": "Arborist.Benchmarks.LevelOrderTraversal.TriangleTree_PruneAfter_1447",
            "value": 63987492.928571425,
            "unit": "ns",
            "range": "± 375466.2194790001"
          },
          {
            "name": "Arborist.Benchmarks.LevelOrderTraversal.CompleteBinaryTree_PruneBefore_20",
            "value": 162756564.88333333,
            "unit": "ns",
            "range": "± 1117206.9744340838"
          },
          {
            "name": "Arborist.Benchmarks.LevelOrderTraversal.CompleteBinaryTree_PruneAfter_19",
            "value": 82021045.16666666,
            "unit": "ns",
            "range": "± 280625.6186827892"
          },
          {
            "name": "Arborist.Benchmarks.PostOrderTraversal.DeepTree",
            "value": 37305640.1952381,
            "unit": "ns",
            "range": "± 218797.1754314641"
          },
          {
            "name": "Arborist.Benchmarks.PostOrderTraversal.TriangleTree_PruneAfter_1447",
            "value": 67197136.51666667,
            "unit": "ns",
            "range": "± 237351.04852010077"
          },
          {
            "name": "Arborist.Benchmarks.PostOrderTraversal.CompleteBinaryTree_PruneBefore_20",
            "value": 124515252.05714285,
            "unit": "ns",
            "range": "± 473117.0045216465"
          },
          {
            "name": "Arborist.Benchmarks.PostOrderTraversal.CompleteBinaryTree_PruneAfter_19",
            "value": 47361694.72077923,
            "unit": "ns",
            "range": "± 92512.17320882069"
          },
          {
            "name": "Arborist.Benchmarks.PreorderTraversal.DeepTree",
            "value": 21105357.485576924,
            "unit": "ns",
            "range": "± 32214.11462973749"
          },
          {
            "name": "Arborist.Benchmarks.PreorderTraversal.TriangleTree_PruneAfter_1447",
            "value": 75741554.26495726,
            "unit": "ns",
            "range": "± 897074.4881240202"
          },
          {
            "name": "Arborist.Benchmarks.PreorderTraversal.CompleteBinaryTree_PruneBefore_20",
            "value": 114571556.41333334,
            "unit": "ns",
            "range": "± 129371.37947814059"
          },
          {
            "name": "Arborist.Benchmarks.PreorderTraversal.CompleteBinaryTree_PruneAfterDepth_19",
            "value": 38859173.71005917,
            "unit": "ns",
            "range": "± 78171.76447355872"
          }
        ]
      },
      {
        "commit": {
          "author": {
            "email": "jason.boyd.ce@gmail.com",
            "name": "Jason Boyd",
            "username": "jasonmcboyd"
          },
          "committer": {
            "email": "jason.boyd.ce@gmail.com",
            "name": "Jason Boyd",
            "username": "jasonmcboyd"
          },
          "distinct": true,
          "id": "6ebd5d0e1d60592672f966eaa4ab81a302c56999",
          "message": "Fix DFT skip-heavy regression: keep TryPushNextChild out-of-line\n\nThe encapsulated DepthFirstPath DFT (14f8393) ran ~1.7-1.9x slower than the\noriginal two-stack on promotion-heavy skip traversal (SkipAllNodes / Preorder /\nPostorder on wide trees), despite identical allocation. JIT disassembly\n(DOTNET_JitDisasm) pinned the cause: [AggressiveInlining] on TryPushNextChild\ninlined the entire promote body (pull + push) into OnMoveNext/OnScheduling,\ninflating their frames to 6 callee-saved registers + sub rsp,72 + vzeroupper and\ndowngrading OnMoveNext's branch dispatch from a tail-jmp to a call+teardown paid\non EVERY node. The original two-stack stayed fast precisely because its promote\nwas a separate, out-of-line method.\n\nMark TryPushNextChild [NoInlining] so the drivers stay thin tail-dispatchers,\nwhile keeping the push chain (PushChild/PushLevel) force-inlined INTO it so the\npush itself is still call-free. Also fold Backtrack's pop + three predicate\nchecks into one DepthFirstPath.PopFinishedLevelAndClassify call: the original\ninline-predicate form is O(1) per level but its repeated struct round-trips cost\n~2x on the deep-unwind path (GetLeaves.DeepTree); folding restores it.\n\nNet (Release/Job.Default, local, vs original two-stack): SkipAllNodes.Dft\n41 -> 22.6 ms, Preorder 42.8 -> 25.7, Postorder 47 -> 32.9 -- all back at the\noriginal; dense traversal and GetLeaves.DeepTree within noise; allocation\nunchanged. The sans-I/O encapsulation (path never pulls; one ref seam) is intact.\n\nValidation: 438/0 oracle (exact-order + exhaustive DFT-vs-BFT scan), 14,759/0\nfull Linq suite.\n\nCo-Authored-By: Claude Opus 4.8 (1M context) <noreply@anthropic.com>\nClaude-Session: https://claude.ai/code/session_01Wg3xArL4FATQaXQMBvhXdg",
          "timestamp": "2026-06-27T02:26:54Z",
          "tree_id": "5301780378953d2f586361d71d9e7e7b8dc1e6d3",
          "url": "https://github.com/jasonmcboyd/Arborist/commit/6ebd5d0e1d60592672f966eaa4ab81a302c56999"
        },
        "date": 1782529878019,
        "tool": "benchmarkdotnet",
        "benches": [
          {
            "name": "Arborist.Benchmarks.BreadthFirstTreenumerator.TriangleTree_2896",
            "value": 240239374.95238093,
            "unit": "ns",
            "range": "± 2623748.414118228"
          },
          {
            "name": "Arborist.Benchmarks.BreadthFirstTreenumerator.CompleteBinaryTree_21",
            "value": 276768463.34615386,
            "unit": "ns",
            "range": "± 936779.1374239088"
          },
          {
            "name": "Arborist.Benchmarks.BreadthFirstTreenumerator.TrivialForest_4M",
            "value": 14024682.209134616,
            "unit": "ns",
            "range": "± 27789.66643515848"
          },
          {
            "name": "Arborist.Benchmarks.BreadthFirstTreenumerator.DegenerateTree_4M",
            "value": 99909629.86666664,
            "unit": "ns",
            "range": "± 167341.01796865585"
          },
          {
            "name": "Arborist.Benchmarks.DepthFirstTreenumerator.TriangleTree_2896",
            "value": 220065577.9111111,
            "unit": "ns",
            "range": "± 1858755.8214716073"
          },
          {
            "name": "Arborist.Benchmarks.DepthFirstTreenumerator.CompleteBinaryTree_21",
            "value": 251505640.64285713,
            "unit": "ns",
            "range": "± 420916.46105041885"
          },
          {
            "name": "Arborist.Benchmarks.DepthFirstTreenumerator.TrivialForest_4M",
            "value": 16195034.341517856,
            "unit": "ns",
            "range": "± 26610.98627225758"
          },
          {
            "name": "Arborist.Benchmarks.DepthFirstTreenumerator.DegenerateTree_4M",
            "value": 60723211.25274725,
            "unit": "ns",
            "range": "± 89152.6157803204"
          },
          {
            "name": "Arborist.Benchmarks.LevelOrderTraversal.DeepTree",
            "value": 47115660.515151516,
            "unit": "ns",
            "range": "± 437571.72432526137"
          },
          {
            "name": "Arborist.Benchmarks.LevelOrderTraversal.TriangleTree_PruneAfter_1447",
            "value": 69981407.39285715,
            "unit": "ns",
            "range": "± 507008.52451118536"
          },
          {
            "name": "Arborist.Benchmarks.LevelOrderTraversal.CompleteBinaryTree_PruneBefore_20",
            "value": 162798136.78333333,
            "unit": "ns",
            "range": "± 856494.9646381094"
          },
          {
            "name": "Arborist.Benchmarks.LevelOrderTraversal.CompleteBinaryTree_PruneAfter_19",
            "value": 80716075.21978022,
            "unit": "ns",
            "range": "± 299278.2953411853"
          },
          {
            "name": "Arborist.Benchmarks.PostOrderTraversal.DeepTree",
            "value": 35700616.35999999,
            "unit": "ns",
            "range": "± 164433.2624946089"
          },
          {
            "name": "Arborist.Benchmarks.PostOrderTraversal.TriangleTree_PruneAfter_1447",
            "value": 41532677.898809515,
            "unit": "ns",
            "range": "± 180726.46812151946"
          },
          {
            "name": "Arborist.Benchmarks.PostOrderTraversal.CompleteBinaryTree_PruneBefore_20",
            "value": 123792328.05333334,
            "unit": "ns",
            "range": "± 875988.0693578123"
          },
          {
            "name": "Arborist.Benchmarks.PostOrderTraversal.CompleteBinaryTree_PruneAfter_19",
            "value": 47116649.04195804,
            "unit": "ns",
            "range": "± 279103.5348551134"
          },
          {
            "name": "Arborist.Benchmarks.PreorderTraversal.DeepTree",
            "value": 25647453.852083333,
            "unit": "ns",
            "range": "± 117549.23982417324"
          },
          {
            "name": "Arborist.Benchmarks.PreorderTraversal.TriangleTree_PruneAfter_1447",
            "value": 34805246.53809524,
            "unit": "ns",
            "range": "± 78209.49694365359"
          },
          {
            "name": "Arborist.Benchmarks.PreorderTraversal.CompleteBinaryTree_PruneBefore_20",
            "value": 114785801.76923077,
            "unit": "ns",
            "range": "± 287167.1037322401"
          },
          {
            "name": "Arborist.Benchmarks.PreorderTraversal.CompleteBinaryTree_PruneAfterDepth_19",
            "value": 42863407.91071428,
            "unit": "ns",
            "range": "± 65207.943875813515"
          }
        ]
      },
      {
        "commit": {
          "author": {
            "email": "jason.boyd.ce@gmail.com",
            "name": "Jason Boyd",
            "username": "jasonmcboyd"
          },
          "committer": {
            "email": "jason.boyd.ce@gmail.com",
            "name": "Jason Boyd",
            "username": "jasonmcboyd"
          },
          "distinct": true,
          "id": "e8bbc30ca40332e6f77c535d2a6e5edf419feb67",
          "message": "Revert DFT Backtrack consolidation (GetLeaves.DeepTree deep-unwind cost)\n\n6ebd5d0 fixed the wide-skip-traversal regression by making TryPushNextChild\nout-of-line, but it also folded Backtrack's pop + three predicate checks into one\nDepthFirstPath.PopFinishedLevelAndClassify call. That consolidation, fine on wide\ntrees, added one call per unwound level -- ~262K calls on the deep-unwind path --\nand regressed GetLeaves.DeepTree from ~10ms to ~24ms on the CI runner (a cost\nlocal benchmarks under-reported, due to cache differences).\n\nRevert the consolidation back to the original two-stack's inline-predicate\nBacktrack, keeping the out-of-line TryPushNextChild that fixed wide skip. The DFT\nis now structurally identical to the original two-stack (inline Backtrack +\nseparate promote method), just encapsulated in DepthFirstPath -- the form the CI\nshows is fast on every tree shape.\n\nNet (vs original two-stack, local same-session): SkipAllNodes.Dft 22.7 vs 22.0;\nGetLeaves.DeepTree 10.7 vs 8.8 (the residual +22% is the out-of-line promote's\nper-node call -- the irreducible cost of keeping wide skip fast; CI will confirm\nGetLeaves is well below 6ebd5d0's 24ms). Allocation unchanged.\n\nValidation: 438/0 oracle (exact-order + exhaustive DFT-vs-BFT scan), 14,759/0 Linq.\n\nCo-Authored-By: Claude Opus 4.8 (1M context) <noreply@anthropic.com>\nClaude-Session: https://claude.ai/code/session_01Wg3xArL4FATQaXQMBvhXdg",
          "timestamp": "2026-06-27T03:58:38Z",
          "tree_id": "a3f9dd64f7a624f48c68d521c40c1a096d4fa152",
          "url": "https://github.com/jasonmcboyd/Arborist/commit/e8bbc30ca40332e6f77c535d2a6e5edf419feb67"
        },
        "date": 1782534423839,
        "tool": "benchmarkdotnet",
        "benches": [
          {
            "name": "Arborist.Benchmarks.BreadthFirstTreenumerator.TriangleTree_2896",
            "value": 238192616.2,
            "unit": "ns",
            "range": "± 2220087.0587826823"
          },
          {
            "name": "Arborist.Benchmarks.BreadthFirstTreenumerator.CompleteBinaryTree_21",
            "value": 278697109.2307692,
            "unit": "ns",
            "range": "± 633754.7518692205"
          },
          {
            "name": "Arborist.Benchmarks.BreadthFirstTreenumerator.TrivialForest_4M",
            "value": 14975648.582589285,
            "unit": "ns",
            "range": "± 18409.599102139717"
          },
          {
            "name": "Arborist.Benchmarks.BreadthFirstTreenumerator.DegenerateTree_4M",
            "value": 102530454.98666668,
            "unit": "ns",
            "range": "± 1254961.8256552522"
          },
          {
            "name": "Arborist.Benchmarks.DepthFirstTreenumerator.TriangleTree_2896",
            "value": 221501691.6666667,
            "unit": "ns",
            "range": "± 967517.5620156847"
          },
          {
            "name": "Arborist.Benchmarks.DepthFirstTreenumerator.CompleteBinaryTree_21",
            "value": 248397144.7,
            "unit": "ns",
            "range": "± 1085301.9302791604"
          },
          {
            "name": "Arborist.Benchmarks.DepthFirstTreenumerator.TrivialForest_4M",
            "value": 16174742.127232144,
            "unit": "ns",
            "range": "± 23660.446281276825"
          },
          {
            "name": "Arborist.Benchmarks.DepthFirstTreenumerator.DegenerateTree_4M",
            "value": 62539578.31428572,
            "unit": "ns",
            "range": "± 259887.10668796548"
          },
          {
            "name": "Arborist.Benchmarks.LevelOrderTraversal.DeepTree",
            "value": 46147284.779220775,
            "unit": "ns",
            "range": "± 93109.56007193441"
          },
          {
            "name": "Arborist.Benchmarks.LevelOrderTraversal.TriangleTree_PruneAfter_1447",
            "value": 63170288.125,
            "unit": "ns",
            "range": "± 143746.8644352319"
          },
          {
            "name": "Arborist.Benchmarks.LevelOrderTraversal.CompleteBinaryTree_PruneBefore_20",
            "value": 164317135.3333333,
            "unit": "ns",
            "range": "± 798527.9483125393"
          },
          {
            "name": "Arborist.Benchmarks.LevelOrderTraversal.CompleteBinaryTree_PruneAfter_19",
            "value": 77192483.83809522,
            "unit": "ns",
            "range": "± 1088184.8982942859"
          },
          {
            "name": "Arborist.Benchmarks.PostOrderTraversal.DeepTree",
            "value": 55027359.207142845,
            "unit": "ns",
            "range": "± 325501.7578772314"
          },
          {
            "name": "Arborist.Benchmarks.PostOrderTraversal.TriangleTree_PruneAfter_1447",
            "value": 42345304.45512819,
            "unit": "ns",
            "range": "± 111239.92155635626"
          },
          {
            "name": "Arborist.Benchmarks.PostOrderTraversal.CompleteBinaryTree_PruneBefore_20",
            "value": 122263507.14285712,
            "unit": "ns",
            "range": "± 606042.3112186067"
          },
          {
            "name": "Arborist.Benchmarks.PostOrderTraversal.CompleteBinaryTree_PruneAfter_19",
            "value": 46810639.42207792,
            "unit": "ns",
            "range": "± 91400.85004301398"
          },
          {
            "name": "Arborist.Benchmarks.PreorderTraversal.DeepTree",
            "value": 25987457.070833333,
            "unit": "ns",
            "range": "± 128161.71076793433"
          },
          {
            "name": "Arborist.Benchmarks.PreorderTraversal.TriangleTree_PruneAfter_1447",
            "value": 36695726.516483516,
            "unit": "ns",
            "range": "± 50564.53172456675"
          },
          {
            "name": "Arborist.Benchmarks.PreorderTraversal.CompleteBinaryTree_PruneBefore_20",
            "value": 114193357.68571427,
            "unit": "ns",
            "range": "± 853606.18733107"
          },
          {
            "name": "Arborist.Benchmarks.PreorderTraversal.CompleteBinaryTree_PruneAfterDepth_19",
            "value": 40765133.20118343,
            "unit": "ns",
            "range": "± 107364.75976654273"
          }
        ]
      }
    ],
    "LINQ Benchmarks": [
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
        "date": 1769975445512,
        "tool": "benchmarkdotnet",
        "benches": [
          {
            "name": "Arborist.Benchmarks.BreadthFirstWhere.TriangleTree_1448",
            "value": 98298953.6,
            "unit": "ns",
            "range": "± 902791.5417448364"
          },
          {
            "name": "Arborist.Benchmarks.BreadthFirstWhere.TrivialForest_WhereAll_1M",
            "value": 41433564.35119047,
            "unit": "ns",
            "range": "± 123487.37330235617"
          },
          {
            "name": "Arborist.Benchmarks.BreadthFirstWhere.TrivialForest_WhereNone_1M",
            "value": 14413210.436458332,
            "unit": "ns",
            "range": "± 79159.84777765989"
          },
          {
            "name": "Arborist.Benchmarks.BreadthFirstWhere.DegenerateTree_WhereAll_1M",
            "value": 74289572.82417582,
            "unit": "ns",
            "range": "± 407149.5125712169"
          },
          {
            "name": "Arborist.Benchmarks.BreadthFirstWhere.DegenerateTree_WhereNone_1M",
            "value": 23539234.414583333,
            "unit": "ns",
            "range": "± 95626.72047033807"
          },
          {
            "name": "Arborist.Benchmarks.DepthFirstWhere.TriangleTree_PruneAfter_1448",
            "value": 92121389.81111111,
            "unit": "ns",
            "range": "± 262564.8494868999"
          },
          {
            "name": "Arborist.Benchmarks.DepthFirstWhere.WhereAll_TrivialForest_1M",
            "value": 37451794.82380952,
            "unit": "ns",
            "range": "± 97603.57676082359"
          },
          {
            "name": "Arborist.Benchmarks.DepthFirstWhere.WhereNone_TrivialForest_1M",
            "value": 7836431.450520833,
            "unit": "ns",
            "range": "± 9470.463397824926"
          },
          {
            "name": "Arborist.Benchmarks.DepthFirstWhere.WhereAll_DegenerateTree_1M",
            "value": 59595584.76785714,
            "unit": "ns",
            "range": "± 188416.45317377726"
          },
          {
            "name": "Arborist.Benchmarks.DepthFirstWhere.WhereNone_DegenerateTree_1M",
            "value": 17718644.338942308,
            "unit": "ns",
            "range": "± 38999.60259623632"
          },
          {
            "name": "Arborist.Benchmarks.Select.SelectComposition",
            "value": 21030227.50669643,
            "unit": "ns",
            "range": "± 57075.21412854258"
          },
          {
            "name": "Arborist.Benchmarks.PruneAfter.Bft_TrivialForest_1M",
            "value": 11731082.271875,
            "unit": "ns",
            "range": "± 35211.88220052994"
          },
          {
            "name": "Arborist.Benchmarks.PruneBefore.Bft_TrivialForest_1M",
            "value": 14308850.50669643,
            "unit": "ns",
            "range": "± 60103.112000004"
          },
          {
            "name": "Arborist.Benchmarks.PruneAfter.Dft_TrivialForest_1M",
            "value": 11729303.455208333,
            "unit": "ns",
            "range": "± 48339.92506027067"
          },
          {
            "name": "Arborist.Benchmarks.PruneBefore.Dft_TrivialForest_1M",
            "value": 8824922.261458334,
            "unit": "ns",
            "range": "± 20557.721679684506"
          },
          {
            "name": "Arborist.Benchmarks.AllNodes.Bft_CompleteBinaryTree_PruneBefore_19",
            "value": 87302757.40277778,
            "unit": "ns",
            "range": "± 227903.09925004336"
          },
          {
            "name": "Arborist.Benchmarks.AnyNodes.Bft_CompleteBinaryTree_PruneBefore_19",
            "value": 86753478.82142857,
            "unit": "ns",
            "range": "± 437298.42756610655"
          },
          {
            "name": "Arborist.Benchmarks.CountNodes.DeepTree",
            "value": 45068982.67532468,
            "unit": "ns",
            "range": "± 212773.5534514552"
          },
          {
            "name": "Arborist.Benchmarks.GetLeaves.DeepTree",
            "value": 10761770.075892856,
            "unit": "ns",
            "range": "± 22328.255605612543"
          },
          {
            "name": "Arborist.Benchmarks.AllNodes.Dft_CompleteBinaryTree_PruneBefore_19",
            "value": 67050925.302083336,
            "unit": "ns",
            "range": "± 83900.00113064893"
          },
          {
            "name": "Arborist.Benchmarks.AnyNodes.Dft_CompleteBinaryTree_PruneBefore_19",
            "value": 68128772.20535715,
            "unit": "ns",
            "range": "± 250810.04635034752"
          },
          {
            "name": "Arborist.Benchmarks.CountNodes.TriangleTree_PruneAfter_2048",
            "value": 60771434.14285715,
            "unit": "ns",
            "range": "± 289054.6800701677"
          },
          {
            "name": "Arborist.Benchmarks.GetLeaves.CompleteBinaryTree_PruneBefore_20",
            "value": 135902196.86666667,
            "unit": "ns",
            "range": "± 933324.8796193775"
          },
          {
            "name": "Arborist.Benchmarks.AllNodes.Bft_TriangleTree_PruneBefore_19",
            "value": 105356336.21333334,
            "unit": "ns",
            "range": "± 511721.1270553912"
          },
          {
            "name": "Arborist.Benchmarks.AnyNodes.Bft_TriangleTree_PruneBefore_19",
            "value": 103514948.12307693,
            "unit": "ns",
            "range": "± 548373.0861383907"
          },
          {
            "name": "Arborist.Benchmarks.CountNodes.CompleteBinaryTree_PruneAfter_20",
            "value": 73050342.97142857,
            "unit": "ns",
            "range": "± 349413.0270815737"
          },
          {
            "name": "Arborist.Benchmarks.AllNodes.Dft_TriangleTree_PruneBefore_19",
            "value": 62580325.45192308,
            "unit": "ns",
            "range": "± 252777.11053742038"
          },
          {
            "name": "Arborist.Benchmarks.AnyNodes.Dft_TriangleTree_PruneBefore_19",
            "value": 61706169.074074075,
            "unit": "ns",
            "range": "± 281904.6156307212"
          },
          {
            "name": "Arborist.Benchmarks.SkipAllNodes.Bft_TriangleTree_1448",
            "value": 29776777.88839286,
            "unit": "ns",
            "range": "± 47112.81701825619"
          },
          {
            "name": "Arborist.Benchmarks.SkipAllNodes.Dft_TriangleTree_1448",
            "value": 29200810.723214287,
            "unit": "ns",
            "range": "± 37368.565383578105"
          }
        ]
      },
      {
        "commit": {
          "author": {
            "email": "jason.boyd.ce@gmail.com",
            "name": "Jason Boyd",
            "username": "jasonmcboyd"
          },
          "committer": {
            "email": "jason.boyd.ce@gmail.com",
            "name": "Jason Boyd",
            "username": "jasonmcboyd"
          },
          "distinct": true,
          "id": "5993e5762d3c0ffa1bbacbb40b35307cff9a3f87",
          "message": "Docs: document BFT Where O(N) prefix-carry design\n\nRewrite the WhereBreadthFirstTreenumerator section to match the implementation (queue + incremental _PredSkipPrefix, predicate-skip vs consumer-SkipNode, the real parent-visit fields, front-anchored sibling index). Update the DFT-vs-BFT table (BFT now O(N)) and fix stale _ExtraParentVisitsEmitted/_LastRemovedSkippedNodePosition/_SkippedStack references.\n\nCo-Authored-By: Claude Opus 4.8 (1M context) <noreply@anthropic.com>\nClaude-Session: https://claude.ai/code/session_01Wg3xArL4FATQaXQMBvhXdg",
          "timestamp": "2026-06-23T04:33:54Z",
          "tree_id": "a2615b3ce8e91df5baffd5f8f08e3c6ac9df2e3b",
          "url": "https://github.com/jasonmcboyd/Arborist/commit/5993e5762d3c0ffa1bbacbb40b35307cff9a3f87"
        },
        "date": 1782190984204,
        "tool": "benchmarkdotnet",
        "benches": [
          {
            "name": "Arborist.Benchmarks.BreadthFirstWhere.TriangleTree_1448",
            "value": 93585533.05555557,
            "unit": "ns",
            "range": "± 216931.25105783335"
          },
          {
            "name": "Arborist.Benchmarks.BreadthFirstWhere.TrivialForest_WhereAll_1M",
            "value": 50224486.019999996,
            "unit": "ns",
            "range": "± 111036.74162298799"
          },
          {
            "name": "Arborist.Benchmarks.BreadthFirstWhere.TrivialForest_WhereNone_1M",
            "value": 6468346.439732143,
            "unit": "ns",
            "range": "± 8854.65668964856"
          },
          {
            "name": "Arborist.Benchmarks.BreadthFirstWhere.DegenerateTree_WhereAll_1M",
            "value": 96353554.96153846,
            "unit": "ns",
            "range": "± 131667.1388916671"
          },
          {
            "name": "Arborist.Benchmarks.BreadthFirstWhere.DegenerateTree_WhereNone_1M",
            "value": 24433273.24041667,
            "unit": "ns",
            "range": "± 1220352.0549244045"
          },
          {
            "name": "Arborist.Benchmarks.DepthFirstWhere.TriangleTree_PruneAfter_1448",
            "value": 79498334.28571428,
            "unit": "ns",
            "range": "± 443226.41944558354"
          },
          {
            "name": "Arborist.Benchmarks.DepthFirstWhere.WhereAll_TrivialForest_1M",
            "value": 42126497.83783784,
            "unit": "ns",
            "range": "± 1228868.1135142029"
          },
          {
            "name": "Arborist.Benchmarks.DepthFirstWhere.WhereNone_TrivialForest_1M",
            "value": 7949477.264423077,
            "unit": "ns",
            "range": "± 12350.202188186613"
          },
          {
            "name": "Arborist.Benchmarks.DepthFirstWhere.WhereAll_DegenerateTree_1M",
            "value": 67805304.13333334,
            "unit": "ns",
            "range": "± 222881.7157010325"
          },
          {
            "name": "Arborist.Benchmarks.DepthFirstWhere.WhereNone_DegenerateTree_1M",
            "value": 17654837.91875,
            "unit": "ns",
            "range": "± 26888.552339391113"
          },
          {
            "name": "Arborist.Benchmarks.Select.SelectComposition",
            "value": 20827022.057291668,
            "unit": "ns",
            "range": "± 40898.41293426334"
          },
          {
            "name": "Arborist.Benchmarks.PruneAfter.Bft_TrivialForest_1M",
            "value": 11628721.760817308,
            "unit": "ns",
            "range": "± 47154.44561480151"
          },
          {
            "name": "Arborist.Benchmarks.PruneBefore.Bft_TrivialForest_1M",
            "value": 5773563.352120535,
            "unit": "ns",
            "range": "± 8021.172887769454"
          },
          {
            "name": "Arborist.Benchmarks.PruneAfter.Dft_TrivialForest_1M",
            "value": 11517529.423958333,
            "unit": "ns",
            "range": "± 32942.05325200394"
          },
          {
            "name": "Arborist.Benchmarks.PruneBefore.Dft_TrivialForest_1M",
            "value": 8532405.42299107,
            "unit": "ns",
            "range": "± 33896.104293612356"
          },
          {
            "name": "Arborist.Benchmarks.AllNodes.Bft_CompleteBinaryTree_PruneBefore_19",
            "value": 88097588.1025641,
            "unit": "ns",
            "range": "± 341354.6029011395"
          },
          {
            "name": "Arborist.Benchmarks.AnyNodes.Bft_CompleteBinaryTree_PruneBefore_19",
            "value": 89152733.61904761,
            "unit": "ns",
            "range": "± 355862.53136541485"
          },
          {
            "name": "Arborist.Benchmarks.CountNodes.DeepTree",
            "value": 24684092.270089287,
            "unit": "ns",
            "range": "± 55193.03306774904"
          },
          {
            "name": "Arborist.Benchmarks.GetLeaves.DeepTree",
            "value": 10823725.885817308,
            "unit": "ns",
            "range": "± 53637.25809527005"
          },
          {
            "name": "Arborist.Benchmarks.AllNodes.Dft_CompleteBinaryTree_PruneBefore_19",
            "value": 56534133.47863248,
            "unit": "ns",
            "range": "± 143184.34333998314"
          },
          {
            "name": "Arborist.Benchmarks.AnyNodes.Dft_CompleteBinaryTree_PruneBefore_19",
            "value": 56324979.47619046,
            "unit": "ns",
            "range": "± 132347.41014023894"
          },
          {
            "name": "Arborist.Benchmarks.CountNodes.TriangleTree_PruneAfter_2048",
            "value": 63612517.234693885,
            "unit": "ns",
            "range": "± 193695.52548725135"
          },
          {
            "name": "Arborist.Benchmarks.GetLeaves.CompleteBinaryTree_PruneBefore_20",
            "value": 114106076.0153846,
            "unit": "ns",
            "range": "± 319778.82308685826"
          },
          {
            "name": "Arborist.Benchmarks.AllNodes.Bft_TriangleTree_PruneBefore_19",
            "value": 127407325.25,
            "unit": "ns",
            "range": "± 1359339.6099337894"
          },
          {
            "name": "Arborist.Benchmarks.AnyNodes.Bft_TriangleTree_PruneBefore_19",
            "value": 126358937.33928572,
            "unit": "ns",
            "range": "± 835400.03694275"
          },
          {
            "name": "Arborist.Benchmarks.CountNodes.CompleteBinaryTree_PruneAfter_20",
            "value": 71645532.7244898,
            "unit": "ns",
            "range": "± 569315.196028669"
          },
          {
            "name": "Arborist.Benchmarks.AllNodes.Dft_TriangleTree_PruneBefore_19",
            "value": 64926138.26666668,
            "unit": "ns",
            "range": "± 464335.97682169324"
          },
          {
            "name": "Arborist.Benchmarks.AnyNodes.Dft_TriangleTree_PruneBefore_19",
            "value": 63985637.538461536,
            "unit": "ns",
            "range": "± 208642.05010735107"
          },
          {
            "name": "Arborist.Benchmarks.SkipAllNodes.Bft_TriangleTree_1448",
            "value": 31043344.029166665,
            "unit": "ns",
            "range": "± 297704.9147379336"
          },
          {
            "name": "Arborist.Benchmarks.SkipAllNodes.Dft_TriangleTree_1448",
            "value": 29869294.725,
            "unit": "ns",
            "range": "± 145506.8763303826"
          }
        ]
      },
      {
        "commit": {
          "author": {
            "email": "jason.boyd.ce@gmail.com",
            "name": "Jason Boyd",
            "username": "jasonmcboyd"
          },
          "committer": {
            "email": "jason.boyd.ce@gmail.com",
            "name": "Jason Boyd",
            "username": "jasonmcboyd"
          },
          "distinct": true,
          "id": "c3ab5ef05fc7c7304bb98cd14e97bed3a8f91e62",
          "message": "Align LeaffixAggregate with LeaffixScan (flat, lazy, zero per-node alloc)\n\nLeaffixAggregate now uses the same flat forward DFS + no-copy ChildAccumulations view as LeaffixScan, yielding each root's accumulated value. Accumulator changes TAccumulate[] -> ChildAccumulations<TAccumulate>. Per-root lazy: a root is emitted the moment its subtree completes and the buffers are reused for the next root, so peak memory is the largest root subtree (not the whole forest) and early-terminating consumers traverse fewer roots -- matching the previous LeaffixAggregator behavior. Zero per-node heap allocation. Adds LeaffixAggregateTests (it previously had none and no callers).\n\nLeaffixAggregator is retained -- still used by Invert (TreeInverter); it'll be removed when Invert is redesigned onto a flat structure.\n\nCo-Authored-By: Claude Opus 4.8 (1M context) <noreply@anthropic.com>\nClaude-Session: https://claude.ai/code/session_01Wg3xArL4FATQaXQMBvhXdg",
          "timestamp": "2026-06-23T21:57:19Z",
          "tree_id": "7dacc032f0faefb81f1e004514e835a60b5bf81a",
          "url": "https://github.com/jasonmcboyd/Arborist/commit/c3ab5ef05fc7c7304bb98cd14e97bed3a8f91e62"
        },
        "date": 1782253173714,
        "tool": "benchmarkdotnet",
        "benches": [
          {
            "name": "Arborist.Benchmarks.BreadthFirstWhere.TriangleTree_1448",
            "value": 93439462.75,
            "unit": "ns",
            "range": "± 73717.11265763489"
          },
          {
            "name": "Arborist.Benchmarks.BreadthFirstWhere.TrivialForest_WhereAll_1M",
            "value": 49930621.52142856,
            "unit": "ns",
            "range": "± 77767.07233891652"
          },
          {
            "name": "Arborist.Benchmarks.BreadthFirstWhere.TrivialForest_WhereNone_1M",
            "value": 6504292.752232143,
            "unit": "ns",
            "range": "± 9594.260260008148"
          },
          {
            "name": "Arborist.Benchmarks.BreadthFirstWhere.DegenerateTree_WhereAll_1M",
            "value": 99853071.9076923,
            "unit": "ns",
            "range": "± 202726.789117316"
          },
          {
            "name": "Arborist.Benchmarks.BreadthFirstWhere.DegenerateTree_WhereNone_1M",
            "value": 24068598.42861842,
            "unit": "ns",
            "range": "± 1372867.3638943299"
          },
          {
            "name": "Arborist.Benchmarks.DepthFirstWhere.TriangleTree_PruneAfter_1448",
            "value": 76836874.79761904,
            "unit": "ns",
            "range": "± 128166.07579522814"
          },
          {
            "name": "Arborist.Benchmarks.DepthFirstWhere.WhereAll_TrivialForest_1M",
            "value": 41850096.01111112,
            "unit": "ns",
            "range": "± 105368.65919273201"
          },
          {
            "name": "Arborist.Benchmarks.DepthFirstWhere.WhereNone_TrivialForest_1M",
            "value": 8124414.277901785,
            "unit": "ns",
            "range": "± 58748.37769635213"
          },
          {
            "name": "Arborist.Benchmarks.DepthFirstWhere.WhereAll_DegenerateTree_1M",
            "value": 69008895.51666667,
            "unit": "ns",
            "range": "± 936030.0803863492"
          },
          {
            "name": "Arborist.Benchmarks.DepthFirstWhere.WhereNone_DegenerateTree_1M",
            "value": 17798697.729166668,
            "unit": "ns",
            "range": "± 10417.061260971117"
          },
          {
            "name": "Arborist.Benchmarks.Select.SelectComposition",
            "value": 15145067.701041667,
            "unit": "ns",
            "range": "± 45366.84976996417"
          },
          {
            "name": "Arborist.Benchmarks.PruneAfter.Bft_TrivialForest_1M",
            "value": 11642672.867708333,
            "unit": "ns",
            "range": "± 57605.10177936332"
          },
          {
            "name": "Arborist.Benchmarks.PruneBefore.Bft_TrivialForest_1M",
            "value": 5949627.681490385,
            "unit": "ns",
            "range": "± 6798.743318689209"
          },
          {
            "name": "Arborist.Benchmarks.PruneAfter.Dft_TrivialForest_1M",
            "value": 11448896.9375,
            "unit": "ns",
            "range": "± 42254.23343613461"
          },
          {
            "name": "Arborist.Benchmarks.PruneBefore.Dft_TrivialForest_1M",
            "value": 8515629.336458333,
            "unit": "ns",
            "range": "± 12182.060096836904"
          },
          {
            "name": "Arborist.Benchmarks.AllNodes.Bft_CompleteBinaryTree_PruneBefore_19",
            "value": 88278951.23076923,
            "unit": "ns",
            "range": "± 397344.3531597357"
          },
          {
            "name": "Arborist.Benchmarks.AnyNodes.Bft_CompleteBinaryTree_PruneBefore_19",
            "value": 87144838.25641027,
            "unit": "ns",
            "range": "± 239824.7073533961"
          },
          {
            "name": "Arborist.Benchmarks.CountNodes.DeepTree",
            "value": 24708339.859375,
            "unit": "ns",
            "range": "± 71202.33101772606"
          },
          {
            "name": "Arborist.Benchmarks.GetLeaves.DeepTree",
            "value": 10679974.735416668,
            "unit": "ns",
            "range": "± 19046.582380899337"
          },
          {
            "name": "Arborist.Benchmarks.AllNodes.Dft_CompleteBinaryTree_PruneBefore_19",
            "value": 55966980.288888894,
            "unit": "ns",
            "range": "± 180722.99900806372"
          },
          {
            "name": "Arborist.Benchmarks.AnyNodes.Dft_CompleteBinaryTree_PruneBefore_19",
            "value": 57811390.76296296,
            "unit": "ns",
            "range": "± 1052834.4703943871"
          },
          {
            "name": "Arborist.Benchmarks.CountNodes.TriangleTree_PruneAfter_2048",
            "value": 64041786.36190476,
            "unit": "ns",
            "range": "± 433363.08994041546"
          },
          {
            "name": "Arborist.Benchmarks.GetLeaves.CompleteBinaryTree_PruneBefore_20",
            "value": 113545617.74666665,
            "unit": "ns",
            "range": "± 274404.9578198972"
          },
          {
            "name": "Arborist.Benchmarks.AllNodes.Bft_TriangleTree_PruneBefore_19",
            "value": 127668142.6,
            "unit": "ns",
            "range": "± 2247468.2090659356"
          },
          {
            "name": "Arborist.Benchmarks.AnyNodes.Bft_TriangleTree_PruneBefore_19",
            "value": 127020732.86666666,
            "unit": "ns",
            "range": "± 1845028.751764582"
          },
          {
            "name": "Arborist.Benchmarks.CountNodes.CompleteBinaryTree_PruneAfter_20",
            "value": 71811292.6,
            "unit": "ns",
            "range": "± 514190.60531258635"
          },
          {
            "name": "Arborist.Benchmarks.AllNodes.Dft_TriangleTree_PruneBefore_19",
            "value": 65832104.183673464,
            "unit": "ns",
            "range": "± 261107.35881070088"
          },
          {
            "name": "Arborist.Benchmarks.AnyNodes.Dft_TriangleTree_PruneBefore_19",
            "value": 64399515.075,
            "unit": "ns",
            "range": "± 555379.5089892177"
          },
          {
            "name": "Arborist.Benchmarks.SkipAllNodes.Bft_TriangleTree_1448",
            "value": 30763376.88169643,
            "unit": "ns",
            "range": "± 90640.17100681274"
          },
          {
            "name": "Arborist.Benchmarks.SkipAllNodes.Dft_TriangleTree_1448",
            "value": 30141570.548076924,
            "unit": "ns",
            "range": "± 34904.205928003226"
          }
        ]
      },
      {
        "commit": {
          "author": {
            "email": "jason.boyd.ce@gmail.com",
            "name": "Jason Boyd",
            "username": "jasonmcboyd"
          },
          "committer": {
            "email": "jason.boyd.ce@gmail.com",
            "name": "Jason Boyd",
            "username": "jasonmcboyd"
          },
          "distinct": true,
          "id": "96f3cc44e2208f47258c2887c2a011530d708864",
          "message": "Rewrite Invert flat; retire SimpleNode and LeaffixAggregator\n\nInvert now materializes the source into flat pre-order arrays and emits the mirror with a stack -- pushing roots/children in forward order so they pop in reverse (the mirror's pre-order). Subtree sizes are invariant under mirroring; result is a PreorderTree. O(N), zero per-node allocation (was a SimpleNode object per node via LeaffixAggregator).\n\nInvert was the last user of both SimpleNode and LeaffixAggregator, so this deletes five files: TreeInverter, LeaffixAggregator (+NodeContextAndResult), SimpleNode, SimpleNodeChildEnumerator, SimpleNodeTreenumerable. The SimpleNode retirement is complete, clearing the runway for the nodeToValueMap elimination. (A level-order/LOUDS mirror -- reverse each child-run in place -- remains a future refinement if a LevelOrderTree is ever built.)\n\nCo-Authored-By: Claude Opus 4.8 (1M context) <noreply@anthropic.com>\nClaude-Session: https://claude.ai/code/session_01Wg3xArL4FATQaXQMBvhXdg",
          "timestamp": "2026-06-23T22:21:16Z",
          "tree_id": "aafd024b9cc4740d5e6c4fe63bc7cf95158cbb5e",
          "url": "https://github.com/jasonmcboyd/Arborist/commit/96f3cc44e2208f47258c2887c2a011530d708864"
        },
        "date": 1782254668146,
        "tool": "benchmarkdotnet",
        "benches": [
          {
            "name": "Arborist.Benchmarks.BreadthFirstWhere.TriangleTree_1448",
            "value": 86013233.73809524,
            "unit": "ns",
            "range": "± 30285.31622480301"
          },
          {
            "name": "Arborist.Benchmarks.BreadthFirstWhere.TrivialForest_WhereAll_1M",
            "value": 46133028.603896104,
            "unit": "ns",
            "range": "± 201556.57568356668"
          },
          {
            "name": "Arborist.Benchmarks.BreadthFirstWhere.TrivialForest_WhereNone_1M",
            "value": 6355067.171875,
            "unit": "ns",
            "range": "± 10097.333333330982"
          },
          {
            "name": "Arborist.Benchmarks.BreadthFirstWhere.DegenerateTree_WhereAll_1M",
            "value": 87409152.3076923,
            "unit": "ns",
            "range": "± 209359.93171728542"
          },
          {
            "name": "Arborist.Benchmarks.BreadthFirstWhere.DegenerateTree_WhereNone_1M",
            "value": 21300036.359375,
            "unit": "ns",
            "range": "± 134210.6730309188"
          },
          {
            "name": "Arborist.Benchmarks.DepthFirstWhere.TriangleTree_PruneAfter_1448",
            "value": 72126339.40659341,
            "unit": "ns",
            "range": "± 70337.35955749168"
          },
          {
            "name": "Arborist.Benchmarks.DepthFirstWhere.WhereAll_TrivialForest_1M",
            "value": 39872245.18974359,
            "unit": "ns",
            "range": "± 144355.32596322667"
          },
          {
            "name": "Arborist.Benchmarks.DepthFirstWhere.WhereNone_TrivialForest_1M",
            "value": 8187056.083333333,
            "unit": "ns",
            "range": "± 147406.77113198425"
          },
          {
            "name": "Arborist.Benchmarks.DepthFirstWhere.WhereAll_DegenerateTree_1M",
            "value": 67350915.6875,
            "unit": "ns",
            "range": "± 398463.8763003249"
          },
          {
            "name": "Arborist.Benchmarks.DepthFirstWhere.WhereNone_DegenerateTree_1M",
            "value": 16953241.558333334,
            "unit": "ns",
            "range": "± 42573.57622832808"
          },
          {
            "name": "Arborist.Benchmarks.LeaffixAggregate.TriangleTree_1448",
            "value": 75775164.44761905,
            "unit": "ns",
            "range": "± 162001.40400579406"
          },
          {
            "name": "Arborist.Benchmarks.LeaffixScan.TriangleTree_1448",
            "value": 74867608.58333334,
            "unit": "ns",
            "range": "± 307650.21047258074"
          },
          {
            "name": "Arborist.Benchmarks.LeaffixAggregate.DegenerateTree_1M",
            "value": 47289214.303030305,
            "unit": "ns",
            "range": "± 758491.1346090632"
          },
          {
            "name": "Arborist.Benchmarks.LeaffixScan.DegenerateTree_1M",
            "value": 44077136.77777777,
            "unit": "ns",
            "range": "± 601160.9676676953"
          },
          {
            "name": "Arborist.Benchmarks.LeaffixAggregate.TrivialForest_1M",
            "value": 17730922.87276786,
            "unit": "ns",
            "range": "± 63110.99628025053"
          },
          {
            "name": "Arborist.Benchmarks.Materialize.TriangleTree_1448",
            "value": 67045910.76190475,
            "unit": "ns",
            "range": "± 249942.15311634337"
          },
          {
            "name": "Arborist.Benchmarks.Materialize.DegenerateTree_1M",
            "value": 24828489.03125,
            "unit": "ns",
            "range": "± 514948.98945518245"
          },
          {
            "name": "Arborist.Benchmarks.Select.SelectComposition",
            "value": 19689947.622916665,
            "unit": "ns",
            "range": "± 183255.70588089642"
          },
          {
            "name": "Arborist.Benchmarks.PruneAfter.Bft_TrivialForest_1M",
            "value": 11643851.9875,
            "unit": "ns",
            "range": "± 41848.57887732294"
          },
          {
            "name": "Arborist.Benchmarks.PruneBefore.Bft_TrivialForest_1M",
            "value": 5997699.386197916,
            "unit": "ns",
            "range": "± 10850.91955031411"
          },
          {
            "name": "Arborist.Benchmarks.PruneAfter.Dft_TrivialForest_1M",
            "value": 11554654.854166666,
            "unit": "ns",
            "range": "± 6774.49797370347"
          },
          {
            "name": "Arborist.Benchmarks.PruneBefore.Dft_TrivialForest_1M",
            "value": 8581944.179086538,
            "unit": "ns",
            "range": "± 22909.589676161173"
          },
          {
            "name": "Arborist.Benchmarks.AllNodes.Bft_CompleteBinaryTree_PruneBefore_19",
            "value": 82030399.89285716,
            "unit": "ns",
            "range": "± 350418.252348414"
          },
          {
            "name": "Arborist.Benchmarks.AnyNodes.Bft_CompleteBinaryTree_PruneBefore_19",
            "value": 81013622.58974358,
            "unit": "ns",
            "range": "± 413929.9659912047"
          },
          {
            "name": "Arborist.Benchmarks.CountNodes.DeepTree",
            "value": 23774057.81026786,
            "unit": "ns",
            "range": "± 118277.30636348033"
          },
          {
            "name": "Arborist.Benchmarks.GetLeaves.DeepTree",
            "value": 10830517.573958334,
            "unit": "ns",
            "range": "± 18046.53146209961"
          },
          {
            "name": "Arborist.Benchmarks.AllNodes.Dft_CompleteBinaryTree_PruneBefore_19",
            "value": 50542707.379999995,
            "unit": "ns",
            "range": "± 33261.23483001447"
          },
          {
            "name": "Arborist.Benchmarks.AnyNodes.Dft_CompleteBinaryTree_PruneBefore_19",
            "value": 50754680.35,
            "unit": "ns",
            "range": "± 23636.821630493818"
          },
          {
            "name": "Arborist.Benchmarks.CountNodes.TriangleTree_PruneAfter_2048",
            "value": 60377908.45,
            "unit": "ns",
            "range": "± 274935.26888647064"
          },
          {
            "name": "Arborist.Benchmarks.GetLeaves.CompleteBinaryTree_PruneBefore_20",
            "value": 102261999.78461538,
            "unit": "ns",
            "range": "± 500672.32749000133"
          },
          {
            "name": "Arborist.Benchmarks.AllNodes.Bft_TriangleTree_PruneBefore_19",
            "value": 113844425.36666666,
            "unit": "ns",
            "range": "± 158936.22564538062"
          },
          {
            "name": "Arborist.Benchmarks.AnyNodes.Bft_TriangleTree_PruneBefore_19",
            "value": 117095746.94999999,
            "unit": "ns",
            "range": "± 403177.1032719091"
          },
          {
            "name": "Arborist.Benchmarks.CountNodes.CompleteBinaryTree_PruneAfter_20",
            "value": 62645478.39560439,
            "unit": "ns",
            "range": "± 119685.70516204723"
          },
          {
            "name": "Arborist.Benchmarks.AllNodes.Dft_TriangleTree_PruneBefore_19",
            "value": 58515478.62393163,
            "unit": "ns",
            "range": "± 108815.32236175363"
          },
          {
            "name": "Arborist.Benchmarks.AnyNodes.Dft_TriangleTree_PruneBefore_19",
            "value": 58275983.611111104,
            "unit": "ns",
            "range": "± 197634.22067685146"
          },
          {
            "name": "Arborist.Benchmarks.SkipAllNodes.Bft_TriangleTree_1448",
            "value": 29507530.703125,
            "unit": "ns",
            "range": "± 17971.679710967794"
          },
          {
            "name": "Arborist.Benchmarks.SkipAllNodes.Dft_TriangleTree_1448",
            "value": 27632602.425,
            "unit": "ns",
            "range": "± 143197.758313461"
          }
        ]
      },
      {
        "commit": {
          "author": {
            "email": "jason.boyd.ce@gmail.com",
            "name": "Jason Boyd",
            "username": "jasonmcboyd"
          },
          "committer": {
            "email": "jason.boyd.ce@gmail.com",
            "name": "Jason Boyd",
            "username": "jasonmcboyd"
          },
          "distinct": true,
          "id": "7c795cd25f5b93fbac1993cf6a71485ef8c2de74",
          "message": "Add MemoryDiagnoser benchmark for Invert\n\nCovers TriangleTree depth-1448 (~1M) and a 1M-deep DegenerateTree, tagged LINQ for the dashboard. Completes benchmark coverage of the now-flat transform/aggregation ops (Materialize, LeaffixScan, LeaffixAggregate, Invert).\n\nCo-Authored-By: Claude Opus 4.8 (1M context) <noreply@anthropic.com>\nClaude-Session: https://claude.ai/code/session_01Wg3xArL4FATQaXQMBvhXdg",
          "timestamp": "2026-06-23T22:22:47Z",
          "tree_id": "547984a2584ebd974bf3f4a1790b68fc9b328cfc",
          "url": "https://github.com/jasonmcboyd/Arborist/commit/7c795cd25f5b93fbac1993cf6a71485ef8c2de74"
        },
        "date": 1782256078544,
        "tool": "benchmarkdotnet",
        "benches": [
          {
            "name": "Arborist.Benchmarks.BreadthFirstWhere.TriangleTree_1448",
            "value": 95991405.15476191,
            "unit": "ns",
            "range": "± 69479.75560757438"
          },
          {
            "name": "Arborist.Benchmarks.BreadthFirstWhere.TrivialForest_WhereAll_1M",
            "value": 54611614.49074074,
            "unit": "ns",
            "range": "± 153967.10107802556"
          },
          {
            "name": "Arborist.Benchmarks.BreadthFirstWhere.TrivialForest_WhereNone_1M",
            "value": 6578175.459635417,
            "unit": "ns",
            "range": "± 8648.109709693814"
          },
          {
            "name": "Arborist.Benchmarks.BreadthFirstWhere.DegenerateTree_WhereAll_1M",
            "value": 115207992.71666665,
            "unit": "ns",
            "range": "± 377491.93527682626"
          },
          {
            "name": "Arborist.Benchmarks.BreadthFirstWhere.DegenerateTree_WhereNone_1M",
            "value": 23513449.118489582,
            "unit": "ns",
            "range": "± 598836.4425017373"
          },
          {
            "name": "Arborist.Benchmarks.DepthFirstWhere.TriangleTree_PruneAfter_1448",
            "value": 83431932.89285715,
            "unit": "ns",
            "range": "± 122083.64928931893"
          },
          {
            "name": "Arborist.Benchmarks.DepthFirstWhere.WhereAll_TrivialForest_1M",
            "value": 43877570.52272727,
            "unit": "ns",
            "range": "± 61240.025059349886"
          },
          {
            "name": "Arborist.Benchmarks.DepthFirstWhere.WhereNone_TrivialForest_1M",
            "value": 8432594.454166668,
            "unit": "ns",
            "range": "± 26834.72099142946"
          },
          {
            "name": "Arborist.Benchmarks.DepthFirstWhere.WhereAll_DegenerateTree_1M",
            "value": 69304630.40659341,
            "unit": "ns",
            "range": "± 139562.6015766157"
          },
          {
            "name": "Arborist.Benchmarks.DepthFirstWhere.WhereNone_DegenerateTree_1M",
            "value": 19050151.566964287,
            "unit": "ns",
            "range": "± 46681.45130061005"
          },
          {
            "name": "Arborist.Benchmarks.Invert.TriangleTree_1448",
            "value": 78748414.63095239,
            "unit": "ns",
            "range": "± 207033.90711777844"
          },
          {
            "name": "Arborist.Benchmarks.Invert.DegenerateTree_1M",
            "value": 29298989.203125,
            "unit": "ns",
            "range": "± 275465.09839695157"
          },
          {
            "name": "Arborist.Benchmarks.LeaffixAggregate.TriangleTree_1448",
            "value": 83061721.99999999,
            "unit": "ns",
            "range": "± 311885.46748076956"
          },
          {
            "name": "Arborist.Benchmarks.LeaffixScan.TriangleTree_1448",
            "value": 83072023.63076922,
            "unit": "ns",
            "range": "± 251565.5588777438"
          },
          {
            "name": "Arborist.Benchmarks.LeaffixAggregate.DegenerateTree_1M",
            "value": 41415511.8051282,
            "unit": "ns",
            "range": "± 580483.0941225357"
          },
          {
            "name": "Arborist.Benchmarks.LeaffixScan.DegenerateTree_1M",
            "value": 38779475.01948052,
            "unit": "ns",
            "range": "± 520808.9795915811"
          },
          {
            "name": "Arborist.Benchmarks.LeaffixAggregate.TrivialForest_1M",
            "value": 20775109.025,
            "unit": "ns",
            "range": "± 72154.39898740745"
          },
          {
            "name": "Arborist.Benchmarks.Materialize.TriangleTree_1448",
            "value": 75049996.47619046,
            "unit": "ns",
            "range": "± 257128.55414813483"
          },
          {
            "name": "Arborist.Benchmarks.Materialize.DegenerateTree_1M",
            "value": 26168116.418402776,
            "unit": "ns",
            "range": "± 554036.6274955432"
          },
          {
            "name": "Arborist.Benchmarks.Select.SelectComposition",
            "value": 23969086.06971154,
            "unit": "ns",
            "range": "± 40613.04502194803"
          },
          {
            "name": "Arborist.Benchmarks.PruneAfter.Bft_TrivialForest_1M",
            "value": 12797036.504464285,
            "unit": "ns",
            "range": "± 18234.5658229537"
          },
          {
            "name": "Arborist.Benchmarks.PruneBefore.Bft_TrivialForest_1M",
            "value": 6342025.641225962,
            "unit": "ns",
            "range": "± 4563.065480425247"
          },
          {
            "name": "Arborist.Benchmarks.PruneAfter.Dft_TrivialForest_1M",
            "value": 12750421.775,
            "unit": "ns",
            "range": "± 15732.788762133514"
          },
          {
            "name": "Arborist.Benchmarks.PruneBefore.Dft_TrivialForest_1M",
            "value": 9357674.733333332,
            "unit": "ns",
            "range": "± 13948.986550310496"
          },
          {
            "name": "Arborist.Benchmarks.AllNodes.Bft_CompleteBinaryTree_PruneBefore_19",
            "value": 92787809.36666666,
            "unit": "ns",
            "range": "± 432278.9744535676"
          },
          {
            "name": "Arborist.Benchmarks.AnyNodes.Bft_CompleteBinaryTree_PruneBefore_19",
            "value": 93260465.85555556,
            "unit": "ns",
            "range": "± 510210.866430016"
          },
          {
            "name": "Arborist.Benchmarks.CountNodes.DeepTree",
            "value": 25462117.425480768,
            "unit": "ns",
            "range": "± 84093.98122813315"
          },
          {
            "name": "Arborist.Benchmarks.GetLeaves.DeepTree",
            "value": 11896908.985576924,
            "unit": "ns",
            "range": "± 10716.339101257125"
          },
          {
            "name": "Arborist.Benchmarks.AllNodes.Dft_CompleteBinaryTree_PruneBefore_19",
            "value": 60108036.33333333,
            "unit": "ns",
            "range": "± 697975.0620784959"
          },
          {
            "name": "Arborist.Benchmarks.AnyNodes.Dft_CompleteBinaryTree_PruneBefore_19",
            "value": 58665170.64285714,
            "unit": "ns",
            "range": "± 122344.95557500493"
          },
          {
            "name": "Arborist.Benchmarks.CountNodes.TriangleTree_PruneAfter_2048",
            "value": 65415021.10714286,
            "unit": "ns",
            "range": "± 167023.40987485243"
          },
          {
            "name": "Arborist.Benchmarks.GetLeaves.CompleteBinaryTree_PruneBefore_20",
            "value": 119172552.04285717,
            "unit": "ns",
            "range": "± 1945066.6006457622"
          },
          {
            "name": "Arborist.Benchmarks.AllNodes.Bft_TriangleTree_PruneBefore_19",
            "value": 138140172.56666666,
            "unit": "ns",
            "range": "± 1834771.7593016424"
          },
          {
            "name": "Arborist.Benchmarks.AnyNodes.Bft_TriangleTree_PruneBefore_19",
            "value": 137140210.20833334,
            "unit": "ns",
            "range": "± 162726.60878123555"
          },
          {
            "name": "Arborist.Benchmarks.CountNodes.CompleteBinaryTree_PruneAfter_20",
            "value": 72889242.07777779,
            "unit": "ns",
            "range": "± 525083.4172266865"
          },
          {
            "name": "Arborist.Benchmarks.AllNodes.Dft_TriangleTree_PruneBefore_19",
            "value": 68831515.26666665,
            "unit": "ns",
            "range": "± 325520.0088017041"
          },
          {
            "name": "Arborist.Benchmarks.AnyNodes.Dft_TriangleTree_PruneBefore_19",
            "value": 67670891.94166666,
            "unit": "ns",
            "range": "± 219362.9967900608"
          },
          {
            "name": "Arborist.Benchmarks.SkipAllNodes.Bft_TriangleTree_1448",
            "value": 32414598.129807692,
            "unit": "ns",
            "range": "± 69631.95704405273"
          },
          {
            "name": "Arborist.Benchmarks.SkipAllNodes.Dft_TriangleTree_1448",
            "value": 31159730.483333334,
            "unit": "ns",
            "range": "± 49691.56214596879"
          }
        ]
      },
      {
        "commit": {
          "author": {
            "email": "jason.boyd.ce@gmail.com",
            "name": "Jason Boyd",
            "username": "jasonmcboyd"
          },
          "committer": {
            "email": "jason.boyd.ce@gmail.com",
            "name": "Jason Boyd",
            "username": "jasonmcboyd"
          },
          "distinct": true,
          "id": "cf2c06864e8fdc1e6c24df1f1437f0dae1da26b0",
          "message": "Add 2-param Treenumerable base; drop redundant identity maps (map cleanup stage 1)\n\nThe sample trees whose node IS their surfaced value (TriangleTree, CollatzTree, CompleteBinaryTree, NDecrementTree, DeepTree) were each passing a redundant 'node => node' nodeToValueMap and carrying a duplicate type parameter. Add a 2-param Treenumerable<TNode, TChildEnumerator> convenience base (identity map) and migrate them to it. Public surfaces unchanged. The 3-param base + map remain for PreorderTree's genuine index->value dereference.\n\nCo-Authored-By: Claude Opus 4.8 (1M context) <noreply@anthropic.com>\nClaude-Session: https://claude.ai/code/session_01Wg3xArL4FATQaXQMBvhXdg",
          "timestamp": "2026-06-23T22:35:56Z",
          "tree_id": "005c51f9d7f00116b4e04d1cdb2a5fd0680fc86e",
          "url": "https://github.com/jasonmcboyd/Arborist/commit/cf2c06864e8fdc1e6c24df1f1437f0dae1da26b0"
        },
        "date": 1782257437807,
        "tool": "benchmarkdotnet",
        "benches": [
          {
            "name": "Arborist.Benchmarks.BreadthFirstWhere.TriangleTree_1448",
            "value": 86284053.18055555,
            "unit": "ns",
            "range": "± 62369.50363364749"
          },
          {
            "name": "Arborist.Benchmarks.BreadthFirstWhere.TrivialForest_WhereAll_1M",
            "value": 46246645.76969697,
            "unit": "ns",
            "range": "± 127786.4455767343"
          },
          {
            "name": "Arborist.Benchmarks.BreadthFirstWhere.TrivialForest_WhereNone_1M",
            "value": 6443160.033482143,
            "unit": "ns",
            "range": "± 7114.511766142061"
          },
          {
            "name": "Arborist.Benchmarks.BreadthFirstWhere.DegenerateTree_WhereAll_1M",
            "value": 87322572.25,
            "unit": "ns",
            "range": "± 160473.2671041512"
          },
          {
            "name": "Arborist.Benchmarks.BreadthFirstWhere.DegenerateTree_WhereNone_1M",
            "value": 22476029.541360293,
            "unit": "ns",
            "range": "± 714525.0511880588"
          },
          {
            "name": "Arborist.Benchmarks.DepthFirstWhere.TriangleTree_PruneAfter_1448",
            "value": 72650346.87619047,
            "unit": "ns",
            "range": "± 138780.64351866234"
          },
          {
            "name": "Arborist.Benchmarks.DepthFirstWhere.WhereAll_TrivialForest_1M",
            "value": 39624512.340659335,
            "unit": "ns",
            "range": "± 140104.1707813927"
          },
          {
            "name": "Arborist.Benchmarks.DepthFirstWhere.WhereNone_TrivialForest_1M",
            "value": 8190041.905133928,
            "unit": "ns",
            "range": "± 31981.522502121297"
          },
          {
            "name": "Arborist.Benchmarks.DepthFirstWhere.WhereAll_DegenerateTree_1M",
            "value": 64986749.04807692,
            "unit": "ns",
            "range": "± 123775.54403250696"
          },
          {
            "name": "Arborist.Benchmarks.DepthFirstWhere.WhereNone_DegenerateTree_1M",
            "value": 17088207.1125,
            "unit": "ns",
            "range": "± 42090.45088253663"
          },
          {
            "name": "Arborist.Benchmarks.Invert.TriangleTree_1448",
            "value": 70896334.7,
            "unit": "ns",
            "range": "± 361242.73130801803"
          },
          {
            "name": "Arborist.Benchmarks.Invert.DegenerateTree_1M",
            "value": 29133391.91875,
            "unit": "ns",
            "range": "± 202355.22386625738"
          },
          {
            "name": "Arborist.Benchmarks.LeaffixAggregate.TriangleTree_1448",
            "value": 76641625.91208792,
            "unit": "ns",
            "range": "± 191643.84463952907"
          },
          {
            "name": "Arborist.Benchmarks.LeaffixScan.TriangleTree_1448",
            "value": 74761701.07777777,
            "unit": "ns",
            "range": "± 664396.8301487877"
          },
          {
            "name": "Arborist.Benchmarks.LeaffixAggregate.DegenerateTree_1M",
            "value": 46850699.73333334,
            "unit": "ns",
            "range": "± 699029.8928108624"
          },
          {
            "name": "Arborist.Benchmarks.LeaffixScan.DegenerateTree_1M",
            "value": 43362068.07222222,
            "unit": "ns",
            "range": "± 406159.8021478465"
          },
          {
            "name": "Arborist.Benchmarks.LeaffixAggregate.TrivialForest_1M",
            "value": 17698417.791666668,
            "unit": "ns",
            "range": "± 85222.25628506848"
          },
          {
            "name": "Arborist.Benchmarks.Materialize.TriangleTree_1448",
            "value": 66632874.3877551,
            "unit": "ns",
            "range": "± 223343.8432226029"
          },
          {
            "name": "Arborist.Benchmarks.Materialize.DegenerateTree_1M",
            "value": 25123689.316666666,
            "unit": "ns",
            "range": "± 410748.4486485801"
          },
          {
            "name": "Arborist.Benchmarks.Select.SelectComposition",
            "value": 21196098.4453125,
            "unit": "ns",
            "range": "± 40061.799670341185"
          },
          {
            "name": "Arborist.Benchmarks.PruneAfter.Bft_TrivialForest_1M",
            "value": 11413110.117788462,
            "unit": "ns",
            "range": "± 12007.81557542321"
          },
          {
            "name": "Arborist.Benchmarks.PruneBefore.Bft_TrivialForest_1M",
            "value": 6014512.845424107,
            "unit": "ns",
            "range": "± 7105.107166205404"
          },
          {
            "name": "Arborist.Benchmarks.PruneAfter.Dft_TrivialForest_1M",
            "value": 11491979.973214285,
            "unit": "ns",
            "range": "± 42942.33579553283"
          },
          {
            "name": "Arborist.Benchmarks.PruneBefore.Dft_TrivialForest_1M",
            "value": 8575308.340625,
            "unit": "ns",
            "range": "± 22012.34429072428"
          },
          {
            "name": "Arborist.Benchmarks.AllNodes.Bft_CompleteBinaryTree_PruneBefore_19",
            "value": 81448401.3,
            "unit": "ns",
            "range": "± 245250.34080463578"
          },
          {
            "name": "Arborist.Benchmarks.AnyNodes.Bft_CompleteBinaryTree_PruneBefore_19",
            "value": 81251060.55128205,
            "unit": "ns",
            "range": "± 259546.61471597923"
          },
          {
            "name": "Arborist.Benchmarks.CountNodes.DeepTree",
            "value": 24303244.916666668,
            "unit": "ns",
            "range": "± 226411.35827464308"
          },
          {
            "name": "Arborist.Benchmarks.GetLeaves.DeepTree",
            "value": 11065408.698660715,
            "unit": "ns",
            "range": "± 31344.614295463445"
          },
          {
            "name": "Arborist.Benchmarks.AllNodes.Dft_CompleteBinaryTree_PruneBefore_19",
            "value": 51271754.786666654,
            "unit": "ns",
            "range": "± 262492.968180641"
          },
          {
            "name": "Arborist.Benchmarks.AnyNodes.Dft_CompleteBinaryTree_PruneBefore_19",
            "value": 51146963.35384615,
            "unit": "ns",
            "range": "± 101712.40132000911"
          },
          {
            "name": "Arborist.Benchmarks.CountNodes.TriangleTree_PruneAfter_2048",
            "value": 60023851.70535714,
            "unit": "ns",
            "range": "± 151585.14265995676"
          },
          {
            "name": "Arborist.Benchmarks.GetLeaves.CompleteBinaryTree_PruneBefore_20",
            "value": 102092140.49333334,
            "unit": "ns",
            "range": "± 491686.64073440654"
          },
          {
            "name": "Arborist.Benchmarks.AllNodes.Bft_TriangleTree_PruneBefore_19",
            "value": 115231886.94999999,
            "unit": "ns",
            "range": "± 542782.7565805417"
          },
          {
            "name": "Arborist.Benchmarks.AnyNodes.Bft_TriangleTree_PruneBefore_19",
            "value": 114177409.48333333,
            "unit": "ns",
            "range": "± 176372.00203358254"
          },
          {
            "name": "Arborist.Benchmarks.CountNodes.CompleteBinaryTree_PruneAfter_20",
            "value": 64735934.4095238,
            "unit": "ns",
            "range": "± 308049.44355158275"
          },
          {
            "name": "Arborist.Benchmarks.AllNodes.Dft_TriangleTree_PruneBefore_19",
            "value": 59244134.977777764,
            "unit": "ns",
            "range": "± 432293.80796411925"
          },
          {
            "name": "Arborist.Benchmarks.AnyNodes.Dft_TriangleTree_PruneBefore_19",
            "value": 58864208.6923077,
            "unit": "ns",
            "range": "± 154431.1642601365"
          },
          {
            "name": "Arborist.Benchmarks.SkipAllNodes.Bft_TriangleTree_1448",
            "value": 30859024.758333333,
            "unit": "ns",
            "range": "± 42183.939062262296"
          },
          {
            "name": "Arborist.Benchmarks.SkipAllNodes.Dft_TriangleTree_1448",
            "value": 27650592.710416667,
            "unit": "ns",
            "range": "± 100533.98523113616"
          }
        ]
      },
      {
        "commit": {
          "author": {
            "email": "jason.boyd.ce@gmail.com",
            "name": "Jason Boyd",
            "username": "jasonmcboyd"
          },
          "committer": {
            "email": "jason.boyd.ce@gmail.com",
            "name": "Jason Boyd",
            "username": "jasonmcboyd"
          },
          "distinct": true,
          "id": "ca567e0f439d6d1f0f5c2143add771a6c13fd41c",
          "message": "Fix BFT Where O(depth) memory regression: tail-carry skip prefix\n\nf7eae61's O(N)-time prefix carry stored _PredSkipPrefix as a List<int>\nindexed by absolute inner depth, grown to the current depth on every\nscheduled node -- so a 1M-deep degenerate Where(_=>true) chain allocated\n~8.39 MB even though nothing is predicate-skipped (every entry 0).\n\nReplace it with a tail-carry: the prefix is monotonic non-decreasing in\ndepth and constant (= total skips on the path) beyond the deepest skipped\nancestor, so store only up to the deepest skip and serve reads past the\nend from a scalar tail. New _PrefixStored + _PrefixStoredCount +\n_PrefixTail; PrefixRead/PrefixWriteScheduled (no-op when value == tail, so\nzero allocation in the accepted region); PrefixAnchor truncates the stored\ncount to frontDepth-1 on front-advance.\n\nWhereAll 8.39 MB -> ~1.9 KB (O(1) in depth); WhereNone byte-identical\n(inherent O(depth) preserved). Visit stream byte-identical: validated\nagainst Where2InProcessScan (full c..i, 891k cases) + WhereTests (218/0),\nnet48 + net8.0 clean. Add WhereBreadthFirstAllocationTests as a hard\nmemory-bound regression guard (the gh-pages benchmark only soft-alerts).\n\nCo-Authored-By: Claude Opus 4.8 (1M context) <noreply@anthropic.com>\nClaude-Session: https://claude.ai/code/session_01Wg3xArL4FATQaXQMBvhXdg",
          "timestamp": "2026-06-25T18:13:44Z",
          "tree_id": "6d5c2256d2c8574537a4127f73a8de04fe0b4011",
          "url": "https://github.com/jasonmcboyd/Arborist/commit/ca567e0f439d6d1f0f5c2143add771a6c13fd41c"
        },
        "date": 1782413420269,
        "tool": "benchmarkdotnet",
        "benches": [
          {
            "name": "Arborist.Benchmarks.BreadthFirstWhere.TriangleTree_1448",
            "value": 93297512.55555557,
            "unit": "ns",
            "range": "± 358871.99029892206"
          },
          {
            "name": "Arborist.Benchmarks.BreadthFirstWhere.TrivialForest_WhereAll_1M",
            "value": 50608212.400000006,
            "unit": "ns",
            "range": "± 41361.988685353404"
          },
          {
            "name": "Arborist.Benchmarks.BreadthFirstWhere.TrivialForest_WhereNone_1M",
            "value": 6254566.741666666,
            "unit": "ns",
            "range": "± 5887.764282022249"
          },
          {
            "name": "Arborist.Benchmarks.BreadthFirstWhere.DegenerateTree_WhereAll_1M",
            "value": 89569247.07692306,
            "unit": "ns",
            "range": "± 304854.1117465"
          },
          {
            "name": "Arborist.Benchmarks.BreadthFirstWhere.DegenerateTree_WhereNone_1M",
            "value": 23202331.941875,
            "unit": "ns",
            "range": "± 1440737.7085339003"
          },
          {
            "name": "Arborist.Benchmarks.DepthFirstWhere.TriangleTree_PruneAfter_1448",
            "value": 78867035.54945055,
            "unit": "ns",
            "range": "± 242914.7528206571"
          },
          {
            "name": "Arborist.Benchmarks.DepthFirstWhere.WhereAll_TrivialForest_1M",
            "value": 42452427.76923077,
            "unit": "ns",
            "range": "± 225670.3175839957"
          },
          {
            "name": "Arborist.Benchmarks.DepthFirstWhere.WhereNone_TrivialForest_1M",
            "value": 7961218.354910715,
            "unit": "ns",
            "range": "± 23224.25026518076"
          },
          {
            "name": "Arborist.Benchmarks.DepthFirstWhere.WhereAll_DegenerateTree_1M",
            "value": 67875412.90384616,
            "unit": "ns",
            "range": "± 126830.13427073715"
          },
          {
            "name": "Arborist.Benchmarks.DepthFirstWhere.WhereNone_DegenerateTree_1M",
            "value": 17829039.760416668,
            "unit": "ns",
            "range": "± 104169.96422190436"
          },
          {
            "name": "Arborist.Benchmarks.Invert.TriangleTree_1448",
            "value": 73783457.34444444,
            "unit": "ns",
            "range": "± 451143.3433049265"
          },
          {
            "name": "Arborist.Benchmarks.Invert.DegenerateTree_1M",
            "value": 30058983.52901786,
            "unit": "ns",
            "range": "± 164980.06887108515"
          },
          {
            "name": "Arborist.Benchmarks.LeaffixAggregate.TriangleTree_1448",
            "value": 75742879.21428572,
            "unit": "ns",
            "range": "± 442203.74367629975"
          },
          {
            "name": "Arborist.Benchmarks.LeaffixScan.TriangleTree_1448",
            "value": 79823925.84444445,
            "unit": "ns",
            "range": "± 1462322.618323076"
          },
          {
            "name": "Arborist.Benchmarks.LeaffixAggregate.DegenerateTree_1M",
            "value": 42777076.352564104,
            "unit": "ns",
            "range": "± 1090612.7440579077"
          },
          {
            "name": "Arborist.Benchmarks.LeaffixScan.DegenerateTree_1M",
            "value": 39481972.89230769,
            "unit": "ns",
            "range": "± 340573.34524391236"
          },
          {
            "name": "Arborist.Benchmarks.LeaffixAggregate.TrivialForest_1M",
            "value": 18831112.108333334,
            "unit": "ns",
            "range": "± 47042.677974154816"
          },
          {
            "name": "Arborist.Benchmarks.Materialize.TriangleTree_1448",
            "value": 69905480.78205128,
            "unit": "ns",
            "range": "± 738052.7313515736"
          },
          {
            "name": "Arborist.Benchmarks.Materialize.DegenerateTree_1M",
            "value": 26859018.34375,
            "unit": "ns",
            "range": "± 352918.7404999183"
          },
          {
            "name": "Arborist.Benchmarks.Select.SelectComposition",
            "value": 16673648.935416667,
            "unit": "ns",
            "range": "± 69335.05704861859"
          },
          {
            "name": "Arborist.Benchmarks.PruneAfter.Bft_TrivialForest_1M",
            "value": 11525286.212740384,
            "unit": "ns",
            "range": "± 43134.418134000254"
          },
          {
            "name": "Arborist.Benchmarks.PruneBefore.Bft_TrivialForest_1M",
            "value": 5720712.701923077,
            "unit": "ns",
            "range": "± 4463.518103637477"
          },
          {
            "name": "Arborist.Benchmarks.PruneAfter.Dft_TrivialForest_1M",
            "value": 12062104.6171875,
            "unit": "ns",
            "range": "± 71183.36994453364"
          },
          {
            "name": "Arborist.Benchmarks.PruneBefore.Dft_TrivialForest_1M",
            "value": 8506346.856971154,
            "unit": "ns",
            "range": "± 29962.544768934928"
          },
          {
            "name": "Arborist.Benchmarks.AllNodes.Bft_CompleteBinaryTree_PruneBefore_19",
            "value": 91752395.23333332,
            "unit": "ns",
            "range": "± 610912.5637824313"
          },
          {
            "name": "Arborist.Benchmarks.AnyNodes.Bft_CompleteBinaryTree_PruneBefore_19",
            "value": 88948974.27777778,
            "unit": "ns",
            "range": "± 498284.830433365"
          },
          {
            "name": "Arborist.Benchmarks.CountNodes.DeepTree",
            "value": 24996219.17410714,
            "unit": "ns",
            "range": "± 120831.94075792078"
          },
          {
            "name": "Arborist.Benchmarks.GetLeaves.DeepTree",
            "value": 11116073.2265625,
            "unit": "ns",
            "range": "± 39630.063665542875"
          },
          {
            "name": "Arborist.Benchmarks.AllNodes.Dft_CompleteBinaryTree_PruneBefore_19",
            "value": 56500598.460317455,
            "unit": "ns",
            "range": "± 233709.75892798015"
          },
          {
            "name": "Arborist.Benchmarks.AnyNodes.Dft_CompleteBinaryTree_PruneBefore_19",
            "value": 56352049.103703715,
            "unit": "ns",
            "range": "± 235201.1685579207"
          },
          {
            "name": "Arborist.Benchmarks.CountNodes.TriangleTree_PruneAfter_2048",
            "value": 63463591.428571425,
            "unit": "ns",
            "range": "± 92590.34074316653"
          },
          {
            "name": "Arborist.Benchmarks.GetLeaves.CompleteBinaryTree_PruneBefore_20",
            "value": 114514854.66666666,
            "unit": "ns",
            "range": "± 267424.23206063703"
          },
          {
            "name": "Arborist.Benchmarks.AllNodes.Bft_TriangleTree_PruneBefore_19",
            "value": 126267273.73333333,
            "unit": "ns",
            "range": "± 2159097.3525758823"
          },
          {
            "name": "Arborist.Benchmarks.AnyNodes.Bft_TriangleTree_PruneBefore_19",
            "value": 121981421.38333333,
            "unit": "ns",
            "range": "± 300407.5778734504"
          },
          {
            "name": "Arborist.Benchmarks.CountNodes.CompleteBinaryTree_PruneAfter_20",
            "value": 72072531.41904761,
            "unit": "ns",
            "range": "± 355931.24413738056"
          },
          {
            "name": "Arborist.Benchmarks.AllNodes.Dft_TriangleTree_PruneBefore_19",
            "value": 64362059.27551021,
            "unit": "ns",
            "range": "± 514595.7745029161"
          },
          {
            "name": "Arborist.Benchmarks.AnyNodes.Dft_TriangleTree_PruneBefore_19",
            "value": 63732691.77678572,
            "unit": "ns",
            "range": "± 180030.85992565312"
          },
          {
            "name": "Arborist.Benchmarks.SkipAllNodes.Bft_TriangleTree_1448",
            "value": 32468018.316666666,
            "unit": "ns",
            "range": "± 257003.12204791128"
          },
          {
            "name": "Arborist.Benchmarks.SkipAllNodes.Dft_TriangleTree_1448",
            "value": 29917233.51875,
            "unit": "ns",
            "range": "± 271048.634219214"
          }
        ]
      },
      {
        "commit": {
          "author": {
            "email": "jason.boyd.ce@gmail.com",
            "name": "Jason Boyd",
            "username": "jasonmcboyd"
          },
          "committer": {
            "email": "jason.boyd.ce@gmail.com",
            "name": "Jason Boyd",
            "username": "jasonmcboyd"
          },
          "distinct": true,
          "id": "6a5d01f5e6181c1c89a0993a6360bab3a4b0bac7",
          "message": "Rewrite BreadthFirstTreenumerator with a structural visit cadence\n\nThe base BFT engine emitted parent visits reactively (when a child was\naccepted), which broke whenever a child was filtered and forced a tangle\nof deferred parent-visit bookkeeping to recover the swallowed visits.\n\nReplace it with a structural cadence: a single FIFO _VisitQueue whose\nfront is the active parent, plus a LIFO _ScheduleStack for the SkipNode\npromotion descent. A parent is visited once when it reaches the front,\nthen once after every child slot that enqueues at least one accepted\nnode -- a single bool, _CurrentSlotEnqueuedNode. Roots are scheduled\nfirst as the children of an implicit no-visit forest sentinel.\n\nThis deletes _OwesPromotedParentVisit, _HasDeferredScheduledChild,\n_DepthOfLastActedOnNode, PayOwedParentVisitAndDeferChild, and the\nOnScheduling/OnVisiting/PromoteChildren/SkipSubtree/Backtrack web in\nfavor of Advance/ApplyStrategy/SkipRemainingSiblings. The now-unused\nOwesPromotedParentVisit field comes off the shared InternalNodeVisitState\nstruct, shrinking every deque slot.\n\nNo behavior change: same (Mode, Node, VisitCount, Position) visit stream.\n\nValidation:\n- Exhaustive BFT-vs-DFT oracle, up to 6 concurrent skips x 27 trees\n- Where2InProcessScan: 891,056 Where-wrapper-vs-oracle cases (groups c..i)\n- Curated exact-order traversal + 14,759 Where/allocation tests\n- Benchmarks: allocations -12% to -14%, time within ShortRun noise\n\nCo-Authored-By: Claude Opus 4.8 (1M context) <noreply@anthropic.com>\nClaude-Session: https://claude.ai/code/session_01Wg3xArL4FATQaXQMBvhXdg",
          "timestamp": "2026-06-26T00:47:42Z",
          "tree_id": "39f01ca28e49bf3bbc704caca59e8d4bb815b334",
          "url": "https://github.com/jasonmcboyd/Arborist/commit/6a5d01f5e6181c1c89a0993a6360bab3a4b0bac7"
        },
        "date": 1782436870883,
        "tool": "benchmarkdotnet",
        "benches": [
          {
            "name": "Arborist.Benchmarks.BreadthFirstWhere.TriangleTree_1448",
            "value": 102375213.76666665,
            "unit": "ns",
            "range": "± 271593.62891422707"
          },
          {
            "name": "Arborist.Benchmarks.BreadthFirstWhere.TrivialForest_WhereAll_1M",
            "value": 50616259.08,
            "unit": "ns",
            "range": "± 49069.33372522357"
          },
          {
            "name": "Arborist.Benchmarks.BreadthFirstWhere.TrivialForest_WhereNone_1M",
            "value": 6259424.969050481,
            "unit": "ns",
            "range": "± 6422.925391048504"
          },
          {
            "name": "Arborist.Benchmarks.BreadthFirstWhere.DegenerateTree_WhereAll_1M",
            "value": 89275630.60714284,
            "unit": "ns",
            "range": "± 535067.071642364"
          },
          {
            "name": "Arborist.Benchmarks.BreadthFirstWhere.DegenerateTree_WhereNone_1M",
            "value": 25080412.21359536,
            "unit": "ns",
            "range": "± 1790342.3153041229"
          },
          {
            "name": "Arborist.Benchmarks.DepthFirstWhere.TriangleTree_PruneAfter_1448",
            "value": 79677441.39795919,
            "unit": "ns",
            "range": "± 91588.40089206719"
          },
          {
            "name": "Arborist.Benchmarks.DepthFirstWhere.WhereAll_TrivialForest_1M",
            "value": 41367869.80952381,
            "unit": "ns",
            "range": "± 247284.74494389005"
          },
          {
            "name": "Arborist.Benchmarks.DepthFirstWhere.WhereNone_TrivialForest_1M",
            "value": 7962820.463169643,
            "unit": "ns",
            "range": "± 16369.56725095945"
          },
          {
            "name": "Arborist.Benchmarks.DepthFirstWhere.WhereAll_DegenerateTree_1M",
            "value": 67783964.25,
            "unit": "ns",
            "range": "± 179675.77022534315"
          },
          {
            "name": "Arborist.Benchmarks.DepthFirstWhere.WhereNone_DegenerateTree_1M",
            "value": 17752102.35714286,
            "unit": "ns",
            "range": "± 65575.64446737948"
          },
          {
            "name": "Arborist.Benchmarks.Invert.TriangleTree_1448",
            "value": 73607559.9871795,
            "unit": "ns",
            "range": "± 577631.2132842706"
          },
          {
            "name": "Arborist.Benchmarks.Invert.DegenerateTree_1M",
            "value": 30503986.775,
            "unit": "ns",
            "range": "± 150005.5620203778"
          },
          {
            "name": "Arborist.Benchmarks.LeaffixAggregate.TriangleTree_1448",
            "value": 77321820.10204081,
            "unit": "ns",
            "range": "± 199016.5995108811"
          },
          {
            "name": "Arborist.Benchmarks.LeaffixScan.TriangleTree_1448",
            "value": 81971437.86666666,
            "unit": "ns",
            "range": "± 267531.2678543543"
          },
          {
            "name": "Arborist.Benchmarks.LeaffixAggregate.DegenerateTree_1M",
            "value": 43259307.72435899,
            "unit": "ns",
            "range": "± 1182560.518563385"
          },
          {
            "name": "Arborist.Benchmarks.LeaffixScan.DegenerateTree_1M",
            "value": 40605768.276923075,
            "unit": "ns",
            "range": "± 602198.6603604412"
          },
          {
            "name": "Arborist.Benchmarks.LeaffixAggregate.TrivialForest_1M",
            "value": 18801246.26785714,
            "unit": "ns",
            "range": "± 76705.91404418026"
          },
          {
            "name": "Arborist.Benchmarks.Materialize.TriangleTree_1448",
            "value": 70188157.47619048,
            "unit": "ns",
            "range": "± 176880.18770022417"
          },
          {
            "name": "Arborist.Benchmarks.Materialize.DegenerateTree_1M",
            "value": 28436789.719065655,
            "unit": "ns",
            "range": "± 1864727.6179113123"
          },
          {
            "name": "Arborist.Benchmarks.Select.SelectComposition",
            "value": 28066111.589583334,
            "unit": "ns",
            "range": "± 139864.57264755107"
          },
          {
            "name": "Arborist.Benchmarks.PruneAfter.Bft_TrivialForest_1M",
            "value": 11788835.704166668,
            "unit": "ns",
            "range": "± 125248.1832813872"
          },
          {
            "name": "Arborist.Benchmarks.PruneBefore.Bft_TrivialForest_1M",
            "value": 5726586.689174107,
            "unit": "ns",
            "range": "± 7720.631967362419"
          },
          {
            "name": "Arborist.Benchmarks.PruneAfter.Dft_TrivialForest_1M",
            "value": 11442138.954166668,
            "unit": "ns",
            "range": "± 44497.33349274752"
          },
          {
            "name": "Arborist.Benchmarks.PruneBefore.Dft_TrivialForest_1M",
            "value": 8514749.401785715,
            "unit": "ns",
            "range": "± 17914.536325977522"
          },
          {
            "name": "Arborist.Benchmarks.AllNodes.Bft_CompleteBinaryTree_PruneBefore_19",
            "value": 88301959.34444444,
            "unit": "ns",
            "range": "± 981924.4731188321"
          },
          {
            "name": "Arborist.Benchmarks.AnyNodes.Bft_CompleteBinaryTree_PruneBefore_19",
            "value": 88292740.63095237,
            "unit": "ns",
            "range": "± 444143.37508305424"
          },
          {
            "name": "Arborist.Benchmarks.CountNodes.DeepTree",
            "value": 24694281.8125,
            "unit": "ns",
            "range": "± 86933.17219898354"
          },
          {
            "name": "Arborist.Benchmarks.GetLeaves.DeepTree",
            "value": 10883334.96205357,
            "unit": "ns",
            "range": "± 35413.53005535097"
          },
          {
            "name": "Arborist.Benchmarks.AllNodes.Dft_CompleteBinaryTree_PruneBefore_19",
            "value": 56853626.74074074,
            "unit": "ns",
            "range": "± 294191.16664042213"
          },
          {
            "name": "Arborist.Benchmarks.AnyNodes.Dft_CompleteBinaryTree_PruneBefore_19",
            "value": 56222555.48148148,
            "unit": "ns",
            "range": "± 333045.73110307165"
          },
          {
            "name": "Arborist.Benchmarks.CountNodes.TriangleTree_PruneAfter_2048",
            "value": 55789527.476190485,
            "unit": "ns",
            "range": "± 83472.9376625722"
          },
          {
            "name": "Arborist.Benchmarks.GetLeaves.CompleteBinaryTree_PruneBefore_20",
            "value": 114862191.77333333,
            "unit": "ns",
            "range": "± 915627.5547242404"
          },
          {
            "name": "Arborist.Benchmarks.AllNodes.Bft_TriangleTree_PruneBefore_19",
            "value": 122125456.73529412,
            "unit": "ns",
            "range": "± 2417180.1502367337"
          },
          {
            "name": "Arborist.Benchmarks.AnyNodes.Bft_TriangleTree_PruneBefore_19",
            "value": 119124500.63076924,
            "unit": "ns",
            "range": "± 234973.5147379501"
          },
          {
            "name": "Arborist.Benchmarks.CountNodes.CompleteBinaryTree_PruneAfter_20",
            "value": 66624407.71428572,
            "unit": "ns",
            "range": "± 201746.94507310307"
          },
          {
            "name": "Arborist.Benchmarks.AllNodes.Dft_TriangleTree_PruneBefore_19",
            "value": 64070331.84693878,
            "unit": "ns",
            "range": "± 375118.3032172411"
          },
          {
            "name": "Arborist.Benchmarks.AnyNodes.Dft_TriangleTree_PruneBefore_19",
            "value": 63560920.66569767,
            "unit": "ns",
            "range": "± 344820.0168732609"
          },
          {
            "name": "Arborist.Benchmarks.SkipAllNodes.Bft_TriangleTree_1448",
            "value": 28048067.76339286,
            "unit": "ns",
            "range": "± 47388.14621698626"
          },
          {
            "name": "Arborist.Benchmarks.SkipAllNodes.Dft_TriangleTree_1448",
            "value": 29575768.770833332,
            "unit": "ns",
            "range": "± 64277.90172114103"
          }
        ]
      },
      {
        "commit": {
          "author": {
            "email": "jason.boyd.ce@gmail.com",
            "name": "Jason Boyd",
            "username": "jasonmcboyd"
          },
          "committer": {
            "email": "jason.boyd.ce@gmail.com",
            "name": "Jason Boyd",
            "username": "jasonmcboyd"
          },
          "distinct": true,
          "id": "ce22f8e1055cf2b7bf6486f1da78d2058d8c69aa",
          "message": "Cap RefSemiDeque partition size to bound LOH allocation and overshoot\n\nGeometric partition growth sized each new partition to the running total\nCapacity, so the largest partition reached ~half the deque's peak element\ncount -- a multi-MB Large Object Heap allocation on wide/deep trees, plus\nup to ~2x peak over-allocation at power-of-two boundaries. Cap partition\nsize at 4096 elements (Math.Min(Capacity, MaxPartitionSize)) to bound both.\n\nBFT CompleteBinaryTree_21 (peak frontier ~2^21, the worst-case boundary):\n96 MB -> 48 MB allocated per traversal, throughput unchanged.\n\nFixed element count rather than a byte budget that would force partitions\nsub-LOH: forcing a 64 B node's partitions sub-LOH measured ~40% slower with\n~7x the Gen0 collections, because large long-lived blocks belong on the LOH.\n\nAdd RefSemiDeque regression tests crossing the cap (heterogeneous-partition\nordering, out-of-order recycling, GetFromBack/RemoveLast across boundaries)\nand an Add_Block64_1M benchmark. Remove unused InitialCapacity.\n\nCo-Authored-By: Claude Opus 4.8 (1M context) <noreply@anthropic.com>",
          "timestamp": "2026-06-26T04:13:38Z",
          "tree_id": "e35c9314063b1f6eda6dfccd1d7349907af73e32",
          "url": "https://github.com/jasonmcboyd/Arborist/commit/ce22f8e1055cf2b7bf6486f1da78d2058d8c69aa"
        },
        "date": 1782449420600,
        "tool": "benchmarkdotnet",
        "benches": [
          {
            "name": "Arborist.Benchmarks.BreadthFirstWhere.TriangleTree_1448",
            "value": 103265724.74285714,
            "unit": "ns",
            "range": "± 615925.7030307084"
          },
          {
            "name": "Arborist.Benchmarks.BreadthFirstWhere.TrivialForest_WhereAll_1M",
            "value": 49909278.99285715,
            "unit": "ns",
            "range": "± 209738.77501159013"
          },
          {
            "name": "Arborist.Benchmarks.BreadthFirstWhere.TrivialForest_WhereNone_1M",
            "value": 6190080.240685096,
            "unit": "ns",
            "range": "± 3920.166835177926"
          },
          {
            "name": "Arborist.Benchmarks.BreadthFirstWhere.DegenerateTree_WhereAll_1M",
            "value": 90029303.84444444,
            "unit": "ns",
            "range": "± 172124.30264890415"
          },
          {
            "name": "Arborist.Benchmarks.BreadthFirstWhere.DegenerateTree_WhereNone_1M",
            "value": 24535408.863729507,
            "unit": "ns",
            "range": "± 1101072.325098343"
          },
          {
            "name": "Arborist.Benchmarks.DepthFirstWhere.TriangleTree_PruneAfter_1448",
            "value": 81407997.0510204,
            "unit": "ns",
            "range": "± 173147.9784593525"
          },
          {
            "name": "Arborist.Benchmarks.DepthFirstWhere.WhereAll_TrivialForest_1M",
            "value": 41603038.76282051,
            "unit": "ns",
            "range": "± 94010.33826209922"
          },
          {
            "name": "Arborist.Benchmarks.DepthFirstWhere.WhereNone_TrivialForest_1M",
            "value": 7981468.794471154,
            "unit": "ns",
            "range": "± 8161.030781369663"
          },
          {
            "name": "Arborist.Benchmarks.DepthFirstWhere.WhereAll_DegenerateTree_1M",
            "value": 72158906.07619047,
            "unit": "ns",
            "range": "± 246074.47634515446"
          },
          {
            "name": "Arborist.Benchmarks.DepthFirstWhere.WhereNone_DegenerateTree_1M",
            "value": 18035119.19375,
            "unit": "ns",
            "range": "± 50438.117634390226"
          },
          {
            "name": "Arborist.Benchmarks.Invert.TriangleTree_1448",
            "value": 73599949.97619048,
            "unit": "ns",
            "range": "± 324337.07809486525"
          },
          {
            "name": "Arborist.Benchmarks.Invert.DegenerateTree_1M",
            "value": 35412705.49999999,
            "unit": "ns",
            "range": "± 500784.4123574338"
          },
          {
            "name": "Arborist.Benchmarks.LeaffixAggregate.TriangleTree_1448",
            "value": 78037777.95714284,
            "unit": "ns",
            "range": "± 166449.51553895156"
          },
          {
            "name": "Arborist.Benchmarks.LeaffixScan.TriangleTree_1448",
            "value": 78828277.48888889,
            "unit": "ns",
            "range": "± 493706.3481944701"
          },
          {
            "name": "Arborist.Benchmarks.LeaffixAggregate.DegenerateTree_1M",
            "value": 43926696.62777778,
            "unit": "ns",
            "range": "± 598684.2609485749"
          },
          {
            "name": "Arborist.Benchmarks.LeaffixScan.DegenerateTree_1M",
            "value": 42152900.61904762,
            "unit": "ns",
            "range": "± 541210.2181750479"
          },
          {
            "name": "Arborist.Benchmarks.LeaffixAggregate.TrivialForest_1M",
            "value": 18892049.551339287,
            "unit": "ns",
            "range": "± 55164.89257048673"
          },
          {
            "name": "Arborist.Benchmarks.Materialize.TriangleTree_1448",
            "value": 70152567.46153846,
            "unit": "ns",
            "range": "± 264800.3778493469"
          },
          {
            "name": "Arborist.Benchmarks.Materialize.DegenerateTree_1M",
            "value": 29613382.170833334,
            "unit": "ns",
            "range": "± 290000.71787384467"
          },
          {
            "name": "Arborist.Benchmarks.Select.SelectComposition",
            "value": 21280324.9375,
            "unit": "ns",
            "range": "± 28566.472008661607"
          },
          {
            "name": "Arborist.Benchmarks.PruneAfter.Bft_TrivialForest_1M",
            "value": 11371033.721875,
            "unit": "ns",
            "range": "± 52492.119546135764"
          },
          {
            "name": "Arborist.Benchmarks.PruneBefore.Bft_TrivialForest_1M",
            "value": 5723106.540625,
            "unit": "ns",
            "range": "± 7309.085155506397"
          },
          {
            "name": "Arborist.Benchmarks.PruneAfter.Dft_TrivialForest_1M",
            "value": 11532744.139583332,
            "unit": "ns",
            "range": "± 52608.607246654734"
          },
          {
            "name": "Arborist.Benchmarks.PruneBefore.Dft_TrivialForest_1M",
            "value": 8587324.55,
            "unit": "ns",
            "range": "± 87808.06595528447"
          },
          {
            "name": "Arborist.Benchmarks.AllNodes.Bft_CompleteBinaryTree_PruneBefore_19",
            "value": 88244523.24444443,
            "unit": "ns",
            "range": "± 578593.8732276658"
          },
          {
            "name": "Arborist.Benchmarks.AnyNodes.Bft_CompleteBinaryTree_PruneBefore_19",
            "value": 89577201.81111111,
            "unit": "ns",
            "range": "± 600182.5869360304"
          },
          {
            "name": "Arborist.Benchmarks.CountNodes.DeepTree",
            "value": 24375006.69419643,
            "unit": "ns",
            "range": "± 70871.29986165985"
          },
          {
            "name": "Arborist.Benchmarks.GetLeaves.DeepTree",
            "value": 10555288.985576924,
            "unit": "ns",
            "range": "± 16844.00754636447"
          },
          {
            "name": "Arborist.Benchmarks.AllNodes.Dft_CompleteBinaryTree_PruneBefore_19",
            "value": 56750502.76984127,
            "unit": "ns",
            "range": "± 265615.2196585232"
          },
          {
            "name": "Arborist.Benchmarks.AnyNodes.Dft_CompleteBinaryTree_PruneBefore_19",
            "value": 56067398.97435898,
            "unit": "ns",
            "range": "± 170126.87713235247"
          },
          {
            "name": "Arborist.Benchmarks.CountNodes.TriangleTree_PruneAfter_2048",
            "value": 56368674.11111112,
            "unit": "ns",
            "range": "± 324597.1538780967"
          },
          {
            "name": "Arborist.Benchmarks.GetLeaves.CompleteBinaryTree_PruneBefore_20",
            "value": 114141005.52857144,
            "unit": "ns",
            "range": "± 638891.6350366366"
          },
          {
            "name": "Arborist.Benchmarks.AllNodes.Bft_TriangleTree_PruneBefore_19",
            "value": 124731585.23333333,
            "unit": "ns",
            "range": "± 1632921.8180572288"
          },
          {
            "name": "Arborist.Benchmarks.AnyNodes.Bft_TriangleTree_PruneBefore_19",
            "value": 121553432.03076924,
            "unit": "ns",
            "range": "± 182923.92346608394"
          },
          {
            "name": "Arborist.Benchmarks.CountNodes.CompleteBinaryTree_PruneAfter_20",
            "value": 65190770.92307692,
            "unit": "ns",
            "range": "± 162975.3701497906"
          },
          {
            "name": "Arborist.Benchmarks.AllNodes.Dft_TriangleTree_PruneBefore_19",
            "value": 64713691.23809524,
            "unit": "ns",
            "range": "± 303371.77056492685"
          },
          {
            "name": "Arborist.Benchmarks.AnyNodes.Dft_TriangleTree_PruneBefore_19",
            "value": 63777737.1,
            "unit": "ns",
            "range": "± 144006.85204373894"
          },
          {
            "name": "Arborist.Benchmarks.SkipAllNodes.Bft_TriangleTree_1448",
            "value": 28144689.84375,
            "unit": "ns",
            "range": "± 88830.24353056519"
          },
          {
            "name": "Arborist.Benchmarks.SkipAllNodes.Dft_TriangleTree_1448",
            "value": 29547537.164583333,
            "unit": "ns",
            "range": "± 219514.48777402626"
          }
        ]
      },
      {
        "commit": {
          "author": {
            "email": "jason.boyd.ce@gmail.com",
            "name": "Jason Boyd",
            "username": "jasonmcboyd"
          },
          "committer": {
            "email": "jason.boyd.ce@gmail.com",
            "name": "Jason Boyd",
            "username": "jasonmcboyd"
          },
          "distinct": true,
          "id": "81b46c42702e71ad313d08c636eb3e3c3c35b140",
          "message": "Cohere BreadthFirstTreenumerator's four deques into one frame stack\n\nThe BFT kept each node's visit state and its child enumerator in FOUR parallel\nRefSemiDeques -- a state deque and an enumerator deque for each of the visit\nqueue and the schedule stack -- relying on keeping each pair in lockstep.\n\nFold each pair into one RefSemiDeque<Frame>, where Frame bundles\n{Node, Position, VisitCount, ChildEnumerator}, driven only by ref so the\nenumerator is never copied. Accepting a node becomes a single whole-frame move\nfrom the schedule stack to the visit queue, which structurally prevents a node\nand its enumerator from desynchronizing. The algorithm and visit cadence are\nunchanged. Mirrors the depth-first engine's frame stack, and removes the\nnow-unreferenced shared InternalNodeVisitState struct.\n\nNo behavior change: same (Mode, Node, VisitCount, Position) visit stream;\ndisposal (including the deliberate idempotent double-dispose on\nSkipDescendants/SkipSiblings) is at exact parity with the original.\n\nValidation:\n- Exact-order traversal + exhaustive DFT-vs-BFT multiset scan (438/0)\n- Full Arborist.Linq suite incl. Where (14,759/0)\n- Benchmarks (Release/Job.Default, clean tree): TriangleTree 289->255ms (-12%),\n  CompleteBinaryTree 337->281ms (-16%), TrivialForest/DegenerateTree 4M within\n  noise; allocation neutral, still zero per-node heap allocation. Timing\n  variance also dropped (StdDev roughly halved on the dense trees).\n\nCo-Authored-By: Claude Opus 4.8 (1M context) <noreply@anthropic.com>\nClaude-Session: https://claude.ai/code/session_01Wg3xArL4FATQaXQMBvhXdg",
          "timestamp": "2026-06-26T21:37:21Z",
          "tree_id": "05aa22f3461078c05e96518d52da4d56655c250f",
          "url": "https://github.com/jasonmcboyd/Arborist/commit/81b46c42702e71ad313d08c636eb3e3c3c35b140"
        },
        "date": 1782511426685,
        "tool": "benchmarkdotnet",
        "benches": [
          {
            "name": "Arborist.Benchmarks.BreadthFirstWhere.TriangleTree_1448",
            "value": 84436161.82142855,
            "unit": "ns",
            "range": "± 245307.4184680759"
          },
          {
            "name": "Arborist.Benchmarks.BreadthFirstWhere.TrivialForest_WhereAll_1M",
            "value": 50397349.79285713,
            "unit": "ns",
            "range": "± 101433.34198169055"
          },
          {
            "name": "Arborist.Benchmarks.BreadthFirstWhere.TrivialForest_WhereNone_1M",
            "value": 6035566.389508928,
            "unit": "ns",
            "range": "± 4305.732479856387"
          },
          {
            "name": "Arborist.Benchmarks.BreadthFirstWhere.DegenerateTree_WhereAll_1M",
            "value": 99020607.47692308,
            "unit": "ns",
            "range": "± 343931.94166507863"
          },
          {
            "name": "Arborist.Benchmarks.BreadthFirstWhere.DegenerateTree_WhereNone_1M",
            "value": 21936143.072916668,
            "unit": "ns",
            "range": "± 376216.9046310704"
          },
          {
            "name": "Arborist.Benchmarks.DepthFirstWhere.TriangleTree_PruneAfter_1448",
            "value": 74772889.63809521,
            "unit": "ns",
            "range": "± 1329681.1991101915"
          },
          {
            "name": "Arborist.Benchmarks.DepthFirstWhere.WhereAll_TrivialForest_1M",
            "value": 41533402.11282052,
            "unit": "ns",
            "range": "± 131185.9979280026"
          },
          {
            "name": "Arborist.Benchmarks.DepthFirstWhere.WhereNone_TrivialForest_1M",
            "value": 7976882.59375,
            "unit": "ns",
            "range": "± 10927.061527179438"
          },
          {
            "name": "Arborist.Benchmarks.DepthFirstWhere.WhereAll_DegenerateTree_1M",
            "value": 73167542.57142857,
            "unit": "ns",
            "range": "± 636414.5394141572"
          },
          {
            "name": "Arborist.Benchmarks.DepthFirstWhere.WhereNone_DegenerateTree_1M",
            "value": 17897596.872916665,
            "unit": "ns",
            "range": "± 21676.611554101808"
          },
          {
            "name": "Arborist.Benchmarks.Invert.TriangleTree_1448",
            "value": 66372982.27380954,
            "unit": "ns",
            "range": "± 568343.0286635737"
          },
          {
            "name": "Arborist.Benchmarks.Invert.DegenerateTree_1M",
            "value": 35040317.70222222,
            "unit": "ns",
            "range": "± 551271.2347783031"
          },
          {
            "name": "Arborist.Benchmarks.LeaffixAggregate.TriangleTree_1448",
            "value": 72708147.03333333,
            "unit": "ns",
            "range": "± 478748.09504319454"
          },
          {
            "name": "Arborist.Benchmarks.LeaffixScan.TriangleTree_1448",
            "value": 76467951.42222223,
            "unit": "ns",
            "range": "± 1177948.8201102982"
          },
          {
            "name": "Arborist.Benchmarks.LeaffixAggregate.DegenerateTree_1M",
            "value": 44307789.2888889,
            "unit": "ns",
            "range": "± 488333.27991833095"
          },
          {
            "name": "Arborist.Benchmarks.LeaffixScan.DegenerateTree_1M",
            "value": 42347201.682051286,
            "unit": "ns",
            "range": "± 757050.3218057004"
          },
          {
            "name": "Arborist.Benchmarks.LeaffixAggregate.TrivialForest_1M",
            "value": 18569587.014583334,
            "unit": "ns",
            "range": "± 52616.52067859344"
          },
          {
            "name": "Arborist.Benchmarks.Materialize.TriangleTree_1448",
            "value": 65126647.25510204,
            "unit": "ns",
            "range": "± 586036.7381490428"
          },
          {
            "name": "Arborist.Benchmarks.Materialize.DegenerateTree_1M",
            "value": 30578962.145833332,
            "unit": "ns",
            "range": "± 510212.54524643533"
          },
          {
            "name": "Arborist.Benchmarks.Select.SelectComposition",
            "value": 21224908.2875,
            "unit": "ns",
            "range": "± 135184.64530931346"
          },
          {
            "name": "Arborist.Benchmarks.PruneAfter.Bft_TrivialForest_1M",
            "value": 11632017.541666666,
            "unit": "ns",
            "range": "± 118688.61489353814"
          },
          {
            "name": "Arborist.Benchmarks.PruneBefore.Bft_TrivialForest_1M",
            "value": 5718507.128348215,
            "unit": "ns",
            "range": "± 5128.570105304559"
          },
          {
            "name": "Arborist.Benchmarks.PruneAfter.Dft_TrivialForest_1M",
            "value": 11833402.434151785,
            "unit": "ns",
            "range": "± 35609.564713931206"
          },
          {
            "name": "Arborist.Benchmarks.PruneBefore.Dft_TrivialForest_1M",
            "value": 8511664.556490384,
            "unit": "ns",
            "range": "± 11524.570613600408"
          },
          {
            "name": "Arborist.Benchmarks.AllNodes.Bft_CompleteBinaryTree_PruneBefore_19",
            "value": 80929700.97777776,
            "unit": "ns",
            "range": "± 328522.21745738806"
          },
          {
            "name": "Arborist.Benchmarks.AnyNodes.Bft_CompleteBinaryTree_PruneBefore_19",
            "value": 81060430.9047619,
            "unit": "ns",
            "range": "± 214568.64355789853"
          },
          {
            "name": "Arborist.Benchmarks.CountNodes.DeepTree",
            "value": 18384313.96875,
            "unit": "ns",
            "range": "± 290856.5766884448"
          },
          {
            "name": "Arborist.Benchmarks.GetLeaves.DeepTree",
            "value": 12763860.352430556,
            "unit": "ns",
            "range": "± 352047.551098284"
          },
          {
            "name": "Arborist.Benchmarks.AllNodes.Dft_CompleteBinaryTree_PruneBefore_19",
            "value": 53615178.571428575,
            "unit": "ns",
            "range": "± 165085.7330883224"
          },
          {
            "name": "Arborist.Benchmarks.AnyNodes.Dft_CompleteBinaryTree_PruneBefore_19",
            "value": 51369076.623076916,
            "unit": "ns",
            "range": "± 121096.82019193855"
          },
          {
            "name": "Arborist.Benchmarks.CountNodes.TriangleTree_PruneAfter_2048",
            "value": 45115239.054545455,
            "unit": "ns",
            "range": "± 226695.49550073047"
          },
          {
            "name": "Arborist.Benchmarks.GetLeaves.CompleteBinaryTree_PruneBefore_20",
            "value": 109851974.83076923,
            "unit": "ns",
            "range": "± 384389.1630731452"
          },
          {
            "name": "Arborist.Benchmarks.AllNodes.Bft_TriangleTree_PruneBefore_19",
            "value": 109763636.49230768,
            "unit": "ns",
            "range": "± 304146.37044272426"
          },
          {
            "name": "Arborist.Benchmarks.AnyNodes.Bft_TriangleTree_PruneBefore_19",
            "value": 111868557.55714284,
            "unit": "ns",
            "range": "± 218587.19627675167"
          },
          {
            "name": "Arborist.Benchmarks.CountNodes.CompleteBinaryTree_PruneAfter_20",
            "value": 58789532.28571428,
            "unit": "ns",
            "range": "± 370826.4847502267"
          },
          {
            "name": "Arborist.Benchmarks.AllNodes.Dft_TriangleTree_PruneBefore_19",
            "value": 56316181.75213676,
            "unit": "ns",
            "range": "± 139797.70053323792"
          },
          {
            "name": "Arborist.Benchmarks.AnyNodes.Dft_TriangleTree_PruneBefore_19",
            "value": 56197287.38518519,
            "unit": "ns",
            "range": "± 478904.22742834396"
          },
          {
            "name": "Arborist.Benchmarks.SkipAllNodes.Bft_TriangleTree_1448",
            "value": 22294693.839583334,
            "unit": "ns",
            "range": "± 62192.75199116113"
          },
          {
            "name": "Arborist.Benchmarks.SkipAllNodes.Dft_TriangleTree_1448",
            "value": 23859493.964583334,
            "unit": "ns",
            "range": "± 79324.26834548717"
          }
        ]
      },
      {
        "commit": {
          "author": {
            "email": "jason.boyd.ce@gmail.com",
            "name": "Jason Boyd",
            "username": "jasonmcboyd"
          },
          "committer": {
            "email": "jason.boyd.ce@gmail.com",
            "name": "Jason Boyd",
            "username": "jasonmcboyd"
          },
          "distinct": true,
          "id": "ab94b3983058299be15f1b4d38b05b21509cb8ad",
          "message": "Encapsulate BFT state in BreadthFirstPath; mirror the DFT driver/state split\n\nMove the breadth-first engine's state -- the visit queue, the schedule stack,\nthe owed-return-visit carry, and the root sibling counter -- into a new\nBreadthFirstPath, leaving BreadthFirstTreenumerator a thin driver, mirroring the\ndepth-first DepthFirstPath split. The cohesive Frame (visit state + child\nenumerator) is kept -- the BFT keeps full state for every resident node anyway,\nso it costs no memory -- so allocation is unchanged from the cohesion engine.\n\nLike DepthFirstPath, BreadthFirstPath is \"sans-I/O\": it never pulls a child; it\nexposes the two active enumerators (the schedule-stack top and the visit-queue\nfront) by ref for the driver to advance. That isolates the engine's asynchronous\noperations to those seams, so a future async BFT can share this class and differ\nonly there. It is a mutable struct embedded as the driver's single _Path field\n(never copied; refs it returns point into the heap deques), keeping dense\ntraversal at the cohesion engine's speed. The two child-pull sites collapse into\none TryScheduleNextChildOf(ref parent).\n\nNo behavior change: same (Mode, Node, VisitCount, Position) visit stream.\n\nValidation:\n- Exact-order traversal + exhaustive DFT-vs-BFT multiset scan (438/0)\n- Full Arborist.Linq suite incl. Where (14,759/0)\n- Benchmarks (Release/Job.Default, same session): TriangleTree 194.5 vs cohesion\n  193.9 ms; CompleteBinaryTree 208.1 vs 213.0 ms (parity). Allocation identical\n  to the cohesion engine (same Frame).\n\nCo-Authored-By: Claude Opus 4.8 (1M context) <noreply@anthropic.com>\nClaude-Session: https://claude.ai/code/session_01Wg3xArL4FATQaXQMBvhXdg",
          "timestamp": "2026-06-27T00:09:54Z",
          "tree_id": "8703f7198030df75b83a7026b75dc74f78b17190",
          "url": "https://github.com/jasonmcboyd/Arborist/commit/ab94b3983058299be15f1b4d38b05b21509cb8ad"
        },
        "date": 1782521634890,
        "tool": "benchmarkdotnet",
        "benches": [
          {
            "name": "Arborist.Benchmarks.BreadthFirstWhere.TriangleTree_1448",
            "value": 80859823.8265306,
            "unit": "ns",
            "range": "± 89970.07663873887"
          },
          {
            "name": "Arborist.Benchmarks.BreadthFirstWhere.TrivialForest_WhereAll_1M",
            "value": 50663074.06428572,
            "unit": "ns",
            "range": "± 168923.64029515185"
          },
          {
            "name": "Arborist.Benchmarks.BreadthFirstWhere.TrivialForest_WhereNone_1M",
            "value": 6100393.2609375,
            "unit": "ns",
            "range": "± 5803.811301601489"
          },
          {
            "name": "Arborist.Benchmarks.BreadthFirstWhere.DegenerateTree_WhereAll_1M",
            "value": 89303927.36904763,
            "unit": "ns",
            "range": "± 578768.3449340455"
          },
          {
            "name": "Arborist.Benchmarks.BreadthFirstWhere.DegenerateTree_WhereNone_1M",
            "value": 22311005.061458334,
            "unit": "ns",
            "range": "± 655188.956230174"
          },
          {
            "name": "Arborist.Benchmarks.DepthFirstWhere.TriangleTree_PruneAfter_1448",
            "value": 82081104.15306124,
            "unit": "ns",
            "range": "± 257049.56671333744"
          },
          {
            "name": "Arborist.Benchmarks.DepthFirstWhere.WhereAll_TrivialForest_1M",
            "value": 42756534.10714285,
            "unit": "ns",
            "range": "± 538246.1817339357"
          },
          {
            "name": "Arborist.Benchmarks.DepthFirstWhere.WhereNone_TrivialForest_1M",
            "value": 7970618.809895833,
            "unit": "ns",
            "range": "± 10033.515787510936"
          },
          {
            "name": "Arborist.Benchmarks.DepthFirstWhere.WhereAll_DegenerateTree_1M",
            "value": 72045805.9120879,
            "unit": "ns",
            "range": "± 200816.41131397046"
          },
          {
            "name": "Arborist.Benchmarks.DepthFirstWhere.WhereNone_DegenerateTree_1M",
            "value": 17622837.846153848,
            "unit": "ns",
            "range": "± 18593.163201859617"
          },
          {
            "name": "Arborist.Benchmarks.Invert.TriangleTree_1448",
            "value": 72592823.89285715,
            "unit": "ns",
            "range": "± 173154.5636648992"
          },
          {
            "name": "Arborist.Benchmarks.Invert.DegenerateTree_1M",
            "value": 33293904.589285713,
            "unit": "ns",
            "range": "± 155732.81873817364"
          },
          {
            "name": "Arborist.Benchmarks.LeaffixAggregate.TriangleTree_1448",
            "value": 76139230.5904762,
            "unit": "ns",
            "range": "± 237296.41991648453"
          },
          {
            "name": "Arborist.Benchmarks.LeaffixScan.TriangleTree_1448",
            "value": 81352901.93589744,
            "unit": "ns",
            "range": "± 200417.2093519173"
          },
          {
            "name": "Arborist.Benchmarks.LeaffixAggregate.DegenerateTree_1M",
            "value": 43312013.81666667,
            "unit": "ns",
            "range": "± 781377.5576949168"
          },
          {
            "name": "Arborist.Benchmarks.LeaffixScan.DegenerateTree_1M",
            "value": 40860955.3800905,
            "unit": "ns",
            "range": "± 798326.8847450333"
          },
          {
            "name": "Arborist.Benchmarks.LeaffixAggregate.TrivialForest_1M",
            "value": 19093959.954166666,
            "unit": "ns",
            "range": "± 87169.1659855418"
          },
          {
            "name": "Arborist.Benchmarks.Materialize.TriangleTree_1448",
            "value": 67366941.0888889,
            "unit": "ns",
            "range": "± 221266.43899175042"
          },
          {
            "name": "Arborist.Benchmarks.Materialize.DegenerateTree_1M",
            "value": 29088760.310416665,
            "unit": "ns",
            "range": "± 125726.27028834652"
          },
          {
            "name": "Arborist.Benchmarks.Select.SelectComposition",
            "value": 20924132.420833334,
            "unit": "ns",
            "range": "± 131684.8391684268"
          },
          {
            "name": "Arborist.Benchmarks.PruneAfter.Bft_TrivialForest_1M",
            "value": 11826144.25625,
            "unit": "ns",
            "range": "± 56441.080643460875"
          },
          {
            "name": "Arborist.Benchmarks.PruneBefore.Bft_TrivialForest_1M",
            "value": 5718144.288461538,
            "unit": "ns",
            "range": "± 5339.311224917629"
          },
          {
            "name": "Arborist.Benchmarks.PruneAfter.Dft_TrivialForest_1M",
            "value": 12128254.361458333,
            "unit": "ns",
            "range": "± 73413.14840604202"
          },
          {
            "name": "Arborist.Benchmarks.PruneBefore.Dft_TrivialForest_1M",
            "value": 8644879.70424107,
            "unit": "ns",
            "range": "± 67904.57740778245"
          },
          {
            "name": "Arborist.Benchmarks.AllNodes.Bft_CompleteBinaryTree_PruneBefore_19",
            "value": 82016475.42222223,
            "unit": "ns",
            "range": "± 241443.642500885"
          },
          {
            "name": "Arborist.Benchmarks.AnyNodes.Bft_CompleteBinaryTree_PruneBefore_19",
            "value": 80540259.97959185,
            "unit": "ns",
            "range": "± 239960.3194413193"
          },
          {
            "name": "Arborist.Benchmarks.CountNodes.DeepTree",
            "value": 17884249.827083334,
            "unit": "ns",
            "range": "± 54673.15886207391"
          },
          {
            "name": "Arborist.Benchmarks.GetLeaves.DeepTree",
            "value": 10054555.78013393,
            "unit": "ns",
            "range": "± 13320.131817429356"
          },
          {
            "name": "Arborist.Benchmarks.AllNodes.Dft_CompleteBinaryTree_PruneBefore_19",
            "value": 52832416.39285714,
            "unit": "ns",
            "range": "± 179677.48747853987"
          },
          {
            "name": "Arborist.Benchmarks.AnyNodes.Dft_CompleteBinaryTree_PruneBefore_19",
            "value": 52757849.96428572,
            "unit": "ns",
            "range": "± 215244.7639431353"
          },
          {
            "name": "Arborist.Benchmarks.CountNodes.TriangleTree_PruneAfter_2048",
            "value": 45632160.70129871,
            "unit": "ns",
            "range": "± 140262.88300591477"
          },
          {
            "name": "Arborist.Benchmarks.GetLeaves.CompleteBinaryTree_PruneBefore_20",
            "value": 116764184.41333334,
            "unit": "ns",
            "range": "± 842543.0561769464"
          },
          {
            "name": "Arborist.Benchmarks.AllNodes.Bft_TriangleTree_PruneBefore_19",
            "value": 110751332.67692305,
            "unit": "ns",
            "range": "± 108636.4733085088"
          },
          {
            "name": "Arborist.Benchmarks.AnyNodes.Bft_TriangleTree_PruneBefore_19",
            "value": 108769106.3,
            "unit": "ns",
            "range": "± 147601.2648739475"
          },
          {
            "name": "Arborist.Benchmarks.CountNodes.CompleteBinaryTree_PruneAfter_20",
            "value": 61075038.693877555,
            "unit": "ns",
            "range": "± 105948.89355418464"
          },
          {
            "name": "Arborist.Benchmarks.AllNodes.Dft_TriangleTree_PruneBefore_19",
            "value": 61949963.067307696,
            "unit": "ns",
            "range": "± 86168.52129129085"
          },
          {
            "name": "Arborist.Benchmarks.AnyNodes.Dft_TriangleTree_PruneBefore_19",
            "value": 61894683.208333336,
            "unit": "ns",
            "range": "± 262945.6938829378"
          },
          {
            "name": "Arborist.Benchmarks.SkipAllNodes.Bft_TriangleTree_1448",
            "value": 22520551.360416666,
            "unit": "ns",
            "range": "± 55213.66901600766"
          },
          {
            "name": "Arborist.Benchmarks.SkipAllNodes.Dft_TriangleTree_1448",
            "value": 56819582.67407407,
            "unit": "ns",
            "range": "± 253099.77745497008"
          }
        ]
      },
      {
        "commit": {
          "author": {
            "email": "jason.boyd.ce@gmail.com",
            "name": "Jason Boyd",
            "username": "jasonmcboyd"
          },
          "committer": {
            "email": "jason.boyd.ce@gmail.com",
            "name": "Jason Boyd",
            "username": "jasonmcboyd"
          },
          "distinct": true,
          "id": "6ebd5d0e1d60592672f966eaa4ab81a302c56999",
          "message": "Fix DFT skip-heavy regression: keep TryPushNextChild out-of-line\n\nThe encapsulated DepthFirstPath DFT (14f8393) ran ~1.7-1.9x slower than the\noriginal two-stack on promotion-heavy skip traversal (SkipAllNodes / Preorder /\nPostorder on wide trees), despite identical allocation. JIT disassembly\n(DOTNET_JitDisasm) pinned the cause: [AggressiveInlining] on TryPushNextChild\ninlined the entire promote body (pull + push) into OnMoveNext/OnScheduling,\ninflating their frames to 6 callee-saved registers + sub rsp,72 + vzeroupper and\ndowngrading OnMoveNext's branch dispatch from a tail-jmp to a call+teardown paid\non EVERY node. The original two-stack stayed fast precisely because its promote\nwas a separate, out-of-line method.\n\nMark TryPushNextChild [NoInlining] so the drivers stay thin tail-dispatchers,\nwhile keeping the push chain (PushChild/PushLevel) force-inlined INTO it so the\npush itself is still call-free. Also fold Backtrack's pop + three predicate\nchecks into one DepthFirstPath.PopFinishedLevelAndClassify call: the original\ninline-predicate form is O(1) per level but its repeated struct round-trips cost\n~2x on the deep-unwind path (GetLeaves.DeepTree); folding restores it.\n\nNet (Release/Job.Default, local, vs original two-stack): SkipAllNodes.Dft\n41 -> 22.6 ms, Preorder 42.8 -> 25.7, Postorder 47 -> 32.9 -- all back at the\noriginal; dense traversal and GetLeaves.DeepTree within noise; allocation\nunchanged. The sans-I/O encapsulation (path never pulls; one ref seam) is intact.\n\nValidation: 438/0 oracle (exact-order + exhaustive DFT-vs-BFT scan), 14,759/0\nfull Linq suite.\n\nCo-Authored-By: Claude Opus 4.8 (1M context) <noreply@anthropic.com>\nClaude-Session: https://claude.ai/code/session_01Wg3xArL4FATQaXQMBvhXdg",
          "timestamp": "2026-06-27T02:26:54Z",
          "tree_id": "5301780378953d2f586361d71d9e7e7b8dc1e6d3",
          "url": "https://github.com/jasonmcboyd/Arborist/commit/6ebd5d0e1d60592672f966eaa4ab81a302c56999"
        },
        "date": 1782529879104,
        "tool": "benchmarkdotnet",
        "benches": [
          {
            "name": "Arborist.Benchmarks.BreadthFirstWhere.TriangleTree_1448",
            "value": 79926416.70238097,
            "unit": "ns",
            "range": "± 201431.57857936525"
          },
          {
            "name": "Arborist.Benchmarks.BreadthFirstWhere.TrivialForest_WhereAll_1M",
            "value": 50714670.37692308,
            "unit": "ns",
            "range": "± 95624.38452055625"
          },
          {
            "name": "Arborist.Benchmarks.BreadthFirstWhere.TrivialForest_WhereNone_1M",
            "value": 6192708.990104167,
            "unit": "ns",
            "range": "± 9305.556080235421"
          },
          {
            "name": "Arborist.Benchmarks.BreadthFirstWhere.DegenerateTree_WhereAll_1M",
            "value": 94476937.20238094,
            "unit": "ns",
            "range": "± 280865.1615523171"
          },
          {
            "name": "Arborist.Benchmarks.BreadthFirstWhere.DegenerateTree_WhereNone_1M",
            "value": 22948620.249327958,
            "unit": "ns",
            "range": "± 1288534.94587461"
          },
          {
            "name": "Arborist.Benchmarks.DepthFirstWhere.TriangleTree_PruneAfter_1448",
            "value": 71264426.58095238,
            "unit": "ns",
            "range": "± 355421.3029454377"
          },
          {
            "name": "Arborist.Benchmarks.DepthFirstWhere.WhereAll_TrivialForest_1M",
            "value": 41787511.26282051,
            "unit": "ns",
            "range": "± 156611.9320917289"
          },
          {
            "name": "Arborist.Benchmarks.DepthFirstWhere.WhereNone_TrivialForest_1M",
            "value": 8000448.324776785,
            "unit": "ns",
            "range": "± 24745.74548756454"
          },
          {
            "name": "Arborist.Benchmarks.DepthFirstWhere.WhereAll_DegenerateTree_1M",
            "value": 74010382.29523809,
            "unit": "ns",
            "range": "± 1249288.533630083"
          },
          {
            "name": "Arborist.Benchmarks.DepthFirstWhere.WhereNone_DegenerateTree_1M",
            "value": 17925319.11830357,
            "unit": "ns",
            "range": "± 21397.729733117656"
          },
          {
            "name": "Arborist.Benchmarks.Invert.TriangleTree_1448",
            "value": 73242756.05555555,
            "unit": "ns",
            "range": "± 195736.67763225883"
          },
          {
            "name": "Arborist.Benchmarks.Invert.DegenerateTree_1M",
            "value": 33570099.352380954,
            "unit": "ns",
            "range": "± 209368.2101540893"
          },
          {
            "name": "Arborist.Benchmarks.LeaffixAggregate.TriangleTree_1448",
            "value": 77705648.47619048,
            "unit": "ns",
            "range": "± 109958.09723149848"
          },
          {
            "name": "Arborist.Benchmarks.LeaffixScan.TriangleTree_1448",
            "value": 79282247.61904761,
            "unit": "ns",
            "range": "± 118734.43003257344"
          },
          {
            "name": "Arborist.Benchmarks.LeaffixAggregate.DegenerateTree_1M",
            "value": 43085583.54444445,
            "unit": "ns",
            "range": "± 678835.767374109"
          },
          {
            "name": "Arborist.Benchmarks.LeaffixScan.DegenerateTree_1M",
            "value": 41286209.54358973,
            "unit": "ns",
            "range": "± 544256.8395616808"
          },
          {
            "name": "Arborist.Benchmarks.LeaffixAggregate.TrivialForest_1M",
            "value": 18810718.395089287,
            "unit": "ns",
            "range": "± 35312.547002131345"
          },
          {
            "name": "Arborist.Benchmarks.Materialize.TriangleTree_1448",
            "value": 68774878.02380952,
            "unit": "ns",
            "range": "± 170779.74051835333"
          },
          {
            "name": "Arborist.Benchmarks.Materialize.DegenerateTree_1M",
            "value": 29156874.439583335,
            "unit": "ns",
            "range": "± 186031.41628088115"
          },
          {
            "name": "Arborist.Benchmarks.Select.SelectComposition",
            "value": 16742419.028846154,
            "unit": "ns",
            "range": "± 50005.77951167795"
          },
          {
            "name": "Arborist.Benchmarks.PruneAfter.Bft_TrivialForest_1M",
            "value": 11432222.9765625,
            "unit": "ns",
            "range": "± 52103.174920327416"
          },
          {
            "name": "Arborist.Benchmarks.PruneBefore.Bft_TrivialForest_1M",
            "value": 5712196.378255208,
            "unit": "ns",
            "range": "± 2711.991395364224"
          },
          {
            "name": "Arborist.Benchmarks.PruneAfter.Dft_TrivialForest_1M",
            "value": 11710402.3875,
            "unit": "ns",
            "range": "± 100803.4776251746"
          },
          {
            "name": "Arborist.Benchmarks.PruneBefore.Dft_TrivialForest_1M",
            "value": 8535813.205208333,
            "unit": "ns",
            "range": "± 27740.1585513272"
          },
          {
            "name": "Arborist.Benchmarks.AllNodes.Bft_CompleteBinaryTree_PruneBefore_19",
            "value": 80895581.62820514,
            "unit": "ns",
            "range": "± 290011.09717080457"
          },
          {
            "name": "Arborist.Benchmarks.AnyNodes.Bft_CompleteBinaryTree_PruneBefore_19",
            "value": 80395007.19999999,
            "unit": "ns",
            "range": "± 342421.7725583297"
          },
          {
            "name": "Arborist.Benchmarks.CountNodes.DeepTree",
            "value": 18071944.525,
            "unit": "ns",
            "range": "± 71489.88385147612"
          },
          {
            "name": "Arborist.Benchmarks.GetLeaves.DeepTree",
            "value": 24225525.3125,
            "unit": "ns",
            "range": "± 69065.67117396861"
          },
          {
            "name": "Arborist.Benchmarks.AllNodes.Dft_CompleteBinaryTree_PruneBefore_19",
            "value": 56048150.55555557,
            "unit": "ns",
            "range": "± 100192.16751248082"
          },
          {
            "name": "Arborist.Benchmarks.AnyNodes.Dft_CompleteBinaryTree_PruneBefore_19",
            "value": 55767885.95370371,
            "unit": "ns",
            "range": "± 119896.26504067377"
          },
          {
            "name": "Arborist.Benchmarks.CountNodes.TriangleTree_PruneAfter_2048",
            "value": 46035867.62237762,
            "unit": "ns",
            "range": "± 117397.43901828038"
          },
          {
            "name": "Arborist.Benchmarks.GetLeaves.CompleteBinaryTree_PruneBefore_20",
            "value": 114281274.30666664,
            "unit": "ns",
            "range": "± 378891.9882408754"
          },
          {
            "name": "Arborist.Benchmarks.AllNodes.Bft_TriangleTree_PruneBefore_19",
            "value": 110704443.5846154,
            "unit": "ns",
            "range": "± 418256.8088822835"
          },
          {
            "name": "Arborist.Benchmarks.AnyNodes.Bft_TriangleTree_PruneBefore_19",
            "value": 109946121.55000001,
            "unit": "ns",
            "range": "± 321616.1633990314"
          },
          {
            "name": "Arborist.Benchmarks.CountNodes.CompleteBinaryTree_PruneAfter_20",
            "value": 60709710.45714285,
            "unit": "ns",
            "range": "± 243618.50803859503"
          },
          {
            "name": "Arborist.Benchmarks.AllNodes.Dft_TriangleTree_PruneBefore_19",
            "value": 65741307.825,
            "unit": "ns",
            "range": "± 801085.2390205538"
          },
          {
            "name": "Arborist.Benchmarks.AnyNodes.Dft_TriangleTree_PruneBefore_19",
            "value": 64325928.83035714,
            "unit": "ns",
            "range": "± 94627.02731853774"
          },
          {
            "name": "Arborist.Benchmarks.SkipAllNodes.Bft_TriangleTree_1448",
            "value": 22658528.735416666,
            "unit": "ns",
            "range": "± 36818.72384729954"
          },
          {
            "name": "Arborist.Benchmarks.SkipAllNodes.Dft_TriangleTree_1448",
            "value": 31467941.27403846,
            "unit": "ns",
            "range": "± 72301.29130831141"
          }
        ]
      },
      {
        "commit": {
          "author": {
            "email": "jason.boyd.ce@gmail.com",
            "name": "Jason Boyd",
            "username": "jasonmcboyd"
          },
          "committer": {
            "email": "jason.boyd.ce@gmail.com",
            "name": "Jason Boyd",
            "username": "jasonmcboyd"
          },
          "distinct": true,
          "id": "e8bbc30ca40332e6f77c535d2a6e5edf419feb67",
          "message": "Revert DFT Backtrack consolidation (GetLeaves.DeepTree deep-unwind cost)\n\n6ebd5d0 fixed the wide-skip-traversal regression by making TryPushNextChild\nout-of-line, but it also folded Backtrack's pop + three predicate checks into one\nDepthFirstPath.PopFinishedLevelAndClassify call. That consolidation, fine on wide\ntrees, added one call per unwound level -- ~262K calls on the deep-unwind path --\nand regressed GetLeaves.DeepTree from ~10ms to ~24ms on the CI runner (a cost\nlocal benchmarks under-reported, due to cache differences).\n\nRevert the consolidation back to the original two-stack's inline-predicate\nBacktrack, keeping the out-of-line TryPushNextChild that fixed wide skip. The DFT\nis now structurally identical to the original two-stack (inline Backtrack +\nseparate promote method), just encapsulated in DepthFirstPath -- the form the CI\nshows is fast on every tree shape.\n\nNet (vs original two-stack, local same-session): SkipAllNodes.Dft 22.7 vs 22.0;\nGetLeaves.DeepTree 10.7 vs 8.8 (the residual +22% is the out-of-line promote's\nper-node call -- the irreducible cost of keeping wide skip fast; CI will confirm\nGetLeaves is well below 6ebd5d0's 24ms). Allocation unchanged.\n\nValidation: 438/0 oracle (exact-order + exhaustive DFT-vs-BFT scan), 14,759/0 Linq.\n\nCo-Authored-By: Claude Opus 4.8 (1M context) <noreply@anthropic.com>\nClaude-Session: https://claude.ai/code/session_01Wg3xArL4FATQaXQMBvhXdg",
          "timestamp": "2026-06-27T03:58:38Z",
          "tree_id": "a3f9dd64f7a624f48c68d521c40c1a096d4fa152",
          "url": "https://github.com/jasonmcboyd/Arborist/commit/e8bbc30ca40332e6f77c535d2a6e5edf419feb67"
        },
        "date": 1782534425587,
        "tool": "benchmarkdotnet",
        "benches": [
          {
            "name": "Arborist.Benchmarks.BreadthFirstWhere.TriangleTree_1448",
            "value": 77969041.73809524,
            "unit": "ns",
            "range": "± 73990.52636577345"
          },
          {
            "name": "Arborist.Benchmarks.BreadthFirstWhere.TrivialForest_WhereAll_1M",
            "value": 50707496.24615385,
            "unit": "ns",
            "range": "± 115467.37748842385"
          },
          {
            "name": "Arborist.Benchmarks.BreadthFirstWhere.TrivialForest_WhereNone_1M",
            "value": 6267071.013950893,
            "unit": "ns",
            "range": "± 6429.168670274477"
          },
          {
            "name": "Arborist.Benchmarks.BreadthFirstWhere.DegenerateTree_WhereAll_1M",
            "value": 94244137.27777776,
            "unit": "ns",
            "range": "± 1373413.880326981"
          },
          {
            "name": "Arborist.Benchmarks.BreadthFirstWhere.DegenerateTree_WhereNone_1M",
            "value": 21179655.111111112,
            "unit": "ns",
            "range": "± 349740.7825247566"
          },
          {
            "name": "Arborist.Benchmarks.DepthFirstWhere.TriangleTree_PruneAfter_1448",
            "value": 75002451.43956044,
            "unit": "ns",
            "range": "± 204996.26867985062"
          },
          {
            "name": "Arborist.Benchmarks.DepthFirstWhere.WhereAll_TrivialForest_1M",
            "value": 41892968.86263736,
            "unit": "ns",
            "range": "± 178483.70511895887"
          },
          {
            "name": "Arborist.Benchmarks.DepthFirstWhere.WhereNone_TrivialForest_1M",
            "value": 7960935.819196428,
            "unit": "ns",
            "range": "± 6993.954625867665"
          },
          {
            "name": "Arborist.Benchmarks.DepthFirstWhere.WhereAll_DegenerateTree_1M",
            "value": 71859530.1904762,
            "unit": "ns",
            "range": "± 596284.028098733"
          },
          {
            "name": "Arborist.Benchmarks.DepthFirstWhere.WhereNone_DegenerateTree_1M",
            "value": 17907018.190104168,
            "unit": "ns",
            "range": "± 46870.55053351424"
          },
          {
            "name": "Arborist.Benchmarks.Invert.TriangleTree_1448",
            "value": 73016393.84615386,
            "unit": "ns",
            "range": "± 335046.22763730626"
          },
          {
            "name": "Arborist.Benchmarks.Invert.DegenerateTree_1M",
            "value": 32595526.77619047,
            "unit": "ns",
            "range": "± 271260.9258796437"
          },
          {
            "name": "Arborist.Benchmarks.LeaffixAggregate.TriangleTree_1448",
            "value": 76977528.9010989,
            "unit": "ns",
            "range": "± 191779.8417935202"
          },
          {
            "name": "Arborist.Benchmarks.LeaffixScan.TriangleTree_1448",
            "value": 77985479.25555557,
            "unit": "ns",
            "range": "± 157663.30941408037"
          },
          {
            "name": "Arborist.Benchmarks.LeaffixAggregate.DegenerateTree_1M",
            "value": 43171534.244444445,
            "unit": "ns",
            "range": "± 543754.2537735697"
          },
          {
            "name": "Arborist.Benchmarks.LeaffixScan.DegenerateTree_1M",
            "value": 40990071.512820505,
            "unit": "ns",
            "range": "± 711228.4090507383"
          },
          {
            "name": "Arborist.Benchmarks.LeaffixAggregate.TrivialForest_1M",
            "value": 18798644.588541668,
            "unit": "ns",
            "range": "± 29991.87905443242"
          },
          {
            "name": "Arborist.Benchmarks.Materialize.TriangleTree_1448",
            "value": 68192608.9404762,
            "unit": "ns",
            "range": "± 206804.4409539197"
          },
          {
            "name": "Arborist.Benchmarks.Materialize.DegenerateTree_1M",
            "value": 29614761.216666665,
            "unit": "ns",
            "range": "± 149752.53367283387"
          },
          {
            "name": "Arborist.Benchmarks.Select.SelectComposition",
            "value": 21448319.799479168,
            "unit": "ns",
            "range": "± 51964.10808226422"
          },
          {
            "name": "Arborist.Benchmarks.PruneAfter.Bft_TrivialForest_1M",
            "value": 12069866.140625,
            "unit": "ns",
            "range": "± 53634.75284222665"
          },
          {
            "name": "Arborist.Benchmarks.PruneBefore.Bft_TrivialForest_1M",
            "value": 5722004.806490385,
            "unit": "ns",
            "range": "± 7365.842950915725"
          },
          {
            "name": "Arborist.Benchmarks.PruneAfter.Dft_TrivialForest_1M",
            "value": 11605180.380208334,
            "unit": "ns",
            "range": "± 129542.43506846976"
          },
          {
            "name": "Arborist.Benchmarks.PruneBefore.Dft_TrivialForest_1M",
            "value": 8500877.168526785,
            "unit": "ns",
            "range": "± 43787.47627598871"
          },
          {
            "name": "Arborist.Benchmarks.AllNodes.Bft_CompleteBinaryTree_PruneBefore_19",
            "value": 81359376.76666667,
            "unit": "ns",
            "range": "± 281814.3353853227"
          },
          {
            "name": "Arborist.Benchmarks.AnyNodes.Bft_CompleteBinaryTree_PruneBefore_19",
            "value": 80718416.07142857,
            "unit": "ns",
            "range": "± 205969.86066818776"
          },
          {
            "name": "Arborist.Benchmarks.CountNodes.DeepTree",
            "value": 18027734.802884616,
            "unit": "ns",
            "range": "± 61213.056434278005"
          },
          {
            "name": "Arborist.Benchmarks.GetLeaves.DeepTree",
            "value": 24293545.015625,
            "unit": "ns",
            "range": "± 110581.46231609126"
          },
          {
            "name": "Arborist.Benchmarks.AllNodes.Dft_CompleteBinaryTree_PruneBefore_19",
            "value": 56663836.72592593,
            "unit": "ns",
            "range": "± 397546.00360464136"
          },
          {
            "name": "Arborist.Benchmarks.AnyNodes.Dft_CompleteBinaryTree_PruneBefore_19",
            "value": 56576164.666666664,
            "unit": "ns",
            "range": "± 198599.47272215172"
          },
          {
            "name": "Arborist.Benchmarks.CountNodes.TriangleTree_PruneAfter_2048",
            "value": 46165023.935064934,
            "unit": "ns",
            "range": "± 266714.4311265394"
          },
          {
            "name": "Arborist.Benchmarks.GetLeaves.CompleteBinaryTree_PruneBefore_20",
            "value": 114939043.42666668,
            "unit": "ns",
            "range": "± 549315.7569686014"
          },
          {
            "name": "Arborist.Benchmarks.AllNodes.Bft_TriangleTree_PruneBefore_19",
            "value": 110573112.06666666,
            "unit": "ns",
            "range": "± 680438.1489897127"
          },
          {
            "name": "Arborist.Benchmarks.AnyNodes.Bft_TriangleTree_PruneBefore_19",
            "value": 109478732.01538458,
            "unit": "ns",
            "range": "± 273081.17135797505"
          },
          {
            "name": "Arborist.Benchmarks.CountNodes.CompleteBinaryTree_PruneAfter_20",
            "value": 61233128.48351649,
            "unit": "ns",
            "range": "± 239863.74755846374"
          },
          {
            "name": "Arborist.Benchmarks.AllNodes.Dft_TriangleTree_PruneBefore_19",
            "value": 65570652.373626366,
            "unit": "ns",
            "range": "± 218251.88314609037"
          },
          {
            "name": "Arborist.Benchmarks.AnyNodes.Dft_TriangleTree_PruneBefore_19",
            "value": 64788551.6875,
            "unit": "ns",
            "range": "± 148246.7351928235"
          },
          {
            "name": "Arborist.Benchmarks.SkipAllNodes.Bft_TriangleTree_1448",
            "value": 20626258.404166665,
            "unit": "ns",
            "range": "± 58455.396528174446"
          },
          {
            "name": "Arborist.Benchmarks.SkipAllNodes.Dft_TriangleTree_1448",
            "value": 31720217.625,
            "unit": "ns",
            "range": "± 99895.88364766286"
          }
        ]
      }
    ],
    "Conversion Benchmarks": [
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
        "date": 1769975445703,
        "tool": "benchmarkdotnet",
        "benches": [
          {
            "name": "Arborist.Benchmarks.EnumerableToTree.ToDegenerateTree",
            "value": 14532779.056490384,
            "unit": "ns",
            "range": "± 9766.432910244412"
          },
          {
            "name": "Arborist.Benchmarks.EnumerableToTree.ToDegenerateTreeUsingToTree",
            "value": 49469890.23571428,
            "unit": "ns",
            "range": "± 361134.59508236306"
          },
          {
            "name": "Arborist.Benchmarks.EnumerableToTree.ToTrivialForest",
            "value": 3586461.216346154,
            "unit": "ns",
            "range": "± 52588.99896932711"
          },
          {
            "name": "Arborist.Benchmarks.EnumerableToTree.ToTrivialForestUsingToTree",
            "value": 36525244.895604394,
            "unit": "ns",
            "range": "± 149874.0086375628"
          }
        ]
      },
      {
        "commit": {
          "author": {
            "email": "jason.boyd.ce@gmail.com",
            "name": "Jason Boyd",
            "username": "jasonmcboyd"
          },
          "committer": {
            "email": "jason.boyd.ce@gmail.com",
            "name": "Jason Boyd",
            "username": "jasonmcboyd"
          },
          "distinct": true,
          "id": "5993e5762d3c0ffa1bbacbb40b35307cff9a3f87",
          "message": "Docs: document BFT Where O(N) prefix-carry design\n\nRewrite the WhereBreadthFirstTreenumerator section to match the implementation (queue + incremental _PredSkipPrefix, predicate-skip vs consumer-SkipNode, the real parent-visit fields, front-anchored sibling index). Update the DFT-vs-BFT table (BFT now O(N)) and fix stale _ExtraParentVisitsEmitted/_LastRemovedSkippedNodePosition/_SkippedStack references.\n\nCo-Authored-By: Claude Opus 4.8 (1M context) <noreply@anthropic.com>\nClaude-Session: https://claude.ai/code/session_01Wg3xArL4FATQaXQMBvhXdg",
          "timestamp": "2026-06-23T04:33:54Z",
          "tree_id": "a2615b3ce8e91df5baffd5f8f08e3c6ac9df2e3b",
          "url": "https://github.com/jasonmcboyd/Arborist/commit/5993e5762d3c0ffa1bbacbb40b35307cff9a3f87"
        },
        "date": 1782190984408,
        "tool": "benchmarkdotnet",
        "benches": [
          {
            "name": "Arborist.Benchmarks.EnumerableToTree.ToDegenerateTree",
            "value": 14638014.412259616,
            "unit": "ns",
            "range": "± 72637.32449060038"
          },
          {
            "name": "Arborist.Benchmarks.EnumerableToTree.ToDegenerateTreeUsingToTree",
            "value": 51850185.513333336,
            "unit": "ns",
            "range": "± 661380.1398914974"
          },
          {
            "name": "Arborist.Benchmarks.EnumerableToTree.ToTrivialForest",
            "value": 3612099.671316964,
            "unit": "ns",
            "range": "± 3197.066082366272"
          },
          {
            "name": "Arborist.Benchmarks.EnumerableToTree.ToTrivialForestUsingToTree",
            "value": 38423611.76923077,
            "unit": "ns",
            "range": "± 46097.79051302303"
          }
        ]
      },
      {
        "commit": {
          "author": {
            "email": "jason.boyd.ce@gmail.com",
            "name": "Jason Boyd",
            "username": "jasonmcboyd"
          },
          "committer": {
            "email": "jason.boyd.ce@gmail.com",
            "name": "Jason Boyd",
            "username": "jasonmcboyd"
          },
          "distinct": true,
          "id": "c3ab5ef05fc7c7304bb98cd14e97bed3a8f91e62",
          "message": "Align LeaffixAggregate with LeaffixScan (flat, lazy, zero per-node alloc)\n\nLeaffixAggregate now uses the same flat forward DFS + no-copy ChildAccumulations view as LeaffixScan, yielding each root's accumulated value. Accumulator changes TAccumulate[] -> ChildAccumulations<TAccumulate>. Per-root lazy: a root is emitted the moment its subtree completes and the buffers are reused for the next root, so peak memory is the largest root subtree (not the whole forest) and early-terminating consumers traverse fewer roots -- matching the previous LeaffixAggregator behavior. Zero per-node heap allocation. Adds LeaffixAggregateTests (it previously had none and no callers).\n\nLeaffixAggregator is retained -- still used by Invert (TreeInverter); it'll be removed when Invert is redesigned onto a flat structure.\n\nCo-Authored-By: Claude Opus 4.8 (1M context) <noreply@anthropic.com>\nClaude-Session: https://claude.ai/code/session_01Wg3xArL4FATQaXQMBvhXdg",
          "timestamp": "2026-06-23T21:57:19Z",
          "tree_id": "7dacc032f0faefb81f1e004514e835a60b5bf81a",
          "url": "https://github.com/jasonmcboyd/Arborist/commit/c3ab5ef05fc7c7304bb98cd14e97bed3a8f91e62"
        },
        "date": 1782253173911,
        "tool": "benchmarkdotnet",
        "benches": [
          {
            "name": "Arborist.Benchmarks.EnumerableToTree.ToDegenerateTree",
            "value": 14573754.555288462,
            "unit": "ns",
            "range": "± 13293.132476950208"
          },
          {
            "name": "Arborist.Benchmarks.EnumerableToTree.ToDegenerateTreeUsingToTree",
            "value": 50738299.58571428,
            "unit": "ns",
            "range": "± 95223.06699867996"
          },
          {
            "name": "Arborist.Benchmarks.EnumerableToTree.ToTrivialForest",
            "value": 3584682.847916667,
            "unit": "ns",
            "range": "± 30921.729991443008"
          },
          {
            "name": "Arborist.Benchmarks.EnumerableToTree.ToTrivialForestUsingToTree",
            "value": 38935770.702564105,
            "unit": "ns",
            "range": "± 173500.47334169337"
          }
        ]
      },
      {
        "commit": {
          "author": {
            "email": "jason.boyd.ce@gmail.com",
            "name": "Jason Boyd",
            "username": "jasonmcboyd"
          },
          "committer": {
            "email": "jason.boyd.ce@gmail.com",
            "name": "Jason Boyd",
            "username": "jasonmcboyd"
          },
          "distinct": true,
          "id": "96f3cc44e2208f47258c2887c2a011530d708864",
          "message": "Rewrite Invert flat; retire SimpleNode and LeaffixAggregator\n\nInvert now materializes the source into flat pre-order arrays and emits the mirror with a stack -- pushing roots/children in forward order so they pop in reverse (the mirror's pre-order). Subtree sizes are invariant under mirroring; result is a PreorderTree. O(N), zero per-node allocation (was a SimpleNode object per node via LeaffixAggregator).\n\nInvert was the last user of both SimpleNode and LeaffixAggregator, so this deletes five files: TreeInverter, LeaffixAggregator (+NodeContextAndResult), SimpleNode, SimpleNodeChildEnumerator, SimpleNodeTreenumerable. The SimpleNode retirement is complete, clearing the runway for the nodeToValueMap elimination. (A level-order/LOUDS mirror -- reverse each child-run in place -- remains a future refinement if a LevelOrderTree is ever built.)\n\nCo-Authored-By: Claude Opus 4.8 (1M context) <noreply@anthropic.com>\nClaude-Session: https://claude.ai/code/session_01Wg3xArL4FATQaXQMBvhXdg",
          "timestamp": "2026-06-23T22:21:16Z",
          "tree_id": "aafd024b9cc4740d5e6c4fe63bc7cf95158cbb5e",
          "url": "https://github.com/jasonmcboyd/Arborist/commit/96f3cc44e2208f47258c2887c2a011530d708864"
        },
        "date": 1782254668319,
        "tool": "benchmarkdotnet",
        "benches": [
          {
            "name": "Arborist.Benchmarks.EnumerableToTree.ToDegenerateTree",
            "value": 14063708.276041666,
            "unit": "ns",
            "range": "± 24305.76412125506"
          },
          {
            "name": "Arborist.Benchmarks.EnumerableToTree.ToDegenerateTreeUsingToTree",
            "value": 48958505.39285714,
            "unit": "ns",
            "range": "± 300036.0231508247"
          },
          {
            "name": "Arborist.Benchmarks.EnumerableToTree.ToTrivialForest",
            "value": 3478662.358984375,
            "unit": "ns",
            "range": "± 16167.6465315003"
          },
          {
            "name": "Arborist.Benchmarks.EnumerableToTree.ToTrivialForestUsingToTree",
            "value": 35444264.284444444,
            "unit": "ns",
            "range": "± 101538.64510378653"
          }
        ]
      },
      {
        "commit": {
          "author": {
            "email": "jason.boyd.ce@gmail.com",
            "name": "Jason Boyd",
            "username": "jasonmcboyd"
          },
          "committer": {
            "email": "jason.boyd.ce@gmail.com",
            "name": "Jason Boyd",
            "username": "jasonmcboyd"
          },
          "distinct": true,
          "id": "7c795cd25f5b93fbac1993cf6a71485ef8c2de74",
          "message": "Add MemoryDiagnoser benchmark for Invert\n\nCovers TriangleTree depth-1448 (~1M) and a 1M-deep DegenerateTree, tagged LINQ for the dashboard. Completes benchmark coverage of the now-flat transform/aggregation ops (Materialize, LeaffixScan, LeaffixAggregate, Invert).\n\nCo-Authored-By: Claude Opus 4.8 (1M context) <noreply@anthropic.com>\nClaude-Session: https://claude.ai/code/session_01Wg3xArL4FATQaXQMBvhXdg",
          "timestamp": "2026-06-23T22:22:47Z",
          "tree_id": "547984a2584ebd974bf3f4a1790b68fc9b328cfc",
          "url": "https://github.com/jasonmcboyd/Arborist/commit/7c795cd25f5b93fbac1993cf6a71485ef8c2de74"
        },
        "date": 1782256078746,
        "tool": "benchmarkdotnet",
        "benches": [
          {
            "name": "Arborist.Benchmarks.EnumerableToTree.ToDegenerateTree",
            "value": 16144160.46875,
            "unit": "ns",
            "range": "± 88571.46073335288"
          },
          {
            "name": "Arborist.Benchmarks.EnumerableToTree.ToDegenerateTreeUsingToTree",
            "value": 50592255.21333333,
            "unit": "ns",
            "range": "± 777757.4681050602"
          },
          {
            "name": "Arborist.Benchmarks.EnumerableToTree.ToTrivialForest",
            "value": 3827133.416015625,
            "unit": "ns",
            "range": "± 2333.1787772576863"
          },
          {
            "name": "Arborist.Benchmarks.EnumerableToTree.ToTrivialForestUsingToTree",
            "value": 40174012.98224852,
            "unit": "ns",
            "range": "± 112867.59290637204"
          }
        ]
      },
      {
        "commit": {
          "author": {
            "email": "jason.boyd.ce@gmail.com",
            "name": "Jason Boyd",
            "username": "jasonmcboyd"
          },
          "committer": {
            "email": "jason.boyd.ce@gmail.com",
            "name": "Jason Boyd",
            "username": "jasonmcboyd"
          },
          "distinct": true,
          "id": "cf2c06864e8fdc1e6c24df1f1437f0dae1da26b0",
          "message": "Add 2-param Treenumerable base; drop redundant identity maps (map cleanup stage 1)\n\nThe sample trees whose node IS their surfaced value (TriangleTree, CollatzTree, CompleteBinaryTree, NDecrementTree, DeepTree) were each passing a redundant 'node => node' nodeToValueMap and carrying a duplicate type parameter. Add a 2-param Treenumerable<TNode, TChildEnumerator> convenience base (identity map) and migrate them to it. Public surfaces unchanged. The 3-param base + map remain for PreorderTree's genuine index->value dereference.\n\nCo-Authored-By: Claude Opus 4.8 (1M context) <noreply@anthropic.com>\nClaude-Session: https://claude.ai/code/session_01Wg3xArL4FATQaXQMBvhXdg",
          "timestamp": "2026-06-23T22:35:56Z",
          "tree_id": "005c51f9d7f00116b4e04d1cdb2a5fd0680fc86e",
          "url": "https://github.com/jasonmcboyd/Arborist/commit/cf2c06864e8fdc1e6c24df1f1437f0dae1da26b0"
        },
        "date": 1782257437978,
        "tool": "benchmarkdotnet",
        "benches": [
          {
            "name": "Arborist.Benchmarks.EnumerableToTree.ToDegenerateTree",
            "value": 14129532.773958333,
            "unit": "ns",
            "range": "± 37812.87646709999"
          },
          {
            "name": "Arborist.Benchmarks.EnumerableToTree.ToDegenerateTreeUsingToTree",
            "value": 48798416.59285714,
            "unit": "ns",
            "range": "± 216740.04253498762"
          },
          {
            "name": "Arborist.Benchmarks.EnumerableToTree.ToTrivialForest",
            "value": 3409059.0002790177,
            "unit": "ns",
            "range": "± 1851.005956193042"
          },
          {
            "name": "Arborist.Benchmarks.EnumerableToTree.ToTrivialForestUsingToTree",
            "value": 35244455.52,
            "unit": "ns",
            "range": "± 112646.40700093821"
          }
        ]
      },
      {
        "commit": {
          "author": {
            "email": "jason.boyd.ce@gmail.com",
            "name": "Jason Boyd",
            "username": "jasonmcboyd"
          },
          "committer": {
            "email": "jason.boyd.ce@gmail.com",
            "name": "Jason Boyd",
            "username": "jasonmcboyd"
          },
          "distinct": true,
          "id": "ca567e0f439d6d1f0f5c2143add771a6c13fd41c",
          "message": "Fix BFT Where O(depth) memory regression: tail-carry skip prefix\n\nf7eae61's O(N)-time prefix carry stored _PredSkipPrefix as a List<int>\nindexed by absolute inner depth, grown to the current depth on every\nscheduled node -- so a 1M-deep degenerate Where(_=>true) chain allocated\n~8.39 MB even though nothing is predicate-skipped (every entry 0).\n\nReplace it with a tail-carry: the prefix is monotonic non-decreasing in\ndepth and constant (= total skips on the path) beyond the deepest skipped\nancestor, so store only up to the deepest skip and serve reads past the\nend from a scalar tail. New _PrefixStored + _PrefixStoredCount +\n_PrefixTail; PrefixRead/PrefixWriteScheduled (no-op when value == tail, so\nzero allocation in the accepted region); PrefixAnchor truncates the stored\ncount to frontDepth-1 on front-advance.\n\nWhereAll 8.39 MB -> ~1.9 KB (O(1) in depth); WhereNone byte-identical\n(inherent O(depth) preserved). Visit stream byte-identical: validated\nagainst Where2InProcessScan (full c..i, 891k cases) + WhereTests (218/0),\nnet48 + net8.0 clean. Add WhereBreadthFirstAllocationTests as a hard\nmemory-bound regression guard (the gh-pages benchmark only soft-alerts).\n\nCo-Authored-By: Claude Opus 4.8 (1M context) <noreply@anthropic.com>\nClaude-Session: https://claude.ai/code/session_01Wg3xArL4FATQaXQMBvhXdg",
          "timestamp": "2026-06-25T18:13:44Z",
          "tree_id": "6d5c2256d2c8574537a4127f73a8de04fe0b4011",
          "url": "https://github.com/jasonmcboyd/Arborist/commit/ca567e0f439d6d1f0f5c2143add771a6c13fd41c"
        },
        "date": 1782413420474,
        "tool": "benchmarkdotnet",
        "benches": [
          {
            "name": "Arborist.Benchmarks.EnumerableToTree.ToDegenerateTree",
            "value": 14582442.357291667,
            "unit": "ns",
            "range": "± 12913.785566132388"
          },
          {
            "name": "Arborist.Benchmarks.EnumerableToTree.ToDegenerateTreeUsingToTree",
            "value": 51128588.88666668,
            "unit": "ns",
            "range": "± 609521.1896902452"
          },
          {
            "name": "Arborist.Benchmarks.EnumerableToTree.ToTrivialForest",
            "value": 3609116.887276786,
            "unit": "ns",
            "range": "± 31532.59797173244"
          },
          {
            "name": "Arborist.Benchmarks.EnumerableToTree.ToTrivialForestUsingToTree",
            "value": 38229985.19047619,
            "unit": "ns",
            "range": "± 35931.533575778725"
          }
        ]
      },
      {
        "commit": {
          "author": {
            "email": "jason.boyd.ce@gmail.com",
            "name": "Jason Boyd",
            "username": "jasonmcboyd"
          },
          "committer": {
            "email": "jason.boyd.ce@gmail.com",
            "name": "Jason Boyd",
            "username": "jasonmcboyd"
          },
          "distinct": true,
          "id": "6a5d01f5e6181c1c89a0993a6360bab3a4b0bac7",
          "message": "Rewrite BreadthFirstTreenumerator with a structural visit cadence\n\nThe base BFT engine emitted parent visits reactively (when a child was\naccepted), which broke whenever a child was filtered and forced a tangle\nof deferred parent-visit bookkeeping to recover the swallowed visits.\n\nReplace it with a structural cadence: a single FIFO _VisitQueue whose\nfront is the active parent, plus a LIFO _ScheduleStack for the SkipNode\npromotion descent. A parent is visited once when it reaches the front,\nthen once after every child slot that enqueues at least one accepted\nnode -- a single bool, _CurrentSlotEnqueuedNode. Roots are scheduled\nfirst as the children of an implicit no-visit forest sentinel.\n\nThis deletes _OwesPromotedParentVisit, _HasDeferredScheduledChild,\n_DepthOfLastActedOnNode, PayOwedParentVisitAndDeferChild, and the\nOnScheduling/OnVisiting/PromoteChildren/SkipSubtree/Backtrack web in\nfavor of Advance/ApplyStrategy/SkipRemainingSiblings. The now-unused\nOwesPromotedParentVisit field comes off the shared InternalNodeVisitState\nstruct, shrinking every deque slot.\n\nNo behavior change: same (Mode, Node, VisitCount, Position) visit stream.\n\nValidation:\n- Exhaustive BFT-vs-DFT oracle, up to 6 concurrent skips x 27 trees\n- Where2InProcessScan: 891,056 Where-wrapper-vs-oracle cases (groups c..i)\n- Curated exact-order traversal + 14,759 Where/allocation tests\n- Benchmarks: allocations -12% to -14%, time within ShortRun noise\n\nCo-Authored-By: Claude Opus 4.8 (1M context) <noreply@anthropic.com>\nClaude-Session: https://claude.ai/code/session_01Wg3xArL4FATQaXQMBvhXdg",
          "timestamp": "2026-06-26T00:47:42Z",
          "tree_id": "39f01ca28e49bf3bbc704caca59e8d4bb815b334",
          "url": "https://github.com/jasonmcboyd/Arborist/commit/6a5d01f5e6181c1c89a0993a6360bab3a4b0bac7"
        },
        "date": 1782436871590,
        "tool": "benchmarkdotnet",
        "benches": [
          {
            "name": "Arborist.Benchmarks.EnumerableToTree.ToDegenerateTree",
            "value": 14605641.84735577,
            "unit": "ns",
            "range": "± 43673.787991473975"
          },
          {
            "name": "Arborist.Benchmarks.EnumerableToTree.ToDegenerateTreeUsingToTree",
            "value": 47780364.93006992,
            "unit": "ns",
            "range": "± 205681.17568262405"
          },
          {
            "name": "Arborist.Benchmarks.EnumerableToTree.ToTrivialForest",
            "value": 3494188.254464286,
            "unit": "ns",
            "range": "± 25279.643657002096"
          },
          {
            "name": "Arborist.Benchmarks.EnumerableToTree.ToTrivialForestUsingToTree",
            "value": 35940405.84888888,
            "unit": "ns",
            "range": "± 229171.15375763996"
          }
        ]
      },
      {
        "commit": {
          "author": {
            "email": "jason.boyd.ce@gmail.com",
            "name": "Jason Boyd",
            "username": "jasonmcboyd"
          },
          "committer": {
            "email": "jason.boyd.ce@gmail.com",
            "name": "Jason Boyd",
            "username": "jasonmcboyd"
          },
          "distinct": true,
          "id": "ce22f8e1055cf2b7bf6486f1da78d2058d8c69aa",
          "message": "Cap RefSemiDeque partition size to bound LOH allocation and overshoot\n\nGeometric partition growth sized each new partition to the running total\nCapacity, so the largest partition reached ~half the deque's peak element\ncount -- a multi-MB Large Object Heap allocation on wide/deep trees, plus\nup to ~2x peak over-allocation at power-of-two boundaries. Cap partition\nsize at 4096 elements (Math.Min(Capacity, MaxPartitionSize)) to bound both.\n\nBFT CompleteBinaryTree_21 (peak frontier ~2^21, the worst-case boundary):\n96 MB -> 48 MB allocated per traversal, throughput unchanged.\n\nFixed element count rather than a byte budget that would force partitions\nsub-LOH: forcing a 64 B node's partitions sub-LOH measured ~40% slower with\n~7x the Gen0 collections, because large long-lived blocks belong on the LOH.\n\nAdd RefSemiDeque regression tests crossing the cap (heterogeneous-partition\nordering, out-of-order recycling, GetFromBack/RemoveLast across boundaries)\nand an Add_Block64_1M benchmark. Remove unused InitialCapacity.\n\nCo-Authored-By: Claude Opus 4.8 (1M context) <noreply@anthropic.com>",
          "timestamp": "2026-06-26T04:13:38Z",
          "tree_id": "e35c9314063b1f6eda6dfccd1d7349907af73e32",
          "url": "https://github.com/jasonmcboyd/Arborist/commit/ce22f8e1055cf2b7bf6486f1da78d2058d8c69aa"
        },
        "date": 1782449420819,
        "tool": "benchmarkdotnet",
        "benches": [
          {
            "name": "Arborist.Benchmarks.EnumerableToTree.ToDegenerateTree",
            "value": 14522433.86830357,
            "unit": "ns",
            "range": "± 26143.790994783838"
          },
          {
            "name": "Arborist.Benchmarks.EnumerableToTree.ToDegenerateTreeUsingToTree",
            "value": 75018056.67582418,
            "unit": "ns",
            "range": "± 3086866.7199294493"
          },
          {
            "name": "Arborist.Benchmarks.EnumerableToTree.ToTrivialForest",
            "value": 3596400.6182291666,
            "unit": "ns",
            "range": "± 46092.98817288811"
          },
          {
            "name": "Arborist.Benchmarks.EnumerableToTree.ToTrivialForestUsingToTree",
            "value": 36156959.62755102,
            "unit": "ns",
            "range": "± 185332.9803102229"
          }
        ]
      },
      {
        "commit": {
          "author": {
            "email": "jason.boyd.ce@gmail.com",
            "name": "Jason Boyd",
            "username": "jasonmcboyd"
          },
          "committer": {
            "email": "jason.boyd.ce@gmail.com",
            "name": "Jason Boyd",
            "username": "jasonmcboyd"
          },
          "distinct": true,
          "id": "81b46c42702e71ad313d08c636eb3e3c3c35b140",
          "message": "Cohere BreadthFirstTreenumerator's four deques into one frame stack\n\nThe BFT kept each node's visit state and its child enumerator in FOUR parallel\nRefSemiDeques -- a state deque and an enumerator deque for each of the visit\nqueue and the schedule stack -- relying on keeping each pair in lockstep.\n\nFold each pair into one RefSemiDeque<Frame>, where Frame bundles\n{Node, Position, VisitCount, ChildEnumerator}, driven only by ref so the\nenumerator is never copied. Accepting a node becomes a single whole-frame move\nfrom the schedule stack to the visit queue, which structurally prevents a node\nand its enumerator from desynchronizing. The algorithm and visit cadence are\nunchanged. Mirrors the depth-first engine's frame stack, and removes the\nnow-unreferenced shared InternalNodeVisitState struct.\n\nNo behavior change: same (Mode, Node, VisitCount, Position) visit stream;\ndisposal (including the deliberate idempotent double-dispose on\nSkipDescendants/SkipSiblings) is at exact parity with the original.\n\nValidation:\n- Exact-order traversal + exhaustive DFT-vs-BFT multiset scan (438/0)\n- Full Arborist.Linq suite incl. Where (14,759/0)\n- Benchmarks (Release/Job.Default, clean tree): TriangleTree 289->255ms (-12%),\n  CompleteBinaryTree 337->281ms (-16%), TrivialForest/DegenerateTree 4M within\n  noise; allocation neutral, still zero per-node heap allocation. Timing\n  variance also dropped (StdDev roughly halved on the dense trees).\n\nCo-Authored-By: Claude Opus 4.8 (1M context) <noreply@anthropic.com>\nClaude-Session: https://claude.ai/code/session_01Wg3xArL4FATQaXQMBvhXdg",
          "timestamp": "2026-06-26T21:37:21Z",
          "tree_id": "05aa22f3461078c05e96518d52da4d56655c250f",
          "url": "https://github.com/jasonmcboyd/Arborist/commit/81b46c42702e71ad313d08c636eb3e3c3c35b140"
        },
        "date": 1782511426908,
        "tool": "benchmarkdotnet",
        "benches": [
          {
            "name": "Arborist.Benchmarks.EnumerableToTree.ToDegenerateTree",
            "value": 14590093.239955356,
            "unit": "ns",
            "range": "± 20914.85962135935"
          },
          {
            "name": "Arborist.Benchmarks.EnumerableToTree.ToDegenerateTreeUsingToTree",
            "value": 49917569.56956522,
            "unit": "ns",
            "range": "± 1210658.2482763652"
          },
          {
            "name": "Arborist.Benchmarks.EnumerableToTree.ToTrivialForest",
            "value": 3655628.5393415177,
            "unit": "ns",
            "range": "± 13574.82605531141"
          },
          {
            "name": "Arborist.Benchmarks.EnumerableToTree.ToTrivialForestUsingToTree",
            "value": 27989714.16964286,
            "unit": "ns",
            "range": "± 22769.806348408798"
          }
        ]
      },
      {
        "commit": {
          "author": {
            "email": "jason.boyd.ce@gmail.com",
            "name": "Jason Boyd",
            "username": "jasonmcboyd"
          },
          "committer": {
            "email": "jason.boyd.ce@gmail.com",
            "name": "Jason Boyd",
            "username": "jasonmcboyd"
          },
          "distinct": true,
          "id": "ab94b3983058299be15f1b4d38b05b21509cb8ad",
          "message": "Encapsulate BFT state in BreadthFirstPath; mirror the DFT driver/state split\n\nMove the breadth-first engine's state -- the visit queue, the schedule stack,\nthe owed-return-visit carry, and the root sibling counter -- into a new\nBreadthFirstPath, leaving BreadthFirstTreenumerator a thin driver, mirroring the\ndepth-first DepthFirstPath split. The cohesive Frame (visit state + child\nenumerator) is kept -- the BFT keeps full state for every resident node anyway,\nso it costs no memory -- so allocation is unchanged from the cohesion engine.\n\nLike DepthFirstPath, BreadthFirstPath is \"sans-I/O\": it never pulls a child; it\nexposes the two active enumerators (the schedule-stack top and the visit-queue\nfront) by ref for the driver to advance. That isolates the engine's asynchronous\noperations to those seams, so a future async BFT can share this class and differ\nonly there. It is a mutable struct embedded as the driver's single _Path field\n(never copied; refs it returns point into the heap deques), keeping dense\ntraversal at the cohesion engine's speed. The two child-pull sites collapse into\none TryScheduleNextChildOf(ref parent).\n\nNo behavior change: same (Mode, Node, VisitCount, Position) visit stream.\n\nValidation:\n- Exact-order traversal + exhaustive DFT-vs-BFT multiset scan (438/0)\n- Full Arborist.Linq suite incl. Where (14,759/0)\n- Benchmarks (Release/Job.Default, same session): TriangleTree 194.5 vs cohesion\n  193.9 ms; CompleteBinaryTree 208.1 vs 213.0 ms (parity). Allocation identical\n  to the cohesion engine (same Frame).\n\nCo-Authored-By: Claude Opus 4.8 (1M context) <noreply@anthropic.com>\nClaude-Session: https://claude.ai/code/session_01Wg3xArL4FATQaXQMBvhXdg",
          "timestamp": "2026-06-27T00:09:54Z",
          "tree_id": "8703f7198030df75b83a7026b75dc74f78b17190",
          "url": "https://github.com/jasonmcboyd/Arborist/commit/ab94b3983058299be15f1b4d38b05b21509cb8ad"
        },
        "date": 1782521635555,
        "tool": "benchmarkdotnet",
        "benches": [
          {
            "name": "Arborist.Benchmarks.EnumerableToTree.ToDegenerateTree",
            "value": 14587308.270432692,
            "unit": "ns",
            "range": "± 19462.713814500847"
          },
          {
            "name": "Arborist.Benchmarks.EnumerableToTree.ToDegenerateTreeUsingToTree",
            "value": 50536200.59090909,
            "unit": "ns",
            "range": "± 1190553.9306338022"
          },
          {
            "name": "Arborist.Benchmarks.EnumerableToTree.ToTrivialForest",
            "value": 3486341.3915264425,
            "unit": "ns",
            "range": "± 5135.592356386498"
          },
          {
            "name": "Arborist.Benchmarks.EnumerableToTree.ToTrivialForestUsingToTree",
            "value": 30268300.510416668,
            "unit": "ns",
            "range": "± 81948.83489725483"
          }
        ]
      },
      {
        "commit": {
          "author": {
            "email": "jason.boyd.ce@gmail.com",
            "name": "Jason Boyd",
            "username": "jasonmcboyd"
          },
          "committer": {
            "email": "jason.boyd.ce@gmail.com",
            "name": "Jason Boyd",
            "username": "jasonmcboyd"
          },
          "distinct": true,
          "id": "6ebd5d0e1d60592672f966eaa4ab81a302c56999",
          "message": "Fix DFT skip-heavy regression: keep TryPushNextChild out-of-line\n\nThe encapsulated DepthFirstPath DFT (14f8393) ran ~1.7-1.9x slower than the\noriginal two-stack on promotion-heavy skip traversal (SkipAllNodes / Preorder /\nPostorder on wide trees), despite identical allocation. JIT disassembly\n(DOTNET_JitDisasm) pinned the cause: [AggressiveInlining] on TryPushNextChild\ninlined the entire promote body (pull + push) into OnMoveNext/OnScheduling,\ninflating their frames to 6 callee-saved registers + sub rsp,72 + vzeroupper and\ndowngrading OnMoveNext's branch dispatch from a tail-jmp to a call+teardown paid\non EVERY node. The original two-stack stayed fast precisely because its promote\nwas a separate, out-of-line method.\n\nMark TryPushNextChild [NoInlining] so the drivers stay thin tail-dispatchers,\nwhile keeping the push chain (PushChild/PushLevel) force-inlined INTO it so the\npush itself is still call-free. Also fold Backtrack's pop + three predicate\nchecks into one DepthFirstPath.PopFinishedLevelAndClassify call: the original\ninline-predicate form is O(1) per level but its repeated struct round-trips cost\n~2x on the deep-unwind path (GetLeaves.DeepTree); folding restores it.\n\nNet (Release/Job.Default, local, vs original two-stack): SkipAllNodes.Dft\n41 -> 22.6 ms, Preorder 42.8 -> 25.7, Postorder 47 -> 32.9 -- all back at the\noriginal; dense traversal and GetLeaves.DeepTree within noise; allocation\nunchanged. The sans-I/O encapsulation (path never pulls; one ref seam) is intact.\n\nValidation: 438/0 oracle (exact-order + exhaustive DFT-vs-BFT scan), 14,759/0\nfull Linq suite.\n\nCo-Authored-By: Claude Opus 4.8 (1M context) <noreply@anthropic.com>\nClaude-Session: https://claude.ai/code/session_01Wg3xArL4FATQaXQMBvhXdg",
          "timestamp": "2026-06-27T02:26:54Z",
          "tree_id": "5301780378953d2f586361d71d9e7e7b8dc1e6d3",
          "url": "https://github.com/jasonmcboyd/Arborist/commit/6ebd5d0e1d60592672f966eaa4ab81a302c56999"
        },
        "date": 1782529879935,
        "tool": "benchmarkdotnet",
        "benches": [
          {
            "name": "Arborist.Benchmarks.EnumerableToTree.ToDegenerateTree",
            "value": 14547533.260044644,
            "unit": "ns",
            "range": "± 33458.85458036689"
          },
          {
            "name": "Arborist.Benchmarks.EnumerableToTree.ToDegenerateTreeUsingToTree",
            "value": 50529453.60882353,
            "unit": "ns",
            "range": "± 1600387.5889517919"
          },
          {
            "name": "Arborist.Benchmarks.EnumerableToTree.ToTrivialForest",
            "value": 3476125.2044270835,
            "unit": "ns",
            "range": "± 2229.9665038694957"
          },
          {
            "name": "Arborist.Benchmarks.EnumerableToTree.ToTrivialForestUsingToTree",
            "value": 30670457.375,
            "unit": "ns",
            "range": "± 41103.89136709783"
          }
        ]
      },
      {
        "commit": {
          "author": {
            "email": "jason.boyd.ce@gmail.com",
            "name": "Jason Boyd",
            "username": "jasonmcboyd"
          },
          "committer": {
            "email": "jason.boyd.ce@gmail.com",
            "name": "Jason Boyd",
            "username": "jasonmcboyd"
          },
          "distinct": true,
          "id": "e8bbc30ca40332e6f77c535d2a6e5edf419feb67",
          "message": "Revert DFT Backtrack consolidation (GetLeaves.DeepTree deep-unwind cost)\n\n6ebd5d0 fixed the wide-skip-traversal regression by making TryPushNextChild\nout-of-line, but it also folded Backtrack's pop + three predicate checks into one\nDepthFirstPath.PopFinishedLevelAndClassify call. That consolidation, fine on wide\ntrees, added one call per unwound level -- ~262K calls on the deep-unwind path --\nand regressed GetLeaves.DeepTree from ~10ms to ~24ms on the CI runner (a cost\nlocal benchmarks under-reported, due to cache differences).\n\nRevert the consolidation back to the original two-stack's inline-predicate\nBacktrack, keeping the out-of-line TryPushNextChild that fixed wide skip. The DFT\nis now structurally identical to the original two-stack (inline Backtrack +\nseparate promote method), just encapsulated in DepthFirstPath -- the form the CI\nshows is fast on every tree shape.\n\nNet (vs original two-stack, local same-session): SkipAllNodes.Dft 22.7 vs 22.0;\nGetLeaves.DeepTree 10.7 vs 8.8 (the residual +22% is the out-of-line promote's\nper-node call -- the irreducible cost of keeping wide skip fast; CI will confirm\nGetLeaves is well below 6ebd5d0's 24ms). Allocation unchanged.\n\nValidation: 438/0 oracle (exact-order + exhaustive DFT-vs-BFT scan), 14,759/0 Linq.\n\nCo-Authored-By: Claude Opus 4.8 (1M context) <noreply@anthropic.com>\nClaude-Session: https://claude.ai/code/session_01Wg3xArL4FATQaXQMBvhXdg",
          "timestamp": "2026-06-27T03:58:38Z",
          "tree_id": "a3f9dd64f7a624f48c68d521c40c1a096d4fa152",
          "url": "https://github.com/jasonmcboyd/Arborist/commit/e8bbc30ca40332e6f77c535d2a6e5edf419feb67"
        },
        "date": 1782534425795,
        "tool": "benchmarkdotnet",
        "benches": [
          {
            "name": "Arborist.Benchmarks.EnumerableToTree.ToDegenerateTree",
            "value": 14530129.0078125,
            "unit": "ns",
            "range": "± 29615.799773007184"
          },
          {
            "name": "Arborist.Benchmarks.EnumerableToTree.ToDegenerateTreeUsingToTree",
            "value": 50369611.559999995,
            "unit": "ns",
            "range": "± 905743.5679524665"
          },
          {
            "name": "Arborist.Benchmarks.EnumerableToTree.ToTrivialForest",
            "value": 3602378.454520089,
            "unit": "ns",
            "range": "± 24100.06365928412"
          },
          {
            "name": "Arborist.Benchmarks.EnumerableToTree.ToTrivialForestUsingToTree",
            "value": 30326025.3984375,
            "unit": "ns",
            "range": "± 52602.82104441895"
          }
        ]
      }
    ],
    "DataStructures Benchmarks": [
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
        "date": 1769975445893,
        "tool": "benchmarkdotnet",
        "benches": [
          {
            "name": "Arborist.Benchmarks.RefSemiDeque.Add_8M",
            "value": 19767683.225,
            "unit": "ns",
            "range": "± 114998.70681724027"
          },
          {
            "name": "Arborist.Benchmarks.RefSemiDeque.RemoveFirst_8M",
            "value": 32311528.229166668,
            "unit": "ns",
            "range": "± 408281.6234405529"
          },
          {
            "name": "Arborist.Benchmarks.RefSemiDeque.RemoveLast_8M",
            "value": 27217033.014423076,
            "unit": "ns",
            "range": "± 192510.91620227537"
          }
        ]
      },
      {
        "commit": {
          "author": {
            "email": "jason.boyd.ce@gmail.com",
            "name": "Jason Boyd",
            "username": "jasonmcboyd"
          },
          "committer": {
            "email": "jason.boyd.ce@gmail.com",
            "name": "Jason Boyd",
            "username": "jasonmcboyd"
          },
          "distinct": true,
          "id": "5993e5762d3c0ffa1bbacbb40b35307cff9a3f87",
          "message": "Docs: document BFT Where O(N) prefix-carry design\n\nRewrite the WhereBreadthFirstTreenumerator section to match the implementation (queue + incremental _PredSkipPrefix, predicate-skip vs consumer-SkipNode, the real parent-visit fields, front-anchored sibling index). Update the DFT-vs-BFT table (BFT now O(N)) and fix stale _ExtraParentVisitsEmitted/_LastRemovedSkippedNodePosition/_SkippedStack references.\n\nCo-Authored-By: Claude Opus 4.8 (1M context) <noreply@anthropic.com>\nClaude-Session: https://claude.ai/code/session_01Wg3xArL4FATQaXQMBvhXdg",
          "timestamp": "2026-06-23T04:33:54Z",
          "tree_id": "a2615b3ce8e91df5baffd5f8f08e3c6ac9df2e3b",
          "url": "https://github.com/jasonmcboyd/Arborist/commit/5993e5762d3c0ffa1bbacbb40b35307cff9a3f87"
        },
        "date": 1782190984611,
        "tool": "benchmarkdotnet",
        "benches": [
          {
            "name": "Arborist.Benchmarks.RefSemiDeque.Add_8M",
            "value": 18436532.73214286,
            "unit": "ns",
            "range": "± 63773.45216853519"
          },
          {
            "name": "Arborist.Benchmarks.RefSemiDeque.RemoveFirst_8M",
            "value": 30325891.160416666,
            "unit": "ns",
            "range": "± 118109.97434497491"
          },
          {
            "name": "Arborist.Benchmarks.RefSemiDeque.RemoveLast_8M",
            "value": 26672013.110416666,
            "unit": "ns",
            "range": "± 52410.8486413432"
          }
        ]
      },
      {
        "commit": {
          "author": {
            "email": "jason.boyd.ce@gmail.com",
            "name": "Jason Boyd",
            "username": "jasonmcboyd"
          },
          "committer": {
            "email": "jason.boyd.ce@gmail.com",
            "name": "Jason Boyd",
            "username": "jasonmcboyd"
          },
          "distinct": true,
          "id": "c3ab5ef05fc7c7304bb98cd14e97bed3a8f91e62",
          "message": "Align LeaffixAggregate with LeaffixScan (flat, lazy, zero per-node alloc)\n\nLeaffixAggregate now uses the same flat forward DFS + no-copy ChildAccumulations view as LeaffixScan, yielding each root's accumulated value. Accumulator changes TAccumulate[] -> ChildAccumulations<TAccumulate>. Per-root lazy: a root is emitted the moment its subtree completes and the buffers are reused for the next root, so peak memory is the largest root subtree (not the whole forest) and early-terminating consumers traverse fewer roots -- matching the previous LeaffixAggregator behavior. Zero per-node heap allocation. Adds LeaffixAggregateTests (it previously had none and no callers).\n\nLeaffixAggregator is retained -- still used by Invert (TreeInverter); it'll be removed when Invert is redesigned onto a flat structure.\n\nCo-Authored-By: Claude Opus 4.8 (1M context) <noreply@anthropic.com>\nClaude-Session: https://claude.ai/code/session_01Wg3xArL4FATQaXQMBvhXdg",
          "timestamp": "2026-06-23T21:57:19Z",
          "tree_id": "7dacc032f0faefb81f1e004514e835a60b5bf81a",
          "url": "https://github.com/jasonmcboyd/Arborist/commit/c3ab5ef05fc7c7304bb98cd14e97bed3a8f91e62"
        },
        "date": 1782253174097,
        "tool": "benchmarkdotnet",
        "benches": [
          {
            "name": "Arborist.Benchmarks.RefSemiDeque.Add_8M",
            "value": 18152696.02455357,
            "unit": "ns",
            "range": "± 170418.15294661687"
          },
          {
            "name": "Arborist.Benchmarks.RefSemiDeque.RemoveFirst_8M",
            "value": 30010359.635416668,
            "unit": "ns",
            "range": "± 155588.2208472988"
          },
          {
            "name": "Arborist.Benchmarks.RefSemiDeque.RemoveLast_8M",
            "value": 27406141.645833332,
            "unit": "ns",
            "range": "± 273735.3298925518"
          }
        ]
      },
      {
        "commit": {
          "author": {
            "email": "jason.boyd.ce@gmail.com",
            "name": "Jason Boyd",
            "username": "jasonmcboyd"
          },
          "committer": {
            "email": "jason.boyd.ce@gmail.com",
            "name": "Jason Boyd",
            "username": "jasonmcboyd"
          },
          "distinct": true,
          "id": "96f3cc44e2208f47258c2887c2a011530d708864",
          "message": "Rewrite Invert flat; retire SimpleNode and LeaffixAggregator\n\nInvert now materializes the source into flat pre-order arrays and emits the mirror with a stack -- pushing roots/children in forward order so they pop in reverse (the mirror's pre-order). Subtree sizes are invariant under mirroring; result is a PreorderTree. O(N), zero per-node allocation (was a SimpleNode object per node via LeaffixAggregator).\n\nInvert was the last user of both SimpleNode and LeaffixAggregator, so this deletes five files: TreeInverter, LeaffixAggregator (+NodeContextAndResult), SimpleNode, SimpleNodeChildEnumerator, SimpleNodeTreenumerable. The SimpleNode retirement is complete, clearing the runway for the nodeToValueMap elimination. (A level-order/LOUDS mirror -- reverse each child-run in place -- remains a future refinement if a LevelOrderTree is ever built.)\n\nCo-Authored-By: Claude Opus 4.8 (1M context) <noreply@anthropic.com>\nClaude-Session: https://claude.ai/code/session_01Wg3xArL4FATQaXQMBvhXdg",
          "timestamp": "2026-06-23T22:21:16Z",
          "tree_id": "aafd024b9cc4740d5e6c4fe63bc7cf95158cbb5e",
          "url": "https://github.com/jasonmcboyd/Arborist/commit/96f3cc44e2208f47258c2887c2a011530d708864"
        },
        "date": 1782254668481,
        "tool": "benchmarkdotnet",
        "benches": [
          {
            "name": "Arborist.Benchmarks.RefSemiDeque.Add_8M",
            "value": 22734292.951041665,
            "unit": "ns",
            "range": "± 222450.76413248904"
          },
          {
            "name": "Arborist.Benchmarks.RefSemiDeque.RemoveFirst_8M",
            "value": 41055264.73846155,
            "unit": "ns",
            "range": "± 166538.6881930848"
          },
          {
            "name": "Arborist.Benchmarks.RefSemiDeque.RemoveLast_8M",
            "value": 34970045.222222224,
            "unit": "ns",
            "range": "± 201073.89252819953"
          }
        ]
      },
      {
        "commit": {
          "author": {
            "email": "jason.boyd.ce@gmail.com",
            "name": "Jason Boyd",
            "username": "jasonmcboyd"
          },
          "committer": {
            "email": "jason.boyd.ce@gmail.com",
            "name": "Jason Boyd",
            "username": "jasonmcboyd"
          },
          "distinct": true,
          "id": "7c795cd25f5b93fbac1993cf6a71485ef8c2de74",
          "message": "Add MemoryDiagnoser benchmark for Invert\n\nCovers TriangleTree depth-1448 (~1M) and a 1M-deep DegenerateTree, tagged LINQ for the dashboard. Completes benchmark coverage of the now-flat transform/aggregation ops (Materialize, LeaffixScan, LeaffixAggregate, Invert).\n\nCo-Authored-By: Claude Opus 4.8 (1M context) <noreply@anthropic.com>\nClaude-Session: https://claude.ai/code/session_01Wg3xArL4FATQaXQMBvhXdg",
          "timestamp": "2026-06-23T22:22:47Z",
          "tree_id": "547984a2584ebd974bf3f4a1790b68fc9b328cfc",
          "url": "https://github.com/jasonmcboyd/Arborist/commit/7c795cd25f5b93fbac1993cf6a71485ef8c2de74"
        },
        "date": 1782256078939,
        "tool": "benchmarkdotnet",
        "benches": [
          {
            "name": "Arborist.Benchmarks.RefSemiDeque.Add_8M",
            "value": 18546450.983333334,
            "unit": "ns",
            "range": "± 110651.8405561774"
          },
          {
            "name": "Arborist.Benchmarks.RefSemiDeque.RemoveFirst_8M",
            "value": 31240093.24330357,
            "unit": "ns",
            "range": "± 105627.66373495368"
          },
          {
            "name": "Arborist.Benchmarks.RefSemiDeque.RemoveLast_8M",
            "value": 28415122.511160713,
            "unit": "ns",
            "range": "± 166757.15503084735"
          }
        ]
      },
      {
        "commit": {
          "author": {
            "email": "jason.boyd.ce@gmail.com",
            "name": "Jason Boyd",
            "username": "jasonmcboyd"
          },
          "committer": {
            "email": "jason.boyd.ce@gmail.com",
            "name": "Jason Boyd",
            "username": "jasonmcboyd"
          },
          "distinct": true,
          "id": "cf2c06864e8fdc1e6c24df1f1437f0dae1da26b0",
          "message": "Add 2-param Treenumerable base; drop redundant identity maps (map cleanup stage 1)\n\nThe sample trees whose node IS their surfaced value (TriangleTree, CollatzTree, CompleteBinaryTree, NDecrementTree, DeepTree) were each passing a redundant 'node => node' nodeToValueMap and carrying a duplicate type parameter. Add a 2-param Treenumerable<TNode, TChildEnumerator> convenience base (identity map) and migrate them to it. Public surfaces unchanged. The 3-param base + map remain for PreorderTree's genuine index->value dereference.\n\nCo-Authored-By: Claude Opus 4.8 (1M context) <noreply@anthropic.com>\nClaude-Session: https://claude.ai/code/session_01Wg3xArL4FATQaXQMBvhXdg",
          "timestamp": "2026-06-23T22:35:56Z",
          "tree_id": "005c51f9d7f00116b4e04d1cdb2a5fd0680fc86e",
          "url": "https://github.com/jasonmcboyd/Arborist/commit/cf2c06864e8fdc1e6c24df1f1437f0dae1da26b0"
        },
        "date": 1782257438139,
        "tool": "benchmarkdotnet",
        "benches": [
          {
            "name": "Arborist.Benchmarks.RefSemiDeque.Add_8M",
            "value": 22659256.303125,
            "unit": "ns",
            "range": "± 140816.21525436544"
          },
          {
            "name": "Arborist.Benchmarks.RefSemiDeque.RemoveFirst_8M",
            "value": 39924305.67692308,
            "unit": "ns",
            "range": "± 114160.92137464473"
          },
          {
            "name": "Arborist.Benchmarks.RefSemiDeque.RemoveLast_8M",
            "value": 34908193.9897436,
            "unit": "ns",
            "range": "± 214617.83376782524"
          }
        ]
      },
      {
        "commit": {
          "author": {
            "email": "jason.boyd.ce@gmail.com",
            "name": "Jason Boyd",
            "username": "jasonmcboyd"
          },
          "committer": {
            "email": "jason.boyd.ce@gmail.com",
            "name": "Jason Boyd",
            "username": "jasonmcboyd"
          },
          "distinct": true,
          "id": "ca567e0f439d6d1f0f5c2143add771a6c13fd41c",
          "message": "Fix BFT Where O(depth) memory regression: tail-carry skip prefix\n\nf7eae61's O(N)-time prefix carry stored _PredSkipPrefix as a List<int>\nindexed by absolute inner depth, grown to the current depth on every\nscheduled node -- so a 1M-deep degenerate Where(_=>true) chain allocated\n~8.39 MB even though nothing is predicate-skipped (every entry 0).\n\nReplace it with a tail-carry: the prefix is monotonic non-decreasing in\ndepth and constant (= total skips on the path) beyond the deepest skipped\nancestor, so store only up to the deepest skip and serve reads past the\nend from a scalar tail. New _PrefixStored + _PrefixStoredCount +\n_PrefixTail; PrefixRead/PrefixWriteScheduled (no-op when value == tail, so\nzero allocation in the accepted region); PrefixAnchor truncates the stored\ncount to frontDepth-1 on front-advance.\n\nWhereAll 8.39 MB -> ~1.9 KB (O(1) in depth); WhereNone byte-identical\n(inherent O(depth) preserved). Visit stream byte-identical: validated\nagainst Where2InProcessScan (full c..i, 891k cases) + WhereTests (218/0),\nnet48 + net8.0 clean. Add WhereBreadthFirstAllocationTests as a hard\nmemory-bound regression guard (the gh-pages benchmark only soft-alerts).\n\nCo-Authored-By: Claude Opus 4.8 (1M context) <noreply@anthropic.com>\nClaude-Session: https://claude.ai/code/session_01Wg3xArL4FATQaXQMBvhXdg",
          "timestamp": "2026-06-25T18:13:44Z",
          "tree_id": "6d5c2256d2c8574537a4127f73a8de04fe0b4011",
          "url": "https://github.com/jasonmcboyd/Arborist/commit/ca567e0f439d6d1f0f5c2143add771a6c13fd41c"
        },
        "date": 1782413420682,
        "tool": "benchmarkdotnet",
        "benches": [
          {
            "name": "Arborist.Benchmarks.RefSemiDeque.Add_8M",
            "value": 18210100.36830357,
            "unit": "ns",
            "range": "± 75336.1436998006"
          },
          {
            "name": "Arborist.Benchmarks.RefSemiDeque.RemoveFirst_8M",
            "value": 30664329.210416667,
            "unit": "ns",
            "range": "± 513106.3483833327"
          },
          {
            "name": "Arborist.Benchmarks.RefSemiDeque.RemoveLast_8M",
            "value": 26796130.099609375,
            "unit": "ns",
            "range": "± 499704.3542393409"
          }
        ]
      },
      {
        "commit": {
          "author": {
            "email": "jason.boyd.ce@gmail.com",
            "name": "Jason Boyd",
            "username": "jasonmcboyd"
          },
          "committer": {
            "email": "jason.boyd.ce@gmail.com",
            "name": "Jason Boyd",
            "username": "jasonmcboyd"
          },
          "distinct": true,
          "id": "6a5d01f5e6181c1c89a0993a6360bab3a4b0bac7",
          "message": "Rewrite BreadthFirstTreenumerator with a structural visit cadence\n\nThe base BFT engine emitted parent visits reactively (when a child was\naccepted), which broke whenever a child was filtered and forced a tangle\nof deferred parent-visit bookkeeping to recover the swallowed visits.\n\nReplace it with a structural cadence: a single FIFO _VisitQueue whose\nfront is the active parent, plus a LIFO _ScheduleStack for the SkipNode\npromotion descent. A parent is visited once when it reaches the front,\nthen once after every child slot that enqueues at least one accepted\nnode -- a single bool, _CurrentSlotEnqueuedNode. Roots are scheduled\nfirst as the children of an implicit no-visit forest sentinel.\n\nThis deletes _OwesPromotedParentVisit, _HasDeferredScheduledChild,\n_DepthOfLastActedOnNode, PayOwedParentVisitAndDeferChild, and the\nOnScheduling/OnVisiting/PromoteChildren/SkipSubtree/Backtrack web in\nfavor of Advance/ApplyStrategy/SkipRemainingSiblings. The now-unused\nOwesPromotedParentVisit field comes off the shared InternalNodeVisitState\nstruct, shrinking every deque slot.\n\nNo behavior change: same (Mode, Node, VisitCount, Position) visit stream.\n\nValidation:\n- Exhaustive BFT-vs-DFT oracle, up to 6 concurrent skips x 27 trees\n- Where2InProcessScan: 891,056 Where-wrapper-vs-oracle cases (groups c..i)\n- Curated exact-order traversal + 14,759 Where/allocation tests\n- Benchmarks: allocations -12% to -14%, time within ShortRun noise\n\nCo-Authored-By: Claude Opus 4.8 (1M context) <noreply@anthropic.com>\nClaude-Session: https://claude.ai/code/session_01Wg3xArL4FATQaXQMBvhXdg",
          "timestamp": "2026-06-26T00:47:42Z",
          "tree_id": "39f01ca28e49bf3bbc704caca59e8d4bb815b334",
          "url": "https://github.com/jasonmcboyd/Arborist/commit/6a5d01f5e6181c1c89a0993a6360bab3a4b0bac7"
        },
        "date": 1782436871786,
        "tool": "benchmarkdotnet",
        "benches": [
          {
            "name": "Arborist.Benchmarks.RefSemiDeque.Add_8M",
            "value": 18698973.685416665,
            "unit": "ns",
            "range": "± 192671.75991577975"
          },
          {
            "name": "Arborist.Benchmarks.RefSemiDeque.RemoveFirst_8M",
            "value": 30394146.495833334,
            "unit": "ns",
            "range": "± 120822.58632490085"
          },
          {
            "name": "Arborist.Benchmarks.RefSemiDeque.RemoveLast_8M",
            "value": 26885584.147916667,
            "unit": "ns",
            "range": "± 216193.08825157626"
          }
        ]
      },
      {
        "commit": {
          "author": {
            "email": "jason.boyd.ce@gmail.com",
            "name": "Jason Boyd",
            "username": "jasonmcboyd"
          },
          "committer": {
            "email": "jason.boyd.ce@gmail.com",
            "name": "Jason Boyd",
            "username": "jasonmcboyd"
          },
          "distinct": true,
          "id": "ce22f8e1055cf2b7bf6486f1da78d2058d8c69aa",
          "message": "Cap RefSemiDeque partition size to bound LOH allocation and overshoot\n\nGeometric partition growth sized each new partition to the running total\nCapacity, so the largest partition reached ~half the deque's peak element\ncount -- a multi-MB Large Object Heap allocation on wide/deep trees, plus\nup to ~2x peak over-allocation at power-of-two boundaries. Cap partition\nsize at 4096 elements (Math.Min(Capacity, MaxPartitionSize)) to bound both.\n\nBFT CompleteBinaryTree_21 (peak frontier ~2^21, the worst-case boundary):\n96 MB -> 48 MB allocated per traversal, throughput unchanged.\n\nFixed element count rather than a byte budget that would force partitions\nsub-LOH: forcing a 64 B node's partitions sub-LOH measured ~40% slower with\n~7x the Gen0 collections, because large long-lived blocks belong on the LOH.\n\nAdd RefSemiDeque regression tests crossing the cap (heterogeneous-partition\nordering, out-of-order recycling, GetFromBack/RemoveLast across boundaries)\nand an Add_Block64_1M benchmark. Remove unused InitialCapacity.\n\nCo-Authored-By: Claude Opus 4.8 (1M context) <noreply@anthropic.com>",
          "timestamp": "2026-06-26T04:13:38Z",
          "tree_id": "e35c9314063b1f6eda6dfccd1d7349907af73e32",
          "url": "https://github.com/jasonmcboyd/Arborist/commit/ce22f8e1055cf2b7bf6486f1da78d2058d8c69aa"
        },
        "date": 1782449421546,
        "tool": "benchmarkdotnet",
        "benches": [
          {
            "name": "Arborist.Benchmarks.RefSemiDeque.Add_8M",
            "value": 17020374.077083334,
            "unit": "ns",
            "range": "± 289489.4650102861"
          },
          {
            "name": "Arborist.Benchmarks.RefSemiDeque.RemoveFirst_8M",
            "value": 29554952.960416667,
            "unit": "ns",
            "range": "± 278589.24062793807"
          },
          {
            "name": "Arborist.Benchmarks.RefSemiDeque.RemoveLast_8M",
            "value": 25665255.42857143,
            "unit": "ns",
            "range": "± 325554.57352549967"
          },
          {
            "name": "Arborist.Benchmarks.RefSemiDeque.Add_Block64_1M",
            "value": 16225628.664818548,
            "unit": "ns",
            "range": "± 732972.433468815"
          }
        ]
      },
      {
        "commit": {
          "author": {
            "email": "jason.boyd.ce@gmail.com",
            "name": "Jason Boyd",
            "username": "jasonmcboyd"
          },
          "committer": {
            "email": "jason.boyd.ce@gmail.com",
            "name": "Jason Boyd",
            "username": "jasonmcboyd"
          },
          "distinct": true,
          "id": "81b46c42702e71ad313d08c636eb3e3c3c35b140",
          "message": "Cohere BreadthFirstTreenumerator's four deques into one frame stack\n\nThe BFT kept each node's visit state and its child enumerator in FOUR parallel\nRefSemiDeques -- a state deque and an enumerator deque for each of the visit\nqueue and the schedule stack -- relying on keeping each pair in lockstep.\n\nFold each pair into one RefSemiDeque<Frame>, where Frame bundles\n{Node, Position, VisitCount, ChildEnumerator}, driven only by ref so the\nenumerator is never copied. Accepting a node becomes a single whole-frame move\nfrom the schedule stack to the visit queue, which structurally prevents a node\nand its enumerator from desynchronizing. The algorithm and visit cadence are\nunchanged. Mirrors the depth-first engine's frame stack, and removes the\nnow-unreferenced shared InternalNodeVisitState struct.\n\nNo behavior change: same (Mode, Node, VisitCount, Position) visit stream;\ndisposal (including the deliberate idempotent double-dispose on\nSkipDescendants/SkipSiblings) is at exact parity with the original.\n\nValidation:\n- Exact-order traversal + exhaustive DFT-vs-BFT multiset scan (438/0)\n- Full Arborist.Linq suite incl. Where (14,759/0)\n- Benchmarks (Release/Job.Default, clean tree): TriangleTree 289->255ms (-12%),\n  CompleteBinaryTree 337->281ms (-16%), TrivialForest/DegenerateTree 4M within\n  noise; allocation neutral, still zero per-node heap allocation. Timing\n  variance also dropped (StdDev roughly halved on the dense trees).\n\nCo-Authored-By: Claude Opus 4.8 (1M context) <noreply@anthropic.com>\nClaude-Session: https://claude.ai/code/session_01Wg3xArL4FATQaXQMBvhXdg",
          "timestamp": "2026-06-26T21:37:21Z",
          "tree_id": "05aa22f3461078c05e96518d52da4d56655c250f",
          "url": "https://github.com/jasonmcboyd/Arborist/commit/81b46c42702e71ad313d08c636eb3e3c3c35b140"
        },
        "date": 1782511427126,
        "tool": "benchmarkdotnet",
        "benches": [
          {
            "name": "Arborist.Benchmarks.RefSemiDeque.Add_8M",
            "value": 16599577.06743421,
            "unit": "ns",
            "range": "± 357393.4599081653"
          },
          {
            "name": "Arborist.Benchmarks.RefSemiDeque.RemoveFirst_8M",
            "value": 29153766.026785713,
            "unit": "ns",
            "range": "± 139811.3882081452"
          },
          {
            "name": "Arborist.Benchmarks.RefSemiDeque.RemoveLast_8M",
            "value": 25054985.519230768,
            "unit": "ns",
            "range": "± 188855.68764494572"
          },
          {
            "name": "Arborist.Benchmarks.RefSemiDeque.Add_Block64_1M",
            "value": 16080276.091372283,
            "unit": "ns",
            "range": "± 609280.7396030414"
          }
        ]
      },
      {
        "commit": {
          "author": {
            "email": "jason.boyd.ce@gmail.com",
            "name": "Jason Boyd",
            "username": "jasonmcboyd"
          },
          "committer": {
            "email": "jason.boyd.ce@gmail.com",
            "name": "Jason Boyd",
            "username": "jasonmcboyd"
          },
          "distinct": true,
          "id": "ab94b3983058299be15f1b4d38b05b21509cb8ad",
          "message": "Encapsulate BFT state in BreadthFirstPath; mirror the DFT driver/state split\n\nMove the breadth-first engine's state -- the visit queue, the schedule stack,\nthe owed-return-visit carry, and the root sibling counter -- into a new\nBreadthFirstPath, leaving BreadthFirstTreenumerator a thin driver, mirroring the\ndepth-first DepthFirstPath split. The cohesive Frame (visit state + child\nenumerator) is kept -- the BFT keeps full state for every resident node anyway,\nso it costs no memory -- so allocation is unchanged from the cohesion engine.\n\nLike DepthFirstPath, BreadthFirstPath is \"sans-I/O\": it never pulls a child; it\nexposes the two active enumerators (the schedule-stack top and the visit-queue\nfront) by ref for the driver to advance. That isolates the engine's asynchronous\noperations to those seams, so a future async BFT can share this class and differ\nonly there. It is a mutable struct embedded as the driver's single _Path field\n(never copied; refs it returns point into the heap deques), keeping dense\ntraversal at the cohesion engine's speed. The two child-pull sites collapse into\none TryScheduleNextChildOf(ref parent).\n\nNo behavior change: same (Mode, Node, VisitCount, Position) visit stream.\n\nValidation:\n- Exact-order traversal + exhaustive DFT-vs-BFT multiset scan (438/0)\n- Full Arborist.Linq suite incl. Where (14,759/0)\n- Benchmarks (Release/Job.Default, same session): TriangleTree 194.5 vs cohesion\n  193.9 ms; CompleteBinaryTree 208.1 vs 213.0 ms (parity). Allocation identical\n  to the cohesion engine (same Frame).\n\nCo-Authored-By: Claude Opus 4.8 (1M context) <noreply@anthropic.com>\nClaude-Session: https://claude.ai/code/session_01Wg3xArL4FATQaXQMBvhXdg",
          "timestamp": "2026-06-27T00:09:54Z",
          "tree_id": "8703f7198030df75b83a7026b75dc74f78b17190",
          "url": "https://github.com/jasonmcboyd/Arborist/commit/ab94b3983058299be15f1b4d38b05b21509cb8ad"
        },
        "date": 1782521635788,
        "tool": "benchmarkdotnet",
        "benches": [
          {
            "name": "Arborist.Benchmarks.RefSemiDeque.Add_8M",
            "value": 16367676.820833333,
            "unit": "ns",
            "range": "± 247510.91633211845"
          },
          {
            "name": "Arborist.Benchmarks.RefSemiDeque.RemoveFirst_8M",
            "value": 29425023.44375,
            "unit": "ns",
            "range": "± 475678.2473304623"
          },
          {
            "name": "Arborist.Benchmarks.RefSemiDeque.RemoveLast_8M",
            "value": 26929824.629166666,
            "unit": "ns",
            "range": "± 98450.3064934275"
          },
          {
            "name": "Arborist.Benchmarks.RefSemiDeque.Add_Block64_1M",
            "value": 16207204.621299341,
            "unit": "ns",
            "range": "± 816259.4389167792"
          }
        ]
      },
      {
        "commit": {
          "author": {
            "email": "jason.boyd.ce@gmail.com",
            "name": "Jason Boyd",
            "username": "jasonmcboyd"
          },
          "committer": {
            "email": "jason.boyd.ce@gmail.com",
            "name": "Jason Boyd",
            "username": "jasonmcboyd"
          },
          "distinct": true,
          "id": "6ebd5d0e1d60592672f966eaa4ab81a302c56999",
          "message": "Fix DFT skip-heavy regression: keep TryPushNextChild out-of-line\n\nThe encapsulated DepthFirstPath DFT (14f8393) ran ~1.7-1.9x slower than the\noriginal two-stack on promotion-heavy skip traversal (SkipAllNodes / Preorder /\nPostorder on wide trees), despite identical allocation. JIT disassembly\n(DOTNET_JitDisasm) pinned the cause: [AggressiveInlining] on TryPushNextChild\ninlined the entire promote body (pull + push) into OnMoveNext/OnScheduling,\ninflating their frames to 6 callee-saved registers + sub rsp,72 + vzeroupper and\ndowngrading OnMoveNext's branch dispatch from a tail-jmp to a call+teardown paid\non EVERY node. The original two-stack stayed fast precisely because its promote\nwas a separate, out-of-line method.\n\nMark TryPushNextChild [NoInlining] so the drivers stay thin tail-dispatchers,\nwhile keeping the push chain (PushChild/PushLevel) force-inlined INTO it so the\npush itself is still call-free. Also fold Backtrack's pop + three predicate\nchecks into one DepthFirstPath.PopFinishedLevelAndClassify call: the original\ninline-predicate form is O(1) per level but its repeated struct round-trips cost\n~2x on the deep-unwind path (GetLeaves.DeepTree); folding restores it.\n\nNet (Release/Job.Default, local, vs original two-stack): SkipAllNodes.Dft\n41 -> 22.6 ms, Preorder 42.8 -> 25.7, Postorder 47 -> 32.9 -- all back at the\noriginal; dense traversal and GetLeaves.DeepTree within noise; allocation\nunchanged. The sans-I/O encapsulation (path never pulls; one ref seam) is intact.\n\nValidation: 438/0 oracle (exact-order + exhaustive DFT-vs-BFT scan), 14,759/0\nfull Linq suite.\n\nCo-Authored-By: Claude Opus 4.8 (1M context) <noreply@anthropic.com>\nClaude-Session: https://claude.ai/code/session_01Wg3xArL4FATQaXQMBvhXdg",
          "timestamp": "2026-06-27T02:26:54Z",
          "tree_id": "5301780378953d2f586361d71d9e7e7b8dc1e6d3",
          "url": "https://github.com/jasonmcboyd/Arborist/commit/6ebd5d0e1d60592672f966eaa4ab81a302c56999"
        },
        "date": 1782529880135,
        "tool": "benchmarkdotnet",
        "benches": [
          {
            "name": "Arborist.Benchmarks.RefSemiDeque.Add_8M",
            "value": 16181980.341517856,
            "unit": "ns",
            "range": "± 81105.0247027625"
          },
          {
            "name": "Arborist.Benchmarks.RefSemiDeque.RemoveFirst_8M",
            "value": 29128789.05,
            "unit": "ns",
            "range": "± 125140.30207459372"
          },
          {
            "name": "Arborist.Benchmarks.RefSemiDeque.RemoveLast_8M",
            "value": 24878506.778645832,
            "unit": "ns",
            "range": "± 115040.93154614445"
          },
          {
            "name": "Arborist.Benchmarks.RefSemiDeque.Add_Block64_1M",
            "value": 15743382.448529411,
            "unit": "ns",
            "range": "± 744046.1148943498"
          }
        ]
      },
      {
        "commit": {
          "author": {
            "email": "jason.boyd.ce@gmail.com",
            "name": "Jason Boyd",
            "username": "jasonmcboyd"
          },
          "committer": {
            "email": "jason.boyd.ce@gmail.com",
            "name": "Jason Boyd",
            "username": "jasonmcboyd"
          },
          "distinct": true,
          "id": "e8bbc30ca40332e6f77c535d2a6e5edf419feb67",
          "message": "Revert DFT Backtrack consolidation (GetLeaves.DeepTree deep-unwind cost)\n\n6ebd5d0 fixed the wide-skip-traversal regression by making TryPushNextChild\nout-of-line, but it also folded Backtrack's pop + three predicate checks into one\nDepthFirstPath.PopFinishedLevelAndClassify call. That consolidation, fine on wide\ntrees, added one call per unwound level -- ~262K calls on the deep-unwind path --\nand regressed GetLeaves.DeepTree from ~10ms to ~24ms on the CI runner (a cost\nlocal benchmarks under-reported, due to cache differences).\n\nRevert the consolidation back to the original two-stack's inline-predicate\nBacktrack, keeping the out-of-line TryPushNextChild that fixed wide skip. The DFT\nis now structurally identical to the original two-stack (inline Backtrack +\nseparate promote method), just encapsulated in DepthFirstPath -- the form the CI\nshows is fast on every tree shape.\n\nNet (vs original two-stack, local same-session): SkipAllNodes.Dft 22.7 vs 22.0;\nGetLeaves.DeepTree 10.7 vs 8.8 (the residual +22% is the out-of-line promote's\nper-node call -- the irreducible cost of keeping wide skip fast; CI will confirm\nGetLeaves is well below 6ebd5d0's 24ms). Allocation unchanged.\n\nValidation: 438/0 oracle (exact-order + exhaustive DFT-vs-BFT scan), 14,759/0 Linq.\n\nCo-Authored-By: Claude Opus 4.8 (1M context) <noreply@anthropic.com>\nClaude-Session: https://claude.ai/code/session_01Wg3xArL4FATQaXQMBvhXdg",
          "timestamp": "2026-06-27T03:58:38Z",
          "tree_id": "a3f9dd64f7a624f48c68d521c40c1a096d4fa152",
          "url": "https://github.com/jasonmcboyd/Arborist/commit/e8bbc30ca40332e6f77c535d2a6e5edf419feb67"
        },
        "date": 1782534426009,
        "tool": "benchmarkdotnet",
        "benches": [
          {
            "name": "Arborist.Benchmarks.RefSemiDeque.Add_8M",
            "value": 16428562.794791667,
            "unit": "ns",
            "range": "± 249494.09540031268"
          },
          {
            "name": "Arborist.Benchmarks.RefSemiDeque.RemoveFirst_8M",
            "value": 29385803.739583332,
            "unit": "ns",
            "range": "± 247181.72094234222"
          },
          {
            "name": "Arborist.Benchmarks.RefSemiDeque.RemoveLast_8M",
            "value": 26726139.583333332,
            "unit": "ns",
            "range": "± 79722.11391369648"
          },
          {
            "name": "Arborist.Benchmarks.RefSemiDeque.Add_Block64_1M",
            "value": 16106030.457974138,
            "unit": "ns",
            "range": "± 705141.7229169845"
          }
        ]
      }
    ],
    "Traversal Memory": [
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
        "date": 1769975446071,
        "tool": "customSmallerIsBetter",
        "benches": [
          {
            "name": "Arborist.Benchmarks.BreadthFirstTreenumerator.TriangleTree_2896",
            "value": 461904,
            "unit": "bytes"
          },
          {
            "name": "Arborist.Benchmarks.BreadthFirstTreenumerator.CompleteBinaryTree_21",
            "value": 100672304,
            "unit": "bytes"
          },
          {
            "name": "Arborist.Benchmarks.BreadthFirstTreenumerator.TrivialForest_4M",
            "value": 295,
            "unit": "bytes"
          },
          {
            "name": "Arborist.Benchmarks.BreadthFirstTreenumerator.DegenerateTree_4M",
            "value": 859,
            "unit": "bytes"
          },
          {
            "name": "Arborist.Benchmarks.DepthFirstTreenumerator.TriangleTree_2896",
            "value": 116853,
            "unit": "bytes"
          },
          {
            "name": "Arborist.Benchmarks.DepthFirstTreenumerator.CompleteBinaryTree_21",
            "value": 1925,
            "unit": "bytes"
          },
          {
            "name": "Arborist.Benchmarks.DepthFirstTreenumerator.TrivialForest_4M",
            "value": 295,
            "unit": "bytes"
          },
          {
            "name": "Arborist.Benchmarks.DepthFirstTreenumerator.DegenerateTree_4M",
            "value": 33557600,
            "unit": "bytes"
          },
          {
            "name": "Arborist.Benchmarks.LevelOrderTraversal.DeepTree",
            "value": 2930,
            "unit": "bytes"
          },
          {
            "name": "Arborist.Benchmarks.LevelOrderTraversal.TriangleTree_PruneAfter_1447",
            "value": 232393,
            "unit": "bytes"
          },
          {
            "name": "Arborist.Benchmarks.LevelOrderTraversal.CompleteBinaryTree_PruneBefore_20",
            "value": 41949805,
            "unit": "bytes"
          },
          {
            "name": "Arborist.Benchmarks.LevelOrderTraversal.CompleteBinaryTree_PruneAfter_19",
            "value": 25174659,
            "unit": "bytes"
          },
          {
            "name": "Arborist.Benchmarks.PostOrderTraversal.DeepTree",
            "value": 4198220,
            "unit": "bytes"
          },
          {
            "name": "Arborist.Benchmarks.PostOrderTraversal.TriangleTree_PruneAfter_1447",
            "value": 35173,
            "unit": "bytes"
          },
          {
            "name": "Arborist.Benchmarks.PostOrderTraversal.CompleteBinaryTree_PruneBefore_20",
            "value": 3272,
            "unit": "bytes"
          },
          {
            "name": "Arborist.Benchmarks.PostOrderTraversal.CompleteBinaryTree_PruneAfter_19",
            "value": 1954,
            "unit": "bytes"
          },
          {
            "name": "Arborist.Benchmarks.PreorderTraversal.DeepTree",
            "value": 2099402,
            "unit": "bytes"
          },
          {
            "name": "Arborist.Benchmarks.PreorderTraversal.TriangleTree_PruneAfter_1447",
            "value": 26225,
            "unit": "bytes"
          },
          {
            "name": "Arborist.Benchmarks.PreorderTraversal.CompleteBinaryTree_PruneBefore_20",
            "value": 2824,
            "unit": "bytes"
          },
          {
            "name": "Arborist.Benchmarks.PreorderTraversal.CompleteBinaryTree_PruneAfterDepth_19",
            "value": 1491,
            "unit": "bytes"
          }
        ]
      },
      {
        "commit": {
          "author": {
            "email": "jason.boyd.ce@gmail.com",
            "name": "Jason Boyd",
            "username": "jasonmcboyd"
          },
          "committer": {
            "email": "jason.boyd.ce@gmail.com",
            "name": "Jason Boyd",
            "username": "jasonmcboyd"
          },
          "distinct": true,
          "id": "5993e5762d3c0ffa1bbacbb40b35307cff9a3f87",
          "message": "Docs: document BFT Where O(N) prefix-carry design\n\nRewrite the WhereBreadthFirstTreenumerator section to match the implementation (queue + incremental _PredSkipPrefix, predicate-skip vs consumer-SkipNode, the real parent-visit fields, front-anchored sibling index). Update the DFT-vs-BFT table (BFT now O(N)) and fix stale _ExtraParentVisitsEmitted/_LastRemovedSkippedNodePosition/_SkippedStack references.\n\nCo-Authored-By: Claude Opus 4.8 (1M context) <noreply@anthropic.com>\nClaude-Session: https://claude.ai/code/session_01Wg3xArL4FATQaXQMBvhXdg",
          "timestamp": "2026-06-23T04:33:54Z",
          "tree_id": "a2615b3ce8e91df5baffd5f8f08e3c6ac9df2e3b",
          "url": "https://github.com/jasonmcboyd/Arborist/commit/5993e5762d3c0ffa1bbacbb40b35307cff9a3f87"
        },
        "date": 1782190985028,
        "tool": "customSmallerIsBetter",
        "benches": [
          {
            "name": "Arborist.Benchmarks.BreadthFirstTreenumerator.TriangleTree_2896",
            "value": 527480,
            "unit": "bytes"
          },
          {
            "name": "Arborist.Benchmarks.BreadthFirstTreenumerator.CompleteBinaryTree_21",
            "value": 117446144,
            "unit": "bytes"
          },
          {
            "name": "Arborist.Benchmarks.BreadthFirstTreenumerator.TrivialForest_4M",
            "value": 295,
            "unit": "bytes"
          },
          {
            "name": "Arborist.Benchmarks.BreadthFirstTreenumerator.DegenerateTree_4M",
            "value": 867,
            "unit": "bytes"
          },
          {
            "name": "Arborist.Benchmarks.DepthFirstTreenumerator.TriangleTree_2896",
            "value": 133245,
            "unit": "bytes"
          },
          {
            "name": "Arborist.Benchmarks.DepthFirstTreenumerator.CompleteBinaryTree_21",
            "value": 2061,
            "unit": "bytes"
          },
          {
            "name": "Arborist.Benchmarks.DepthFirstTreenumerator.TrivialForest_4M",
            "value": 295,
            "unit": "bytes"
          },
          {
            "name": "Arborist.Benchmarks.DepthFirstTreenumerator.DegenerateTree_4M",
            "value": 33557608,
            "unit": "bytes"
          },
          {
            "name": "Arborist.Benchmarks.LevelOrderTraversal.DeepTree",
            "value": 3226,
            "unit": "bytes"
          },
          {
            "name": "Arborist.Benchmarks.LevelOrderTraversal.TriangleTree_PruneAfter_1447",
            "value": 265201,
            "unit": "bytes"
          },
          {
            "name": "Arborist.Benchmarks.LevelOrderTraversal.CompleteBinaryTree_PruneBefore_20",
            "value": 58727157,
            "unit": "bytes"
          },
          {
            "name": "Arborist.Benchmarks.LevelOrderTraversal.CompleteBinaryTree_PruneAfter_19",
            "value": 29366085,
            "unit": "bytes"
          },
          {
            "name": "Arborist.Benchmarks.PostOrderTraversal.DeepTree",
            "value": 4198260,
            "unit": "bytes"
          },
          {
            "name": "Arborist.Benchmarks.PostOrderTraversal.TriangleTree_PruneAfter_1447",
            "value": 35217,
            "unit": "bytes"
          },
          {
            "name": "Arborist.Benchmarks.PostOrderTraversal.CompleteBinaryTree_PruneBefore_20",
            "value": 3763,
            "unit": "bytes"
          },
          {
            "name": "Arborist.Benchmarks.PostOrderTraversal.CompleteBinaryTree_PruneAfter_19",
            "value": 1979,
            "unit": "bytes"
          },
          {
            "name": "Arborist.Benchmarks.PreorderTraversal.DeepTree",
            "value": 2099442,
            "unit": "bytes"
          },
          {
            "name": "Arborist.Benchmarks.PreorderTraversal.TriangleTree_PruneAfter_1447",
            "value": 26265,
            "unit": "bytes"
          },
          {
            "name": "Arborist.Benchmarks.PreorderTraversal.CompleteBinaryTree_PruneBefore_20",
            "value": 3315,
            "unit": "bytes"
          },
          {
            "name": "Arborist.Benchmarks.PreorderTraversal.CompleteBinaryTree_PruneAfterDepth_19",
            "value": 1521,
            "unit": "bytes"
          }
        ]
      },
      {
        "commit": {
          "author": {
            "email": "jason.boyd.ce@gmail.com",
            "name": "Jason Boyd",
            "username": "jasonmcboyd"
          },
          "committer": {
            "email": "jason.boyd.ce@gmail.com",
            "name": "Jason Boyd",
            "username": "jasonmcboyd"
          },
          "distinct": true,
          "id": "c3ab5ef05fc7c7304bb98cd14e97bed3a8f91e62",
          "message": "Align LeaffixAggregate with LeaffixScan (flat, lazy, zero per-node alloc)\n\nLeaffixAggregate now uses the same flat forward DFS + no-copy ChildAccumulations view as LeaffixScan, yielding each root's accumulated value. Accumulator changes TAccumulate[] -> ChildAccumulations<TAccumulate>. Per-root lazy: a root is emitted the moment its subtree completes and the buffers are reused for the next root, so peak memory is the largest root subtree (not the whole forest) and early-terminating consumers traverse fewer roots -- matching the previous LeaffixAggregator behavior. Zero per-node heap allocation. Adds LeaffixAggregateTests (it previously had none and no callers).\n\nLeaffixAggregator is retained -- still used by Invert (TreeInverter); it'll be removed when Invert is redesigned onto a flat structure.\n\nCo-Authored-By: Claude Opus 4.8 (1M context) <noreply@anthropic.com>\nClaude-Session: https://claude.ai/code/session_01Wg3xArL4FATQaXQMBvhXdg",
          "timestamp": "2026-06-23T21:57:19Z",
          "tree_id": "7dacc032f0faefb81f1e004514e835a60b5bf81a",
          "url": "https://github.com/jasonmcboyd/Arborist/commit/c3ab5ef05fc7c7304bb98cd14e97bed3a8f91e62"
        },
        "date": 1782253174469,
        "tool": "customSmallerIsBetter",
        "benches": [
          {
            "name": "Arborist.Benchmarks.BreadthFirstTreenumerator.TriangleTree_2896",
            "value": 527480,
            "unit": "bytes"
          },
          {
            "name": "Arborist.Benchmarks.BreadthFirstTreenumerator.CompleteBinaryTree_21",
            "value": 117446144,
            "unit": "bytes"
          },
          {
            "name": "Arborist.Benchmarks.BreadthFirstTreenumerator.TrivialForest_4M",
            "value": 295,
            "unit": "bytes"
          },
          {
            "name": "Arborist.Benchmarks.BreadthFirstTreenumerator.DegenerateTree_4M",
            "value": 867,
            "unit": "bytes"
          },
          {
            "name": "Arborist.Benchmarks.DepthFirstTreenumerator.TriangleTree_2896",
            "value": 133245,
            "unit": "bytes"
          },
          {
            "name": "Arborist.Benchmarks.DepthFirstTreenumerator.CompleteBinaryTree_21",
            "value": 2061,
            "unit": "bytes"
          },
          {
            "name": "Arborist.Benchmarks.DepthFirstTreenumerator.TrivialForest_4M",
            "value": 295,
            "unit": "bytes"
          },
          {
            "name": "Arborist.Benchmarks.DepthFirstTreenumerator.DegenerateTree_4M",
            "value": 33557609,
            "unit": "bytes"
          },
          {
            "name": "Arborist.Benchmarks.LevelOrderTraversal.DeepTree",
            "value": 3226,
            "unit": "bytes"
          },
          {
            "name": "Arborist.Benchmarks.LevelOrderTraversal.TriangleTree_PruneAfter_1447",
            "value": 265201,
            "unit": "bytes"
          },
          {
            "name": "Arborist.Benchmarks.LevelOrderTraversal.CompleteBinaryTree_PruneBefore_20",
            "value": 58727157,
            "unit": "bytes"
          },
          {
            "name": "Arborist.Benchmarks.LevelOrderTraversal.CompleteBinaryTree_PruneAfter_19",
            "value": 29366085,
            "unit": "bytes"
          },
          {
            "name": "Arborist.Benchmarks.PostOrderTraversal.DeepTree",
            "value": 4198260,
            "unit": "bytes"
          },
          {
            "name": "Arborist.Benchmarks.PostOrderTraversal.TriangleTree_PruneAfter_1447",
            "value": 35213,
            "unit": "bytes"
          },
          {
            "name": "Arborist.Benchmarks.PostOrderTraversal.CompleteBinaryTree_PruneBefore_20",
            "value": 3763,
            "unit": "bytes"
          },
          {
            "name": "Arborist.Benchmarks.PostOrderTraversal.CompleteBinaryTree_PruneAfter_19",
            "value": 1979,
            "unit": "bytes"
          },
          {
            "name": "Arborist.Benchmarks.PreorderTraversal.DeepTree",
            "value": 2099442,
            "unit": "bytes"
          },
          {
            "name": "Arborist.Benchmarks.PreorderTraversal.TriangleTree_PruneAfter_1447",
            "value": 26262,
            "unit": "bytes"
          },
          {
            "name": "Arborist.Benchmarks.PreorderTraversal.CompleteBinaryTree_PruneBefore_20",
            "value": 3315,
            "unit": "bytes"
          },
          {
            "name": "Arborist.Benchmarks.PreorderTraversal.CompleteBinaryTree_PruneAfterDepth_19",
            "value": 1521,
            "unit": "bytes"
          }
        ]
      },
      {
        "commit": {
          "author": {
            "email": "jason.boyd.ce@gmail.com",
            "name": "Jason Boyd",
            "username": "jasonmcboyd"
          },
          "committer": {
            "email": "jason.boyd.ce@gmail.com",
            "name": "Jason Boyd",
            "username": "jasonmcboyd"
          },
          "distinct": true,
          "id": "96f3cc44e2208f47258c2887c2a011530d708864",
          "message": "Rewrite Invert flat; retire SimpleNode and LeaffixAggregator\n\nInvert now materializes the source into flat pre-order arrays and emits the mirror with a stack -- pushing roots/children in forward order so they pop in reverse (the mirror's pre-order). Subtree sizes are invariant under mirroring; result is a PreorderTree. O(N), zero per-node allocation (was a SimpleNode object per node via LeaffixAggregator).\n\nInvert was the last user of both SimpleNode and LeaffixAggregator, so this deletes five files: TreeInverter, LeaffixAggregator (+NodeContextAndResult), SimpleNode, SimpleNodeChildEnumerator, SimpleNodeTreenumerable. The SimpleNode retirement is complete, clearing the runway for the nodeToValueMap elimination. (A level-order/LOUDS mirror -- reverse each child-run in place -- remains a future refinement if a LevelOrderTree is ever built.)\n\nCo-Authored-By: Claude Opus 4.8 (1M context) <noreply@anthropic.com>\nClaude-Session: https://claude.ai/code/session_01Wg3xArL4FATQaXQMBvhXdg",
          "timestamp": "2026-06-23T22:21:16Z",
          "tree_id": "aafd024b9cc4740d5e6c4fe63bc7cf95158cbb5e",
          "url": "https://github.com/jasonmcboyd/Arborist/commit/96f3cc44e2208f47258c2887c2a011530d708864"
        },
        "date": 1782254668811,
        "tool": "customSmallerIsBetter",
        "benches": [
          {
            "name": "Arborist.Benchmarks.BreadthFirstTreenumerator.TriangleTree_2896",
            "value": 527480,
            "unit": "bytes"
          },
          {
            "name": "Arborist.Benchmarks.BreadthFirstTreenumerator.CompleteBinaryTree_21",
            "value": 117446144,
            "unit": "bytes"
          },
          {
            "name": "Arborist.Benchmarks.BreadthFirstTreenumerator.TrivialForest_4M",
            "value": 295,
            "unit": "bytes"
          },
          {
            "name": "Arborist.Benchmarks.BreadthFirstTreenumerator.DegenerateTree_4M",
            "value": 843,
            "unit": "bytes"
          },
          {
            "name": "Arborist.Benchmarks.DepthFirstTreenumerator.TriangleTree_2896",
            "value": 133245,
            "unit": "bytes"
          },
          {
            "name": "Arborist.Benchmarks.DepthFirstTreenumerator.CompleteBinaryTree_21",
            "value": 2061,
            "unit": "bytes"
          },
          {
            "name": "Arborist.Benchmarks.DepthFirstTreenumerator.TrivialForest_4M",
            "value": 295,
            "unit": "bytes"
          },
          {
            "name": "Arborist.Benchmarks.DepthFirstTreenumerator.DegenerateTree_4M",
            "value": 33557608,
            "unit": "bytes"
          },
          {
            "name": "Arborist.Benchmarks.LevelOrderTraversal.DeepTree",
            "value": 3226,
            "unit": "bytes"
          },
          {
            "name": "Arborist.Benchmarks.LevelOrderTraversal.TriangleTree_PruneAfter_1447",
            "value": 265201,
            "unit": "bytes"
          },
          {
            "name": "Arborist.Benchmarks.LevelOrderTraversal.CompleteBinaryTree_PruneBefore_20",
            "value": 58727157,
            "unit": "bytes"
          },
          {
            "name": "Arborist.Benchmarks.LevelOrderTraversal.CompleteBinaryTree_PruneAfter_19",
            "value": 29366085,
            "unit": "bytes"
          },
          {
            "name": "Arborist.Benchmarks.PostOrderTraversal.DeepTree",
            "value": 4198260,
            "unit": "bytes"
          },
          {
            "name": "Arborist.Benchmarks.PostOrderTraversal.TriangleTree_PruneAfter_1447",
            "value": 35209,
            "unit": "bytes"
          },
          {
            "name": "Arborist.Benchmarks.PostOrderTraversal.CompleteBinaryTree_PruneBefore_20",
            "value": 3763,
            "unit": "bytes"
          },
          {
            "name": "Arborist.Benchmarks.PostOrderTraversal.CompleteBinaryTree_PruneAfter_19",
            "value": 1969,
            "unit": "bytes"
          },
          {
            "name": "Arborist.Benchmarks.PreorderTraversal.DeepTree",
            "value": 2099442,
            "unit": "bytes"
          },
          {
            "name": "Arborist.Benchmarks.PreorderTraversal.TriangleTree_PruneAfter_1447",
            "value": 26239,
            "unit": "bytes"
          },
          {
            "name": "Arborist.Benchmarks.PreorderTraversal.CompleteBinaryTree_PruneBefore_20",
            "value": 3315,
            "unit": "bytes"
          },
          {
            "name": "Arborist.Benchmarks.PreorderTraversal.CompleteBinaryTree_PruneAfterDepth_19",
            "value": 1513,
            "unit": "bytes"
          }
        ]
      },
      {
        "commit": {
          "author": {
            "email": "jason.boyd.ce@gmail.com",
            "name": "Jason Boyd",
            "username": "jasonmcboyd"
          },
          "committer": {
            "email": "jason.boyd.ce@gmail.com",
            "name": "Jason Boyd",
            "username": "jasonmcboyd"
          },
          "distinct": true,
          "id": "7c795cd25f5b93fbac1993cf6a71485ef8c2de74",
          "message": "Add MemoryDiagnoser benchmark for Invert\n\nCovers TriangleTree depth-1448 (~1M) and a 1M-deep DegenerateTree, tagged LINQ for the dashboard. Completes benchmark coverage of the now-flat transform/aggregation ops (Materialize, LeaffixScan, LeaffixAggregate, Invert).\n\nCo-Authored-By: Claude Opus 4.8 (1M context) <noreply@anthropic.com>\nClaude-Session: https://claude.ai/code/session_01Wg3xArL4FATQaXQMBvhXdg",
          "timestamp": "2026-06-23T22:22:47Z",
          "tree_id": "547984a2584ebd974bf3f4a1790b68fc9b328cfc",
          "url": "https://github.com/jasonmcboyd/Arborist/commit/7c795cd25f5b93fbac1993cf6a71485ef8c2de74"
        },
        "date": 1782256079327,
        "tool": "customSmallerIsBetter",
        "benches": [
          {
            "name": "Arborist.Benchmarks.BreadthFirstTreenumerator.TriangleTree_2896",
            "value": 527480,
            "unit": "bytes"
          },
          {
            "name": "Arborist.Benchmarks.BreadthFirstTreenumerator.CompleteBinaryTree_21",
            "value": 117450220,
            "unit": "bytes"
          },
          {
            "name": "Arborist.Benchmarks.BreadthFirstTreenumerator.TrivialForest_4M",
            "value": 295,
            "unit": "bytes"
          },
          {
            "name": "Arborist.Benchmarks.BreadthFirstTreenumerator.DegenerateTree_4M",
            "value": 867,
            "unit": "bytes"
          },
          {
            "name": "Arborist.Benchmarks.DepthFirstTreenumerator.TriangleTree_2896",
            "value": 133245,
            "unit": "bytes"
          },
          {
            "name": "Arborist.Benchmarks.DepthFirstTreenumerator.CompleteBinaryTree_21",
            "value": 2184,
            "unit": "bytes"
          },
          {
            "name": "Arborist.Benchmarks.DepthFirstTreenumerator.TrivialForest_4M",
            "value": 295,
            "unit": "bytes"
          },
          {
            "name": "Arborist.Benchmarks.DepthFirstTreenumerator.DegenerateTree_4M",
            "value": 33557608,
            "unit": "bytes"
          },
          {
            "name": "Arborist.Benchmarks.LevelOrderTraversal.DeepTree",
            "value": 3234,
            "unit": "bytes"
          },
          {
            "name": "Arborist.Benchmarks.LevelOrderTraversal.TriangleTree_PruneAfter_1447",
            "value": 265219,
            "unit": "bytes"
          },
          {
            "name": "Arborist.Benchmarks.LevelOrderTraversal.CompleteBinaryTree_PruneBefore_20",
            "value": 58732544,
            "unit": "bytes"
          },
          {
            "name": "Arborist.Benchmarks.LevelOrderTraversal.CompleteBinaryTree_PruneAfter_19",
            "value": 29368803,
            "unit": "bytes"
          },
          {
            "name": "Arborist.Benchmarks.PostOrderTraversal.DeepTree",
            "value": 4198261,
            "unit": "bytes"
          },
          {
            "name": "Arborist.Benchmarks.PostOrderTraversal.TriangleTree_PruneAfter_1447",
            "value": 35217,
            "unit": "bytes"
          },
          {
            "name": "Arborist.Benchmarks.PostOrderTraversal.CompleteBinaryTree_PruneBefore_20",
            "value": 3800,
            "unit": "bytes"
          },
          {
            "name": "Arborist.Benchmarks.PostOrderTraversal.CompleteBinaryTree_PruneAfter_19",
            "value": 1979,
            "unit": "bytes"
          },
          {
            "name": "Arborist.Benchmarks.PreorderTraversal.DeepTree",
            "value": 2099442,
            "unit": "bytes"
          },
          {
            "name": "Arborist.Benchmarks.PreorderTraversal.TriangleTree_PruneAfter_1447",
            "value": 26269,
            "unit": "bytes"
          },
          {
            "name": "Arborist.Benchmarks.PreorderTraversal.CompleteBinaryTree_PruneBefore_20",
            "value": 3315,
            "unit": "bytes"
          },
          {
            "name": "Arborist.Benchmarks.PreorderTraversal.CompleteBinaryTree_PruneAfterDepth_19",
            "value": 1521,
            "unit": "bytes"
          }
        ]
      },
      {
        "commit": {
          "author": {
            "email": "jason.boyd.ce@gmail.com",
            "name": "Jason Boyd",
            "username": "jasonmcboyd"
          },
          "committer": {
            "email": "jason.boyd.ce@gmail.com",
            "name": "Jason Boyd",
            "username": "jasonmcboyd"
          },
          "distinct": true,
          "id": "cf2c06864e8fdc1e6c24df1f1437f0dae1da26b0",
          "message": "Add 2-param Treenumerable base; drop redundant identity maps (map cleanup stage 1)\n\nThe sample trees whose node IS their surfaced value (TriangleTree, CollatzTree, CompleteBinaryTree, NDecrementTree, DeepTree) were each passing a redundant 'node => node' nodeToValueMap and carrying a duplicate type parameter. Add a 2-param Treenumerable<TNode, TChildEnumerator> convenience base (identity map) and migrate them to it. Public surfaces unchanged. The 3-param base + map remain for PreorderTree's genuine index->value dereference.\n\nCo-Authored-By: Claude Opus 4.8 (1M context) <noreply@anthropic.com>\nClaude-Session: https://claude.ai/code/session_01Wg3xArL4FATQaXQMBvhXdg",
          "timestamp": "2026-06-23T22:35:56Z",
          "tree_id": "005c51f9d7f00116b4e04d1cdb2a5fd0680fc86e",
          "url": "https://github.com/jasonmcboyd/Arborist/commit/cf2c06864e8fdc1e6c24df1f1437f0dae1da26b0"
        },
        "date": 1782257438473,
        "tool": "customSmallerIsBetter",
        "benches": [
          {
            "name": "Arborist.Benchmarks.BreadthFirstTreenumerator.TriangleTree_2896",
            "value": 527480,
            "unit": "bytes"
          },
          {
            "name": "Arborist.Benchmarks.BreadthFirstTreenumerator.CompleteBinaryTree_21",
            "value": 117446144,
            "unit": "bytes"
          },
          {
            "name": "Arborist.Benchmarks.BreadthFirstTreenumerator.TrivialForest_4M",
            "value": 295,
            "unit": "bytes"
          },
          {
            "name": "Arborist.Benchmarks.BreadthFirstTreenumerator.DegenerateTree_4M",
            "value": 843,
            "unit": "bytes"
          },
          {
            "name": "Arborist.Benchmarks.DepthFirstTreenumerator.TriangleTree_2896",
            "value": 133245,
            "unit": "bytes"
          },
          {
            "name": "Arborist.Benchmarks.DepthFirstTreenumerator.CompleteBinaryTree_21",
            "value": 2061,
            "unit": "bytes"
          },
          {
            "name": "Arborist.Benchmarks.DepthFirstTreenumerator.TrivialForest_4M",
            "value": 295,
            "unit": "bytes"
          },
          {
            "name": "Arborist.Benchmarks.DepthFirstTreenumerator.DegenerateTree_4M",
            "value": 33557608,
            "unit": "bytes"
          },
          {
            "name": "Arborist.Benchmarks.LevelOrderTraversal.DeepTree",
            "value": 3226,
            "unit": "bytes"
          },
          {
            "name": "Arborist.Benchmarks.LevelOrderTraversal.TriangleTree_PruneAfter_1447",
            "value": 265201,
            "unit": "bytes"
          },
          {
            "name": "Arborist.Benchmarks.LevelOrderTraversal.CompleteBinaryTree_PruneBefore_20",
            "value": 58727157,
            "unit": "bytes"
          },
          {
            "name": "Arborist.Benchmarks.LevelOrderTraversal.CompleteBinaryTree_PruneAfter_19",
            "value": 29366085,
            "unit": "bytes"
          },
          {
            "name": "Arborist.Benchmarks.PostOrderTraversal.DeepTree",
            "value": 4198260,
            "unit": "bytes"
          },
          {
            "name": "Arborist.Benchmarks.PostOrderTraversal.TriangleTree_PruneAfter_1447",
            "value": 35213,
            "unit": "bytes"
          },
          {
            "name": "Arborist.Benchmarks.PostOrderTraversal.CompleteBinaryTree_PruneBefore_20",
            "value": 3763,
            "unit": "bytes"
          },
          {
            "name": "Arborist.Benchmarks.PostOrderTraversal.CompleteBinaryTree_PruneAfter_19",
            "value": 1969,
            "unit": "bytes"
          },
          {
            "name": "Arborist.Benchmarks.PreorderTraversal.DeepTree",
            "value": 2099442,
            "unit": "bytes"
          },
          {
            "name": "Arborist.Benchmarks.PreorderTraversal.TriangleTree_PruneAfter_1447",
            "value": 26262,
            "unit": "bytes"
          },
          {
            "name": "Arborist.Benchmarks.PreorderTraversal.CompleteBinaryTree_PruneBefore_20",
            "value": 3315,
            "unit": "bytes"
          },
          {
            "name": "Arborist.Benchmarks.PreorderTraversal.CompleteBinaryTree_PruneAfterDepth_19",
            "value": 1517,
            "unit": "bytes"
          }
        ]
      },
      {
        "commit": {
          "author": {
            "email": "jason.boyd.ce@gmail.com",
            "name": "Jason Boyd",
            "username": "jasonmcboyd"
          },
          "committer": {
            "email": "jason.boyd.ce@gmail.com",
            "name": "Jason Boyd",
            "username": "jasonmcboyd"
          },
          "distinct": true,
          "id": "ca567e0f439d6d1f0f5c2143add771a6c13fd41c",
          "message": "Fix BFT Where O(depth) memory regression: tail-carry skip prefix\n\nf7eae61's O(N)-time prefix carry stored _PredSkipPrefix as a List<int>\nindexed by absolute inner depth, grown to the current depth on every\nscheduled node -- so a 1M-deep degenerate Where(_=>true) chain allocated\n~8.39 MB even though nothing is predicate-skipped (every entry 0).\n\nReplace it with a tail-carry: the prefix is monotonic non-decreasing in\ndepth and constant (= total skips on the path) beyond the deepest skipped\nancestor, so store only up to the deepest skip and serve reads past the\nend from a scalar tail. New _PrefixStored + _PrefixStoredCount +\n_PrefixTail; PrefixRead/PrefixWriteScheduled (no-op when value == tail, so\nzero allocation in the accepted region); PrefixAnchor truncates the stored\ncount to frontDepth-1 on front-advance.\n\nWhereAll 8.39 MB -> ~1.9 KB (O(1) in depth); WhereNone byte-identical\n(inherent O(depth) preserved). Visit stream byte-identical: validated\nagainst Where2InProcessScan (full c..i, 891k cases) + WhereTests (218/0),\nnet48 + net8.0 clean. Add WhereBreadthFirstAllocationTests as a hard\nmemory-bound regression guard (the gh-pages benchmark only soft-alerts).\n\nCo-Authored-By: Claude Opus 4.8 (1M context) <noreply@anthropic.com>\nClaude-Session: https://claude.ai/code/session_01Wg3xArL4FATQaXQMBvhXdg",
          "timestamp": "2026-06-25T18:13:44Z",
          "tree_id": "6d5c2256d2c8574537a4127f73a8de04fe0b4011",
          "url": "https://github.com/jasonmcboyd/Arborist/commit/ca567e0f439d6d1f0f5c2143add771a6c13fd41c"
        },
        "date": 1782413421082,
        "tool": "customSmallerIsBetter",
        "benches": [
          {
            "name": "Arborist.Benchmarks.BreadthFirstTreenumerator.TriangleTree_2896",
            "value": 527480,
            "unit": "bytes"
          },
          {
            "name": "Arborist.Benchmarks.BreadthFirstTreenumerator.CompleteBinaryTree_21",
            "value": 117452312,
            "unit": "bytes"
          },
          {
            "name": "Arborist.Benchmarks.BreadthFirstTreenumerator.TrivialForest_4M",
            "value": 295,
            "unit": "bytes"
          },
          {
            "name": "Arborist.Benchmarks.BreadthFirstTreenumerator.DegenerateTree_4M",
            "value": 867,
            "unit": "bytes"
          },
          {
            "name": "Arborist.Benchmarks.DepthFirstTreenumerator.TriangleTree_2896",
            "value": 133245,
            "unit": "bytes"
          },
          {
            "name": "Arborist.Benchmarks.DepthFirstTreenumerator.CompleteBinaryTree_21",
            "value": 2061,
            "unit": "bytes"
          },
          {
            "name": "Arborist.Benchmarks.DepthFirstTreenumerator.TrivialForest_4M",
            "value": 295,
            "unit": "bytes"
          },
          {
            "name": "Arborist.Benchmarks.DepthFirstTreenumerator.DegenerateTree_4M",
            "value": 33557609,
            "unit": "bytes"
          },
          {
            "name": "Arborist.Benchmarks.LevelOrderTraversal.DeepTree",
            "value": 3226,
            "unit": "bytes"
          },
          {
            "name": "Arborist.Benchmarks.LevelOrderTraversal.TriangleTree_PruneAfter_1447",
            "value": 265219,
            "unit": "bytes"
          },
          {
            "name": "Arborist.Benchmarks.LevelOrderTraversal.CompleteBinaryTree_PruneBefore_20",
            "value": 58727165,
            "unit": "bytes"
          },
          {
            "name": "Arborist.Benchmarks.LevelOrderTraversal.CompleteBinaryTree_PruneAfter_19",
            "value": 29367444,
            "unit": "bytes"
          },
          {
            "name": "Arborist.Benchmarks.PostOrderTraversal.DeepTree",
            "value": 4198260,
            "unit": "bytes"
          },
          {
            "name": "Arborist.Benchmarks.PostOrderTraversal.TriangleTree_PruneAfter_1447",
            "value": 35213,
            "unit": "bytes"
          },
          {
            "name": "Arborist.Benchmarks.PostOrderTraversal.CompleteBinaryTree_PruneBefore_20",
            "value": 3763,
            "unit": "bytes"
          },
          {
            "name": "Arborist.Benchmarks.PostOrderTraversal.CompleteBinaryTree_PruneAfter_19",
            "value": 1979,
            "unit": "bytes"
          },
          {
            "name": "Arborist.Benchmarks.PreorderTraversal.DeepTree",
            "value": 2099442,
            "unit": "bytes"
          },
          {
            "name": "Arborist.Benchmarks.PreorderTraversal.TriangleTree_PruneAfter_1447",
            "value": 26265,
            "unit": "bytes"
          },
          {
            "name": "Arborist.Benchmarks.PreorderTraversal.CompleteBinaryTree_PruneBefore_20",
            "value": 3315,
            "unit": "bytes"
          },
          {
            "name": "Arborist.Benchmarks.PreorderTraversal.CompleteBinaryTree_PruneAfterDepth_19",
            "value": 1521,
            "unit": "bytes"
          }
        ]
      },
      {
        "commit": {
          "author": {
            "email": "jason.boyd.ce@gmail.com",
            "name": "Jason Boyd",
            "username": "jasonmcboyd"
          },
          "committer": {
            "email": "jason.boyd.ce@gmail.com",
            "name": "Jason Boyd",
            "username": "jasonmcboyd"
          },
          "distinct": true,
          "id": "6a5d01f5e6181c1c89a0993a6360bab3a4b0bac7",
          "message": "Rewrite BreadthFirstTreenumerator with a structural visit cadence\n\nThe base BFT engine emitted parent visits reactively (when a child was\naccepted), which broke whenever a child was filtered and forced a tangle\nof deferred parent-visit bookkeeping to recover the swallowed visits.\n\nReplace it with a structural cadence: a single FIFO _VisitQueue whose\nfront is the active parent, plus a LIFO _ScheduleStack for the SkipNode\npromotion descent. A parent is visited once when it reaches the front,\nthen once after every child slot that enqueues at least one accepted\nnode -- a single bool, _CurrentSlotEnqueuedNode. Roots are scheduled\nfirst as the children of an implicit no-visit forest sentinel.\n\nThis deletes _OwesPromotedParentVisit, _HasDeferredScheduledChild,\n_DepthOfLastActedOnNode, PayOwedParentVisitAndDeferChild, and the\nOnScheduling/OnVisiting/PromoteChildren/SkipSubtree/Backtrack web in\nfavor of Advance/ApplyStrategy/SkipRemainingSiblings. The now-unused\nOwesPromotedParentVisit field comes off the shared InternalNodeVisitState\nstruct, shrinking every deque slot.\n\nNo behavior change: same (Mode, Node, VisitCount, Position) visit stream.\n\nValidation:\n- Exhaustive BFT-vs-DFT oracle, up to 6 concurrent skips x 27 trees\n- Where2InProcessScan: 891,056 Where-wrapper-vs-oracle cases (groups c..i)\n- Curated exact-order traversal + 14,759 Where/allocation tests\n- Benchmarks: allocations -12% to -14%, time within ShortRun noise\n\nCo-Authored-By: Claude Opus 4.8 (1M context) <noreply@anthropic.com>\nClaude-Session: https://claude.ai/code/session_01Wg3xArL4FATQaXQMBvhXdg",
          "timestamp": "2026-06-26T00:47:42Z",
          "tree_id": "39f01ca28e49bf3bbc704caca59e8d4bb815b334",
          "url": "https://github.com/jasonmcboyd/Arborist/commit/6a5d01f5e6181c1c89a0993a6360bab3a4b0bac7"
        },
        "date": 1782436872192,
        "tool": "customSmallerIsBetter",
        "benches": [
          {
            "name": "Arborist.Benchmarks.BreadthFirstTreenumerator.TriangleTree_2896",
            "value": 461904,
            "unit": "bytes"
          },
          {
            "name": "Arborist.Benchmarks.BreadthFirstTreenumerator.CompleteBinaryTree_21",
            "value": 100672304,
            "unit": "bytes"
          },
          {
            "name": "Arborist.Benchmarks.BreadthFirstTreenumerator.TrivialForest_4M",
            "value": 295,
            "unit": "bytes"
          },
          {
            "name": "Arborist.Benchmarks.BreadthFirstTreenumerator.DegenerateTree_4M",
            "value": 867,
            "unit": "bytes"
          },
          {
            "name": "Arborist.Benchmarks.DepthFirstTreenumerator.TriangleTree_2896",
            "value": 116861,
            "unit": "bytes"
          },
          {
            "name": "Arborist.Benchmarks.DepthFirstTreenumerator.CompleteBinaryTree_21",
            "value": 1933,
            "unit": "bytes"
          },
          {
            "name": "Arborist.Benchmarks.DepthFirstTreenumerator.TrivialForest_4M",
            "value": 295,
            "unit": "bytes"
          },
          {
            "name": "Arborist.Benchmarks.DepthFirstTreenumerator.DegenerateTree_4M",
            "value": 33557609,
            "unit": "bytes"
          },
          {
            "name": "Arborist.Benchmarks.LevelOrderTraversal.DeepTree",
            "value": 2938,
            "unit": "bytes"
          },
          {
            "name": "Arborist.Benchmarks.LevelOrderTraversal.TriangleTree_PruneAfter_1447",
            "value": 232393,
            "unit": "bytes"
          },
          {
            "name": "Arborist.Benchmarks.LevelOrderTraversal.CompleteBinaryTree_PruneBefore_20",
            "value": 54535539,
            "unit": "bytes"
          },
          {
            "name": "Arborist.Benchmarks.LevelOrderTraversal.CompleteBinaryTree_PruneAfter_19",
            "value": 25174659,
            "unit": "bytes"
          },
          {
            "name": "Arborist.Benchmarks.PostOrderTraversal.DeepTree",
            "value": 4198228,
            "unit": "bytes"
          },
          {
            "name": "Arborist.Benchmarks.PostOrderTraversal.TriangleTree_PruneAfter_1447",
            "value": 35185,
            "unit": "bytes"
          },
          {
            "name": "Arborist.Benchmarks.PostOrderTraversal.CompleteBinaryTree_PruneBefore_20",
            "value": 3731,
            "unit": "bytes"
          },
          {
            "name": "Arborist.Benchmarks.PostOrderTraversal.CompleteBinaryTree_PruneAfter_19",
            "value": 1947,
            "unit": "bytes"
          },
          {
            "name": "Arborist.Benchmarks.PreorderTraversal.DeepTree",
            "value": 2099410,
            "unit": "bytes"
          },
          {
            "name": "Arborist.Benchmarks.PreorderTraversal.TriangleTree_PruneAfter_1447",
            "value": 26233,
            "unit": "bytes"
          },
          {
            "name": "Arborist.Benchmarks.PreorderTraversal.CompleteBinaryTree_PruneBefore_20",
            "value": 3283,
            "unit": "bytes"
          },
          {
            "name": "Arborist.Benchmarks.PreorderTraversal.CompleteBinaryTree_PruneAfterDepth_19",
            "value": 1489,
            "unit": "bytes"
          }
        ]
      },
      {
        "commit": {
          "author": {
            "email": "jason.boyd.ce@gmail.com",
            "name": "Jason Boyd",
            "username": "jasonmcboyd"
          },
          "committer": {
            "email": "jason.boyd.ce@gmail.com",
            "name": "Jason Boyd",
            "username": "jasonmcboyd"
          },
          "distinct": true,
          "id": "ce22f8e1055cf2b7bf6486f1da78d2058d8c69aa",
          "message": "Cap RefSemiDeque partition size to bound LOH allocation and overshoot\n\nGeometric partition growth sized each new partition to the running total\nCapacity, so the largest partition reached ~half the deque's peak element\ncount -- a multi-MB Large Object Heap allocation on wide/deep trees, plus\nup to ~2x peak over-allocation at power-of-two boundaries. Cap partition\nsize at 4096 elements (Math.Min(Capacity, MaxPartitionSize)) to bound both.\n\nBFT CompleteBinaryTree_21 (peak frontier ~2^21, the worst-case boundary):\n96 MB -> 48 MB allocated per traversal, throughput unchanged.\n\nFixed element count rather than a byte budget that would force partitions\nsub-LOH: forcing a 64 B node's partitions sub-LOH measured ~40% slower with\n~7x the Gen0 collections, because large long-lived blocks belong on the LOH.\n\nAdd RefSemiDeque regression tests crossing the cap (heterogeneous-partition\nordering, out-of-order recycling, GetFromBack/RemoveLast across boundaries)\nand an Add_Block64_1M benchmark. Remove unused InitialCapacity.\n\nCo-Authored-By: Claude Opus 4.8 (1M context) <noreply@anthropic.com>",
          "timestamp": "2026-06-26T04:13:38Z",
          "tree_id": "e35c9314063b1f6eda6dfccd1d7349907af73e32",
          "url": "https://github.com/jasonmcboyd/Arborist/commit/ce22f8e1055cf2b7bf6486f1da78d2058d8c69aa"
        },
        "date": 1782449421972,
        "tool": "customSmallerIsBetter",
        "benches": [
          {
            "name": "Arborist.Benchmarks.BreadthFirstTreenumerator.TriangleTree_2896",
            "value": 347184,
            "unit": "bytes"
          },
          {
            "name": "Arborist.Benchmarks.BreadthFirstTreenumerator.CompleteBinaryTree_21",
            "value": 50515280,
            "unit": "bytes"
          },
          {
            "name": "Arborist.Benchmarks.BreadthFirstTreenumerator.TrivialForest_4M",
            "value": 295,
            "unit": "bytes"
          },
          {
            "name": "Arborist.Benchmarks.BreadthFirstTreenumerator.DegenerateTree_4M",
            "value": 859,
            "unit": "bytes"
          },
          {
            "name": "Arborist.Benchmarks.DepthFirstTreenumerator.TriangleTree_2896",
            "value": 116968,
            "unit": "bytes"
          },
          {
            "name": "Arborist.Benchmarks.DepthFirstTreenumerator.CompleteBinaryTree_21",
            "value": 1917,
            "unit": "bytes"
          },
          {
            "name": "Arborist.Benchmarks.DepthFirstTreenumerator.TrivialForest_4M",
            "value": 295,
            "unit": "bytes"
          },
          {
            "name": "Arborist.Benchmarks.DepthFirstTreenumerator.DegenerateTree_4M",
            "value": 32089735,
            "unit": "bytes"
          },
          {
            "name": "Arborist.Benchmarks.LevelOrderTraversal.DeepTree",
            "value": 2906,
            "unit": "bytes"
          },
          {
            "name": "Arborist.Benchmarks.LevelOrderTraversal.TriangleTree_PruneAfter_1447",
            "value": 232361,
            "unit": "bytes"
          },
          {
            "name": "Arborist.Benchmarks.LevelOrderTraversal.CompleteBinaryTree_PruneBefore_20",
            "value": 27510995,
            "unit": "bytes"
          },
          {
            "name": "Arborist.Benchmarks.LevelOrderTraversal.CompleteBinaryTree_PruneAfter_19",
            "value": 12702475,
            "unit": "bytes"
          },
          {
            "name": "Arborist.Benchmarks.PostOrderTraversal.DeepTree",
            "value": 4214854,
            "unit": "bytes"
          },
          {
            "name": "Arborist.Benchmarks.PostOrderTraversal.TriangleTree_PruneAfter_1447",
            "value": 35157,
            "unit": "bytes"
          },
          {
            "name": "Arborist.Benchmarks.PostOrderTraversal.CompleteBinaryTree_PruneBefore_20",
            "value": 3728,
            "unit": "bytes"
          },
          {
            "name": "Arborist.Benchmarks.PostOrderTraversal.CompleteBinaryTree_PruneAfter_19",
            "value": 1923,
            "unit": "bytes"
          },
          {
            "name": "Arborist.Benchmarks.PreorderTraversal.DeepTree",
            "value": 2107719,
            "unit": "bytes"
          },
          {
            "name": "Arborist.Benchmarks.PreorderTraversal.TriangleTree_PruneAfter_1447",
            "value": 26214,
            "unit": "bytes"
          },
          {
            "name": "Arborist.Benchmarks.PreorderTraversal.CompleteBinaryTree_PruneBefore_20",
            "value": 3251,
            "unit": "bytes"
          },
          {
            "name": "Arborist.Benchmarks.PreorderTraversal.CompleteBinaryTree_PruneAfterDepth_19",
            "value": 1473,
            "unit": "bytes"
          }
        ]
      },
      {
        "commit": {
          "author": {
            "email": "jason.boyd.ce@gmail.com",
            "name": "Jason Boyd",
            "username": "jasonmcboyd"
          },
          "committer": {
            "email": "jason.boyd.ce@gmail.com",
            "name": "Jason Boyd",
            "username": "jasonmcboyd"
          },
          "distinct": true,
          "id": "81b46c42702e71ad313d08c636eb3e3c3c35b140",
          "message": "Cohere BreadthFirstTreenumerator's four deques into one frame stack\n\nThe BFT kept each node's visit state and its child enumerator in FOUR parallel\nRefSemiDeques -- a state deque and an enumerator deque for each of the visit\nqueue and the schedule stack -- relying on keeping each pair in lockstep.\n\nFold each pair into one RefSemiDeque<Frame>, where Frame bundles\n{Node, Position, VisitCount, ChildEnumerator}, driven only by ref so the\nenumerator is never copied. Accepting a node becomes a single whole-frame move\nfrom the schedule stack to the visit queue, which structurally prevents a node\nand its enumerator from desynchronizing. The algorithm and visit cadence are\nunchanged. Mirrors the depth-first engine's frame stack, and removes the\nnow-unreferenced shared InternalNodeVisitState struct.\n\nNo behavior change: same (Mode, Node, VisitCount, Position) visit stream;\ndisposal (including the deliberate idempotent double-dispose on\nSkipDescendants/SkipSiblings) is at exact parity with the original.\n\nValidation:\n- Exact-order traversal + exhaustive DFT-vs-BFT multiset scan (438/0)\n- Full Arborist.Linq suite incl. Where (14,759/0)\n- Benchmarks (Release/Job.Default, clean tree): TriangleTree 289->255ms (-12%),\n  CompleteBinaryTree 337->281ms (-16%), TrivialForest/DegenerateTree 4M within\n  noise; allocation neutral, still zero per-node heap allocation. Timing\n  variance also dropped (StdDev roughly halved on the dense trees).\n\nCo-Authored-By: Claude Opus 4.8 (1M context) <noreply@anthropic.com>\nClaude-Session: https://claude.ai/code/session_01Wg3xArL4FATQaXQMBvhXdg",
          "timestamp": "2026-06-26T21:37:21Z",
          "tree_id": "05aa22f3461078c05e96518d52da4d56655c250f",
          "url": "https://github.com/jasonmcboyd/Arborist/commit/81b46c42702e71ad313d08c636eb3e3c3c35b140"
        },
        "date": 1782511427542,
        "tool": "customSmallerIsBetter",
        "benches": [
          {
            "name": "Arborist.Benchmarks.BreadthFirstTreenumerator.TriangleTree_2896",
            "value": 345933,
            "unit": "bytes"
          },
          {
            "name": "Arborist.Benchmarks.BreadthFirstTreenumerator.CompleteBinaryTree_21",
            "value": 50478104,
            "unit": "bytes"
          },
          {
            "name": "Arborist.Benchmarks.BreadthFirstTreenumerator.TrivialForest_4M",
            "value": 295,
            "unit": "bytes"
          },
          {
            "name": "Arborist.Benchmarks.BreadthFirstTreenumerator.DegenerateTree_4M",
            "value": 859,
            "unit": "bytes"
          },
          {
            "name": "Arborist.Benchmarks.DepthFirstTreenumerator.TriangleTree_2896",
            "value": 132405,
            "unit": "bytes"
          },
          {
            "name": "Arborist.Benchmarks.DepthFirstTreenumerator.CompleteBinaryTree_21",
            "value": 1725,
            "unit": "bytes"
          },
          {
            "name": "Arborist.Benchmarks.DepthFirstTreenumerator.TrivialForest_4M",
            "value": 295,
            "unit": "bytes"
          },
          {
            "name": "Arborist.Benchmarks.DepthFirstTreenumerator.DegenerateTree_4M",
            "value": 32089742,
            "unit": "bytes"
          },
          {
            "name": "Arborist.Benchmarks.LevelOrderTraversal.DeepTree",
            "value": 2339,
            "unit": "bytes"
          },
          {
            "name": "Arborist.Benchmarks.LevelOrderTraversal.TriangleTree_PruneAfter_1447",
            "value": 231292,
            "unit": "bytes"
          },
          {
            "name": "Arborist.Benchmarks.LevelOrderTraversal.CompleteBinaryTree_PruneBefore_20",
            "value": 27502276,
            "unit": "bytes"
          },
          {
            "name": "Arborist.Benchmarks.LevelOrderTraversal.CompleteBinaryTree_PruneAfter_19",
            "value": 12699325,
            "unit": "bytes"
          },
          {
            "name": "Arborist.Benchmarks.PostOrderTraversal.DeepTree",
            "value": 14706710,
            "unit": "bytes"
          },
          {
            "name": "Arborist.Benchmarks.PostOrderTraversal.TriangleTree_PruneAfter_1447",
            "value": 75809,
            "unit": "bytes"
          },
          {
            "name": "Arborist.Benchmarks.PostOrderTraversal.CompleteBinaryTree_PruneBefore_20",
            "value": 4027,
            "unit": "bytes"
          },
          {
            "name": "Arborist.Benchmarks.PostOrderTraversal.CompleteBinaryTree_PruneAfter_19",
            "value": 2259,
            "unit": "bytes"
          },
          {
            "name": "Arborist.Benchmarks.PreorderTraversal.DeepTree",
            "value": 12598604,
            "unit": "bytes"
          },
          {
            "name": "Arborist.Benchmarks.PreorderTraversal.TriangleTree_PruneAfter_1447",
            "value": 66847,
            "unit": "bytes"
          },
          {
            "name": "Arborist.Benchmarks.PreorderTraversal.CompleteBinaryTree_PruneBefore_20",
            "value": 3587,
            "unit": "bytes"
          },
          {
            "name": "Arborist.Benchmarks.PreorderTraversal.CompleteBinaryTree_PruneAfterDepth_19",
            "value": 1805,
            "unit": "bytes"
          }
        ]
      },
      {
        "commit": {
          "author": {
            "email": "jason.boyd.ce@gmail.com",
            "name": "Jason Boyd",
            "username": "jasonmcboyd"
          },
          "committer": {
            "email": "jason.boyd.ce@gmail.com",
            "name": "Jason Boyd",
            "username": "jasonmcboyd"
          },
          "distinct": true,
          "id": "ab94b3983058299be15f1b4d38b05b21509cb8ad",
          "message": "Encapsulate BFT state in BreadthFirstPath; mirror the DFT driver/state split\n\nMove the breadth-first engine's state -- the visit queue, the schedule stack,\nthe owed-return-visit carry, and the root sibling counter -- into a new\nBreadthFirstPath, leaving BreadthFirstTreenumerator a thin driver, mirroring the\ndepth-first DepthFirstPath split. The cohesive Frame (visit state + child\nenumerator) is kept -- the BFT keeps full state for every resident node anyway,\nso it costs no memory -- so allocation is unchanged from the cohesion engine.\n\nLike DepthFirstPath, BreadthFirstPath is \"sans-I/O\": it never pulls a child; it\nexposes the two active enumerators (the schedule-stack top and the visit-queue\nfront) by ref for the driver to advance. That isolates the engine's asynchronous\noperations to those seams, so a future async BFT can share this class and differ\nonly there. It is a mutable struct embedded as the driver's single _Path field\n(never copied; refs it returns point into the heap deques), keeping dense\ntraversal at the cohesion engine's speed. The two child-pull sites collapse into\none TryScheduleNextChildOf(ref parent).\n\nNo behavior change: same (Mode, Node, VisitCount, Position) visit stream.\n\nValidation:\n- Exact-order traversal + exhaustive DFT-vs-BFT multiset scan (438/0)\n- Full Arborist.Linq suite incl. Where (14,759/0)\n- Benchmarks (Release/Job.Default, same session): TriangleTree 194.5 vs cohesion\n  193.9 ms; CompleteBinaryTree 208.1 vs 213.0 ms (parity). Allocation identical\n  to the cohesion engine (same Frame).\n\nCo-Authored-By: Claude Opus 4.8 (1M context) <noreply@anthropic.com>\nClaude-Session: https://claude.ai/code/session_01Wg3xArL4FATQaXQMBvhXdg",
          "timestamp": "2026-06-27T00:09:54Z",
          "tree_id": "8703f7198030df75b83a7026b75dc74f78b17190",
          "url": "https://github.com/jasonmcboyd/Arborist/commit/ab94b3983058299be15f1b4d38b05b21509cb8ad"
        },
        "date": 1782521636258,
        "tool": "customSmallerIsBetter",
        "benches": [
          {
            "name": "Arborist.Benchmarks.BreadthFirstTreenumerator.TriangleTree_2896",
            "value": 345941,
            "unit": "bytes"
          },
          {
            "name": "Arborist.Benchmarks.BreadthFirstTreenumerator.CompleteBinaryTree_21",
            "value": 50478112,
            "unit": "bytes"
          },
          {
            "name": "Arborist.Benchmarks.BreadthFirstTreenumerator.TrivialForest_4M",
            "value": 295,
            "unit": "bytes"
          },
          {
            "name": "Arborist.Benchmarks.BreadthFirstTreenumerator.DegenerateTree_4M",
            "value": 859,
            "unit": "bytes"
          },
          {
            "name": "Arborist.Benchmarks.DepthFirstTreenumerator.TriangleTree_2896",
            "value": 116845,
            "unit": "bytes"
          },
          {
            "name": "Arborist.Benchmarks.DepthFirstTreenumerator.CompleteBinaryTree_21",
            "value": 1917,
            "unit": "bytes"
          },
          {
            "name": "Arborist.Benchmarks.DepthFirstTreenumerator.TrivialForest_4M",
            "value": 295,
            "unit": "bytes"
          },
          {
            "name": "Arborist.Benchmarks.DepthFirstTreenumerator.DegenerateTree_4M",
            "value": 32089721,
            "unit": "bytes"
          },
          {
            "name": "Arborist.Benchmarks.LevelOrderTraversal.DeepTree",
            "value": 2347,
            "unit": "bytes"
          },
          {
            "name": "Arborist.Benchmarks.LevelOrderTraversal.TriangleTree_PruneAfter_1447",
            "value": 231300,
            "unit": "bytes"
          },
          {
            "name": "Arborist.Benchmarks.LevelOrderTraversal.CompleteBinaryTree_PruneBefore_20",
            "value": 27502284,
            "unit": "bytes"
          },
          {
            "name": "Arborist.Benchmarks.LevelOrderTraversal.CompleteBinaryTree_PruneAfter_19",
            "value": 12698781,
            "unit": "bytes"
          },
          {
            "name": "Arborist.Benchmarks.PostOrderTraversal.DeepTree",
            "value": 4214861,
            "unit": "bytes"
          },
          {
            "name": "Arborist.Benchmarks.PostOrderTraversal.TriangleTree_PruneAfter_1447",
            "value": 35196,
            "unit": "bytes"
          },
          {
            "name": "Arborist.Benchmarks.PostOrderTraversal.CompleteBinaryTree_PruneBefore_20",
            "value": 3691,
            "unit": "bytes"
          },
          {
            "name": "Arborist.Benchmarks.PostOrderTraversal.CompleteBinaryTree_PruneAfter_19",
            "value": 1923,
            "unit": "bytes"
          },
          {
            "name": "Arborist.Benchmarks.PreorderTraversal.DeepTree",
            "value": 2107719,
            "unit": "bytes"
          },
          {
            "name": "Arborist.Benchmarks.PreorderTraversal.TriangleTree_PruneAfter_1447",
            "value": 26250,
            "unit": "bytes"
          },
          {
            "name": "Arborist.Benchmarks.PreorderTraversal.CompleteBinaryTree_PruneBefore_20",
            "value": 3251,
            "unit": "bytes"
          },
          {
            "name": "Arborist.Benchmarks.PreorderTraversal.CompleteBinaryTree_PruneAfterDepth_19",
            "value": 1473,
            "unit": "bytes"
          }
        ]
      },
      {
        "commit": {
          "author": {
            "email": "jason.boyd.ce@gmail.com",
            "name": "Jason Boyd",
            "username": "jasonmcboyd"
          },
          "committer": {
            "email": "jason.boyd.ce@gmail.com",
            "name": "Jason Boyd",
            "username": "jasonmcboyd"
          },
          "distinct": true,
          "id": "6ebd5d0e1d60592672f966eaa4ab81a302c56999",
          "message": "Fix DFT skip-heavy regression: keep TryPushNextChild out-of-line\n\nThe encapsulated DepthFirstPath DFT (14f8393) ran ~1.7-1.9x slower than the\noriginal two-stack on promotion-heavy skip traversal (SkipAllNodes / Preorder /\nPostorder on wide trees), despite identical allocation. JIT disassembly\n(DOTNET_JitDisasm) pinned the cause: [AggressiveInlining] on TryPushNextChild\ninlined the entire promote body (pull + push) into OnMoveNext/OnScheduling,\ninflating their frames to 6 callee-saved registers + sub rsp,72 + vzeroupper and\ndowngrading OnMoveNext's branch dispatch from a tail-jmp to a call+teardown paid\non EVERY node. The original two-stack stayed fast precisely because its promote\nwas a separate, out-of-line method.\n\nMark TryPushNextChild [NoInlining] so the drivers stay thin tail-dispatchers,\nwhile keeping the push chain (PushChild/PushLevel) force-inlined INTO it so the\npush itself is still call-free. Also fold Backtrack's pop + three predicate\nchecks into one DepthFirstPath.PopFinishedLevelAndClassify call: the original\ninline-predicate form is O(1) per level but its repeated struct round-trips cost\n~2x on the deep-unwind path (GetLeaves.DeepTree); folding restores it.\n\nNet (Release/Job.Default, local, vs original two-stack): SkipAllNodes.Dft\n41 -> 22.6 ms, Preorder 42.8 -> 25.7, Postorder 47 -> 32.9 -- all back at the\noriginal; dense traversal and GetLeaves.DeepTree within noise; allocation\nunchanged. The sans-I/O encapsulation (path never pulls; one ref seam) is intact.\n\nValidation: 438/0 oracle (exact-order + exhaustive DFT-vs-BFT scan), 14,759/0\nfull Linq suite.\n\nCo-Authored-By: Claude Opus 4.8 (1M context) <noreply@anthropic.com>\nClaude-Session: https://claude.ai/code/session_01Wg3xArL4FATQaXQMBvhXdg",
          "timestamp": "2026-06-27T02:26:54Z",
          "tree_id": "5301780378953d2f586361d71d9e7e7b8dc1e6d3",
          "url": "https://github.com/jasonmcboyd/Arborist/commit/6ebd5d0e1d60592672f966eaa4ab81a302c56999"
        },
        "date": 1782529880550,
        "tool": "customSmallerIsBetter",
        "benches": [
          {
            "name": "Arborist.Benchmarks.BreadthFirstTreenumerator.TriangleTree_2896",
            "value": 345941,
            "unit": "bytes"
          },
          {
            "name": "Arborist.Benchmarks.BreadthFirstTreenumerator.CompleteBinaryTree_21",
            "value": 50478112,
            "unit": "bytes"
          },
          {
            "name": "Arborist.Benchmarks.BreadthFirstTreenumerator.TrivialForest_4M",
            "value": 295,
            "unit": "bytes"
          },
          {
            "name": "Arborist.Benchmarks.BreadthFirstTreenumerator.DegenerateTree_4M",
            "value": 859,
            "unit": "bytes"
          },
          {
            "name": "Arborist.Benchmarks.DepthFirstTreenumerator.TriangleTree_2896",
            "value": 116845,
            "unit": "bytes"
          },
          {
            "name": "Arborist.Benchmarks.DepthFirstTreenumerator.CompleteBinaryTree_21",
            "value": 2040,
            "unit": "bytes"
          },
          {
            "name": "Arborist.Benchmarks.DepthFirstTreenumerator.TrivialForest_4M",
            "value": 295,
            "unit": "bytes"
          },
          {
            "name": "Arborist.Benchmarks.DepthFirstTreenumerator.DegenerateTree_4M",
            "value": 32089714,
            "unit": "bytes"
          },
          {
            "name": "Arborist.Benchmarks.LevelOrderTraversal.DeepTree",
            "value": 2347,
            "unit": "bytes"
          },
          {
            "name": "Arborist.Benchmarks.LevelOrderTraversal.TriangleTree_PruneAfter_1447",
            "value": 231300,
            "unit": "bytes"
          },
          {
            "name": "Arborist.Benchmarks.LevelOrderTraversal.CompleteBinaryTree_PruneBefore_20",
            "value": 27502284,
            "unit": "bytes"
          },
          {
            "name": "Arborist.Benchmarks.LevelOrderTraversal.CompleteBinaryTree_PruneAfter_19",
            "value": 12699328,
            "unit": "bytes"
          },
          {
            "name": "Arborist.Benchmarks.PostOrderTraversal.DeepTree",
            "value": 4214857,
            "unit": "bytes"
          },
          {
            "name": "Arborist.Benchmarks.PostOrderTraversal.TriangleTree_PruneAfter_1447",
            "value": 35165,
            "unit": "bytes"
          },
          {
            "name": "Arborist.Benchmarks.PostOrderTraversal.CompleteBinaryTree_PruneBefore_20",
            "value": 3691,
            "unit": "bytes"
          },
          {
            "name": "Arborist.Benchmarks.PostOrderTraversal.CompleteBinaryTree_PruneAfter_19",
            "value": 1923,
            "unit": "bytes"
          },
          {
            "name": "Arborist.Benchmarks.PreorderTraversal.DeepTree",
            "value": 2107719,
            "unit": "bytes"
          },
          {
            "name": "Arborist.Benchmarks.PreorderTraversal.TriangleTree_PruneAfter_1447",
            "value": 26217,
            "unit": "bytes"
          },
          {
            "name": "Arborist.Benchmarks.PreorderTraversal.CompleteBinaryTree_PruneBefore_20",
            "value": 3288,
            "unit": "bytes"
          },
          {
            "name": "Arborist.Benchmarks.PreorderTraversal.CompleteBinaryTree_PruneAfterDepth_19",
            "value": 1477,
            "unit": "bytes"
          }
        ]
      }
    ],
    "LINQ Memory": [
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
        "date": 1769975446251,
        "tool": "customSmallerIsBetter",
        "benches": [
          {
            "name": "Arborist.Benchmarks.BreadthFirstWhere.TriangleTree_1448",
            "value": 365075,
            "unit": "bytes"
          },
          {
            "name": "Arborist.Benchmarks.BreadthFirstWhere.TrivialForest_WhereAll_1M",
            "value": 1565,
            "unit": "bytes"
          },
          {
            "name": "Arborist.Benchmarks.BreadthFirstWhere.TrivialForest_WhereNone_1M",
            "value": 1316,
            "unit": "bytes"
          },
          {
            "name": "Arborist.Benchmarks.BreadthFirstWhere.DegenerateTree_WhereAll_1M",
            "value": 2049,
            "unit": "bytes"
          },
          {
            "name": "Arborist.Benchmarks.BreadthFirstWhere.DegenerateTree_WhereNone_1M",
            "value": 20975848,
            "unit": "bytes"
          },
          {
            "name": "Arborist.Benchmarks.DepthFirstWhere.TriangleTree_PruneAfter_1448",
            "value": 60699,
            "unit": "bytes"
          },
          {
            "name": "Arborist.Benchmarks.DepthFirstWhere.WhereAll_TrivialForest_1M",
            "value": 1261,
            "unit": "bytes"
          },
          {
            "name": "Arborist.Benchmarks.DepthFirstWhere.WhereNone_TrivialForest_1M",
            "value": 1220,
            "unit": "bytes"
          },
          {
            "name": "Arborist.Benchmarks.DepthFirstWhere.WhereAll_DegenerateTree_1M",
            "value": 25173238,
            "unit": "bytes"
          },
          {
            "name": "Arborist.Benchmarks.DepthFirstWhere.WhereNone_DegenerateTree_1M",
            "value": 1471,
            "unit": "bytes"
          },
          {
            "name": "Arborist.Benchmarks.Select.SelectComposition",
            "value": 871,
            "unit": "bytes"
          },
          {
            "name": "Arborist.Benchmarks.PruneAfter.Bft_TrivialForest_1M",
            "value": 596,
            "unit": "bytes"
          },
          {
            "name": "Arborist.Benchmarks.PruneBefore.Bft_TrivialForest_1M",
            "value": 1252,
            "unit": "bytes"
          },
          {
            "name": "Arborist.Benchmarks.PruneAfter.Dft_TrivialForest_1M",
            "value": 596,
            "unit": "bytes"
          },
          {
            "name": "Arborist.Benchmarks.PruneBefore.Dft_TrivialForest_1M",
            "value": 1284,
            "unit": "bytes"
          },
          {
            "name": "Arborist.Benchmarks.AllNodes.Bft_CompleteBinaryTree_PruneBefore_19",
            "value": 20978989,
            "unit": "bytes"
          },
          {
            "name": "Arborist.Benchmarks.AnyNodes.Bft_CompleteBinaryTree_PruneBefore_19",
            "value": 20978901,
            "unit": "bytes"
          },
          {
            "name": "Arborist.Benchmarks.CountNodes.DeepTree",
            "value": 10492539,
            "unit": "bytes"
          },
          {
            "name": "Arborist.Benchmarks.GetLeaves.DeepTree",
            "value": 1050616,
            "unit": "bytes"
          },
          {
            "name": "Arborist.Benchmarks.AllNodes.Dft_CompleteBinaryTree_PruneBefore_19",
            "value": 2764,
            "unit": "bytes"
          },
          {
            "name": "Arborist.Benchmarks.AnyNodes.Dft_CompleteBinaryTree_PruneBefore_19",
            "value": 2676,
            "unit": "bytes"
          },
          {
            "name": "Arborist.Benchmarks.CountNodes.TriangleTree_PruneAfter_2048",
            "value": 117505,
            "unit": "bytes"
          },
          {
            "name": "Arborist.Benchmarks.GetLeaves.CompleteBinaryTree_PruneBefore_20",
            "value": 2824,
            "unit": "bytes"
          },
          {
            "name": "Arborist.Benchmarks.AllNodes.Bft_TriangleTree_PruneBefore_19",
            "value": 364787,
            "unit": "bytes"
          },
          {
            "name": "Arborist.Benchmarks.AnyNodes.Bft_TriangleTree_PruneBefore_19",
            "value": 364699,
            "unit": "bytes"
          },
          {
            "name": "Arborist.Benchmarks.CountNodes.CompleteBinaryTree_PruneAfter_20",
            "value": 2545,
            "unit": "bytes"
          },
          {
            "name": "Arborist.Benchmarks.AllNodes.Dft_TriangleTree_PruneBefore_19",
            "value": 60204,
            "unit": "bytes"
          },
          {
            "name": "Arborist.Benchmarks.AnyNodes.Dft_TriangleTree_PruneBefore_19",
            "value": 60106,
            "unit": "bytes"
          },
          {
            "name": "Arborist.Benchmarks.SkipAllNodes.Bft_TriangleTree_1448",
            "value": 59935,
            "unit": "bytes"
          },
          {
            "name": "Arborist.Benchmarks.SkipAllNodes.Dft_TriangleTree_1448",
            "value": 26143,
            "unit": "bytes"
          }
        ]
      },
      {
        "commit": {
          "author": {
            "email": "jason.boyd.ce@gmail.com",
            "name": "Jason Boyd",
            "username": "jasonmcboyd"
          },
          "committer": {
            "email": "jason.boyd.ce@gmail.com",
            "name": "Jason Boyd",
            "username": "jasonmcboyd"
          },
          "distinct": true,
          "id": "5993e5762d3c0ffa1bbacbb40b35307cff9a3f87",
          "message": "Docs: document BFT Where O(N) prefix-carry design\n\nRewrite the WhereBreadthFirstTreenumerator section to match the implementation (queue + incremental _PredSkipPrefix, predicate-skip vs consumer-SkipNode, the real parent-visit fields, front-anchored sibling index). Update the DFT-vs-BFT table (BFT now O(N)) and fix stale _ExtraParentVisitsEmitted/_LastRemovedSkippedNodePosition/_SkippedStack references.\n\nCo-Authored-By: Claude Opus 4.8 (1M context) <noreply@anthropic.com>\nClaude-Session: https://claude.ai/code/session_01Wg3xArL4FATQaXQMBvhXdg",
          "timestamp": "2026-06-23T04:33:54Z",
          "tree_id": "a2615b3ce8e91df5baffd5f8f08e3c6ac9df2e3b",
          "url": "https://github.com/jasonmcboyd/Arborist/commit/5993e5762d3c0ffa1bbacbb40b35307cff9a3f87"
        },
        "date": 1782190985222,
        "tool": "customSmallerIsBetter",
        "benches": [
          {
            "name": "Arborist.Benchmarks.BreadthFirstWhere.TriangleTree_1448",
            "value": 512531,
            "unit": "bytes"
          },
          {
            "name": "Arborist.Benchmarks.BreadthFirstWhere.TrivialForest_WhereAll_1M",
            "value": 1570,
            "unit": "bytes"
          },
          {
            "name": "Arborist.Benchmarks.BreadthFirstWhere.TrivialForest_WhereNone_1M",
            "value": 1206,
            "unit": "bytes"
          },
          {
            "name": "Arborist.Benchmarks.BreadthFirstWhere.DegenerateTree_WhereAll_1M",
            "value": 8392287,
            "unit": "bytes"
          },
          {
            "name": "Arborist.Benchmarks.BreadthFirstWhere.DegenerateTree_WhereNone_1M",
            "value": 8391811,
            "unit": "bytes"
          },
          {
            "name": "Arborist.Benchmarks.DepthFirstWhere.TriangleTree_PruneAfter_1448",
            "value": 77177,
            "unit": "bytes"
          },
          {
            "name": "Arborist.Benchmarks.DepthFirstWhere.WhereAll_TrivialForest_1M",
            "value": 1469,
            "unit": "bytes"
          },
          {
            "name": "Arborist.Benchmarks.DepthFirstWhere.WhereNone_TrivialForest_1M",
            "value": 1420,
            "unit": "bytes"
          },
          {
            "name": "Arborist.Benchmarks.DepthFirstWhere.WhereAll_DegenerateTree_1M",
            "value": 37755096,
            "unit": "bytes"
          },
          {
            "name": "Arborist.Benchmarks.DepthFirstWhere.WhereNone_DegenerateTree_1M",
            "value": 1679,
            "unit": "bytes"
          },
          {
            "name": "Arborist.Benchmarks.Select.SelectComposition",
            "value": 871,
            "unit": "bytes"
          },
          {
            "name": "Arborist.Benchmarks.PruneAfter.Bft_TrivialForest_1M",
            "value": 596,
            "unit": "bytes"
          },
          {
            "name": "Arborist.Benchmarks.PruneBefore.Bft_TrivialForest_1M",
            "value": 1142,
            "unit": "bytes"
          },
          {
            "name": "Arborist.Benchmarks.PruneAfter.Dft_TrivialForest_1M",
            "value": 596,
            "unit": "bytes"
          },
          {
            "name": "Arborist.Benchmarks.PruneBefore.Dft_TrivialForest_1M",
            "value": 1484,
            "unit": "bytes"
          },
          {
            "name": "Arborist.Benchmarks.AllNodes.Bft_CompleteBinaryTree_PruneBefore_19",
            "value": 29367861,
            "unit": "bytes"
          },
          {
            "name": "Arborist.Benchmarks.AnyNodes.Bft_CompleteBinaryTree_PruneBefore_19",
            "value": 29369008,
            "unit": "bytes"
          },
          {
            "name": "Arborist.Benchmarks.CountNodes.DeepTree",
            "value": 12588347,
            "unit": "bytes"
          },
          {
            "name": "Arborist.Benchmarks.GetLeaves.DeepTree",
            "value": 1050656,
            "unit": "bytes"
          },
          {
            "name": "Arborist.Benchmarks.AllNodes.Dft_CompleteBinaryTree_PruneBefore_19",
            "value": 3282,
            "unit": "bytes"
          },
          {
            "name": "Arborist.Benchmarks.AnyNodes.Dft_CompleteBinaryTree_PruneBefore_19",
            "value": 3194,
            "unit": "bytes"
          },
          {
            "name": "Arborist.Benchmarks.CountNodes.TriangleTree_PruneAfter_2048",
            "value": 133929,
            "unit": "bytes"
          },
          {
            "name": "Arborist.Benchmarks.GetLeaves.CompleteBinaryTree_PruneBefore_20",
            "value": 3315,
            "unit": "bytes"
          },
          {
            "name": "Arborist.Benchmarks.AllNodes.Bft_TriangleTree_PruneBefore_19",
            "value": 512304,
            "unit": "bytes"
          },
          {
            "name": "Arborist.Benchmarks.AnyNodes.Bft_TriangleTree_PruneBefore_19",
            "value": 512216,
            "unit": "bytes"
          },
          {
            "name": "Arborist.Benchmarks.CountNodes.CompleteBinaryTree_PruneAfter_20",
            "value": 2713,
            "unit": "bytes"
          },
          {
            "name": "Arborist.Benchmarks.AllNodes.Dft_TriangleTree_PruneBefore_19",
            "value": 84937,
            "unit": "bytes"
          },
          {
            "name": "Arborist.Benchmarks.AnyNodes.Dft_TriangleTree_PruneBefore_19",
            "value": 84836,
            "unit": "bytes"
          },
          {
            "name": "Arborist.Benchmarks.SkipAllNodes.Bft_TriangleTree_1448",
            "value": 68167,
            "unit": "bytes"
          },
          {
            "name": "Arborist.Benchmarks.SkipAllNodes.Dft_TriangleTree_1448",
            "value": 26183,
            "unit": "bytes"
          }
        ]
      },
      {
        "commit": {
          "author": {
            "email": "jason.boyd.ce@gmail.com",
            "name": "Jason Boyd",
            "username": "jasonmcboyd"
          },
          "committer": {
            "email": "jason.boyd.ce@gmail.com",
            "name": "Jason Boyd",
            "username": "jasonmcboyd"
          },
          "distinct": true,
          "id": "c3ab5ef05fc7c7304bb98cd14e97bed3a8f91e62",
          "message": "Align LeaffixAggregate with LeaffixScan (flat, lazy, zero per-node alloc)\n\nLeaffixAggregate now uses the same flat forward DFS + no-copy ChildAccumulations view as LeaffixScan, yielding each root's accumulated value. Accumulator changes TAccumulate[] -> ChildAccumulations<TAccumulate>. Per-root lazy: a root is emitted the moment its subtree completes and the buffers are reused for the next root, so peak memory is the largest root subtree (not the whole forest) and early-terminating consumers traverse fewer roots -- matching the previous LeaffixAggregator behavior. Zero per-node heap allocation. Adds LeaffixAggregateTests (it previously had none and no callers).\n\nLeaffixAggregator is retained -- still used by Invert (TreeInverter); it'll be removed when Invert is redesigned onto a flat structure.\n\nCo-Authored-By: Claude Opus 4.8 (1M context) <noreply@anthropic.com>\nClaude-Session: https://claude.ai/code/session_01Wg3xArL4FATQaXQMBvhXdg",
          "timestamp": "2026-06-23T21:57:19Z",
          "tree_id": "7dacc032f0faefb81f1e004514e835a60b5bf81a",
          "url": "https://github.com/jasonmcboyd/Arborist/commit/c3ab5ef05fc7c7304bb98cd14e97bed3a8f91e62"
        },
        "date": 1782253174657,
        "tool": "customSmallerIsBetter",
        "benches": [
          {
            "name": "Arborist.Benchmarks.BreadthFirstWhere.TriangleTree_1448",
            "value": 512531,
            "unit": "bytes"
          },
          {
            "name": "Arborist.Benchmarks.BreadthFirstWhere.TrivialForest_WhereAll_1M",
            "value": 1570,
            "unit": "bytes"
          },
          {
            "name": "Arborist.Benchmarks.BreadthFirstWhere.TrivialForest_WhereNone_1M",
            "value": 1206,
            "unit": "bytes"
          },
          {
            "name": "Arborist.Benchmarks.BreadthFirstWhere.DegenerateTree_WhereAll_1M",
            "value": 8392290,
            "unit": "bytes"
          },
          {
            "name": "Arborist.Benchmarks.BreadthFirstWhere.DegenerateTree_WhereNone_1M",
            "value": 8391811,
            "unit": "bytes"
          },
          {
            "name": "Arborist.Benchmarks.DepthFirstWhere.TriangleTree_PruneAfter_1448",
            "value": 77177,
            "unit": "bytes"
          },
          {
            "name": "Arborist.Benchmarks.DepthFirstWhere.WhereAll_TrivialForest_1M",
            "value": 1469,
            "unit": "bytes"
          },
          {
            "name": "Arborist.Benchmarks.DepthFirstWhere.WhereNone_TrivialForest_1M",
            "value": 1420,
            "unit": "bytes"
          },
          {
            "name": "Arborist.Benchmarks.DepthFirstWhere.WhereAll_DegenerateTree_1M",
            "value": 37755096,
            "unit": "bytes"
          },
          {
            "name": "Arborist.Benchmarks.DepthFirstWhere.WhereNone_DegenerateTree_1M",
            "value": 1679,
            "unit": "bytes"
          },
          {
            "name": "Arborist.Benchmarks.Select.SelectComposition",
            "value": 860,
            "unit": "bytes"
          },
          {
            "name": "Arborist.Benchmarks.PruneAfter.Bft_TrivialForest_1M",
            "value": 596,
            "unit": "bytes"
          },
          {
            "name": "Arborist.Benchmarks.PruneBefore.Bft_TrivialForest_1M",
            "value": 1142,
            "unit": "bytes"
          },
          {
            "name": "Arborist.Benchmarks.PruneAfter.Dft_TrivialForest_1M",
            "value": 596,
            "unit": "bytes"
          },
          {
            "name": "Arborist.Benchmarks.PruneBefore.Dft_TrivialForest_1M",
            "value": 1484,
            "unit": "bytes"
          },
          {
            "name": "Arborist.Benchmarks.AllNodes.Bft_CompleteBinaryTree_PruneBefore_19",
            "value": 29367861,
            "unit": "bytes"
          },
          {
            "name": "Arborist.Benchmarks.AnyNodes.Bft_CompleteBinaryTree_PruneBefore_19",
            "value": 29369008,
            "unit": "bytes"
          },
          {
            "name": "Arborist.Benchmarks.CountNodes.DeepTree",
            "value": 12587524,
            "unit": "bytes"
          },
          {
            "name": "Arborist.Benchmarks.GetLeaves.DeepTree",
            "value": 1050656,
            "unit": "bytes"
          },
          {
            "name": "Arborist.Benchmarks.AllNodes.Dft_CompleteBinaryTree_PruneBefore_19",
            "value": 3282,
            "unit": "bytes"
          },
          {
            "name": "Arborist.Benchmarks.AnyNodes.Dft_CompleteBinaryTree_PruneBefore_19",
            "value": 3194,
            "unit": "bytes"
          },
          {
            "name": "Arborist.Benchmarks.CountNodes.TriangleTree_PruneAfter_2048",
            "value": 133929,
            "unit": "bytes"
          },
          {
            "name": "Arborist.Benchmarks.GetLeaves.CompleteBinaryTree_PruneBefore_20",
            "value": 3315,
            "unit": "bytes"
          },
          {
            "name": "Arborist.Benchmarks.AllNodes.Bft_TriangleTree_PruneBefore_19",
            "value": 512304,
            "unit": "bytes"
          },
          {
            "name": "Arborist.Benchmarks.AnyNodes.Bft_TriangleTree_PruneBefore_19",
            "value": 512216,
            "unit": "bytes"
          },
          {
            "name": "Arborist.Benchmarks.CountNodes.CompleteBinaryTree_PruneAfter_20",
            "value": 2713,
            "unit": "bytes"
          },
          {
            "name": "Arborist.Benchmarks.AllNodes.Dft_TriangleTree_PruneBefore_19",
            "value": 84937,
            "unit": "bytes"
          },
          {
            "name": "Arborist.Benchmarks.AnyNodes.Dft_TriangleTree_PruneBefore_19",
            "value": 84836,
            "unit": "bytes"
          },
          {
            "name": "Arborist.Benchmarks.SkipAllNodes.Bft_TriangleTree_1448",
            "value": 68167,
            "unit": "bytes"
          },
          {
            "name": "Arborist.Benchmarks.SkipAllNodes.Dft_TriangleTree_1448",
            "value": 26183,
            "unit": "bytes"
          }
        ]
      },
      {
        "commit": {
          "author": {
            "email": "jason.boyd.ce@gmail.com",
            "name": "Jason Boyd",
            "username": "jasonmcboyd"
          },
          "committer": {
            "email": "jason.boyd.ce@gmail.com",
            "name": "Jason Boyd",
            "username": "jasonmcboyd"
          },
          "distinct": true,
          "id": "96f3cc44e2208f47258c2887c2a011530d708864",
          "message": "Rewrite Invert flat; retire SimpleNode and LeaffixAggregator\n\nInvert now materializes the source into flat pre-order arrays and emits the mirror with a stack -- pushing roots/children in forward order so they pop in reverse (the mirror's pre-order). Subtree sizes are invariant under mirroring; result is a PreorderTree. O(N), zero per-node allocation (was a SimpleNode object per node via LeaffixAggregator).\n\nInvert was the last user of both SimpleNode and LeaffixAggregator, so this deletes five files: TreeInverter, LeaffixAggregator (+NodeContextAndResult), SimpleNode, SimpleNodeChildEnumerator, SimpleNodeTreenumerable. The SimpleNode retirement is complete, clearing the runway for the nodeToValueMap elimination. (A level-order/LOUDS mirror -- reverse each child-run in place -- remains a future refinement if a LevelOrderTree is ever built.)\n\nCo-Authored-By: Claude Opus 4.8 (1M context) <noreply@anthropic.com>\nClaude-Session: https://claude.ai/code/session_01Wg3xArL4FATQaXQMBvhXdg",
          "timestamp": "2026-06-23T22:21:16Z",
          "tree_id": "aafd024b9cc4740d5e6c4fe63bc7cf95158cbb5e",
          "url": "https://github.com/jasonmcboyd/Arborist/commit/96f3cc44e2208f47258c2887c2a011530d708864"
        },
        "date": 1782254668977,
        "tool": "customSmallerIsBetter",
        "benches": [
          {
            "name": "Arborist.Benchmarks.BreadthFirstWhere.TriangleTree_1448",
            "value": 512531,
            "unit": "bytes"
          },
          {
            "name": "Arborist.Benchmarks.BreadthFirstWhere.TrivialForest_WhereAll_1M",
            "value": 1563,
            "unit": "bytes"
          },
          {
            "name": "Arborist.Benchmarks.BreadthFirstWhere.TrivialForest_WhereNone_1M",
            "value": 1206,
            "unit": "bytes"
          },
          {
            "name": "Arborist.Benchmarks.BreadthFirstWhere.DegenerateTree_WhereAll_1M",
            "value": 8392287,
            "unit": "bytes"
          },
          {
            "name": "Arborist.Benchmarks.BreadthFirstWhere.DegenerateTree_WhereNone_1M",
            "value": 8391811,
            "unit": "bytes"
          },
          {
            "name": "Arborist.Benchmarks.DepthFirstWhere.TriangleTree_PruneAfter_1448",
            "value": 77177,
            "unit": "bytes"
          },
          {
            "name": "Arborist.Benchmarks.DepthFirstWhere.WhereAll_TrivialForest_1M",
            "value": 1465,
            "unit": "bytes"
          },
          {
            "name": "Arborist.Benchmarks.DepthFirstWhere.WhereNone_TrivialForest_1M",
            "value": 1420,
            "unit": "bytes"
          },
          {
            "name": "Arborist.Benchmarks.DepthFirstWhere.WhereAll_DegenerateTree_1M",
            "value": 37755096,
            "unit": "bytes"
          },
          {
            "name": "Arborist.Benchmarks.DepthFirstWhere.WhereNone_DegenerateTree_1M",
            "value": 1679,
            "unit": "bytes"
          },
          {
            "name": "Arborist.Benchmarks.LeaffixAggregate.TriangleTree_1448",
            "value": 33689877,
            "unit": "bytes"
          },
          {
            "name": "Arborist.Benchmarks.LeaffixScan.TriangleTree_1448",
            "value": 42095211,
            "unit": "bytes"
          },
          {
            "name": "Arborist.Benchmarks.LeaffixAggregate.DegenerateTree_1M",
            "value": 58724745,
            "unit": "bytes"
          },
          {
            "name": "Arborist.Benchmarks.LeaffixScan.DegenerateTree_1M",
            "value": 66724915,
            "unit": "bytes"
          },
          {
            "name": "Arborist.Benchmarks.LeaffixAggregate.TrivialForest_1M",
            "value": 687,
            "unit": "bytes"
          },
          {
            "name": "Arborist.Benchmarks.Materialize.TriangleTree_1448",
            "value": 42046105,
            "unit": "bytes"
          },
          {
            "name": "Arborist.Benchmarks.Materialize.DegenerateTree_1M",
            "value": 41560941,
            "unit": "bytes"
          },
          {
            "name": "Arborist.Benchmarks.Select.SelectComposition",
            "value": 871,
            "unit": "bytes"
          },
          {
            "name": "Arborist.Benchmarks.PruneAfter.Bft_TrivialForest_1M",
            "value": 596,
            "unit": "bytes"
          },
          {
            "name": "Arborist.Benchmarks.PruneBefore.Bft_TrivialForest_1M",
            "value": 1142,
            "unit": "bytes"
          },
          {
            "name": "Arborist.Benchmarks.PruneAfter.Dft_TrivialForest_1M",
            "value": 596,
            "unit": "bytes"
          },
          {
            "name": "Arborist.Benchmarks.PruneBefore.Dft_TrivialForest_1M",
            "value": 1484,
            "unit": "bytes"
          },
          {
            "name": "Arborist.Benchmarks.AllNodes.Bft_CompleteBinaryTree_PruneBefore_19",
            "value": 29367861,
            "unit": "bytes"
          },
          {
            "name": "Arborist.Benchmarks.AnyNodes.Bft_CompleteBinaryTree_PruneBefore_19",
            "value": 29367773,
            "unit": "bytes"
          },
          {
            "name": "Arborist.Benchmarks.CountNodes.DeepTree",
            "value": 12588540,
            "unit": "bytes"
          },
          {
            "name": "Arborist.Benchmarks.GetLeaves.DeepTree",
            "value": 1050656,
            "unit": "bytes"
          },
          {
            "name": "Arborist.Benchmarks.AllNodes.Dft_CompleteBinaryTree_PruneBefore_19",
            "value": 3274,
            "unit": "bytes"
          },
          {
            "name": "Arborist.Benchmarks.AnyNodes.Dft_CompleteBinaryTree_PruneBefore_19",
            "value": 3186,
            "unit": "bytes"
          },
          {
            "name": "Arborist.Benchmarks.CountNodes.TriangleTree_PruneAfter_2048",
            "value": 133916,
            "unit": "bytes"
          },
          {
            "name": "Arborist.Benchmarks.GetLeaves.CompleteBinaryTree_PruneBefore_20",
            "value": 3315,
            "unit": "bytes"
          },
          {
            "name": "Arborist.Benchmarks.AllNodes.Bft_TriangleTree_PruneBefore_19",
            "value": 512267,
            "unit": "bytes"
          },
          {
            "name": "Arborist.Benchmarks.AnyNodes.Bft_TriangleTree_PruneBefore_19",
            "value": 512179,
            "unit": "bytes"
          },
          {
            "name": "Arborist.Benchmarks.CountNodes.CompleteBinaryTree_PruneAfter_20",
            "value": 2713,
            "unit": "bytes"
          },
          {
            "name": "Arborist.Benchmarks.AllNodes.Dft_TriangleTree_PruneBefore_19",
            "value": 84914,
            "unit": "bytes"
          },
          {
            "name": "Arborist.Benchmarks.AnyNodes.Dft_TriangleTree_PruneBefore_19",
            "value": 84826,
            "unit": "bytes"
          },
          {
            "name": "Arborist.Benchmarks.SkipAllNodes.Bft_TriangleTree_1448",
            "value": 68167,
            "unit": "bytes"
          },
          {
            "name": "Arborist.Benchmarks.SkipAllNodes.Dft_TriangleTree_1448",
            "value": 26183,
            "unit": "bytes"
          }
        ]
      },
      {
        "commit": {
          "author": {
            "email": "jason.boyd.ce@gmail.com",
            "name": "Jason Boyd",
            "username": "jasonmcboyd"
          },
          "committer": {
            "email": "jason.boyd.ce@gmail.com",
            "name": "Jason Boyd",
            "username": "jasonmcboyd"
          },
          "distinct": true,
          "id": "7c795cd25f5b93fbac1993cf6a71485ef8c2de74",
          "message": "Add MemoryDiagnoser benchmark for Invert\n\nCovers TriangleTree depth-1448 (~1M) and a 1M-deep DegenerateTree, tagged LINQ for the dashboard. Completes benchmark coverage of the now-flat transform/aggregation ops (Materialize, LeaffixScan, LeaffixAggregate, Invert).\n\nCo-Authored-By: Claude Opus 4.8 (1M context) <noreply@anthropic.com>\nClaude-Session: https://claude.ai/code/session_01Wg3xArL4FATQaXQMBvhXdg",
          "timestamp": "2026-06-23T22:22:47Z",
          "tree_id": "547984a2584ebd974bf3f4a1790b68fc9b328cfc",
          "url": "https://github.com/jasonmcboyd/Arborist/commit/7c795cd25f5b93fbac1993cf6a71485ef8c2de74"
        },
        "date": 1782256079521,
        "tool": "customSmallerIsBetter",
        "benches": [
          {
            "name": "Arborist.Benchmarks.BreadthFirstWhere.TriangleTree_1448",
            "value": 512531,
            "unit": "bytes"
          },
          {
            "name": "Arborist.Benchmarks.BreadthFirstWhere.TrivialForest_WhereAll_1M",
            "value": 1578,
            "unit": "bytes"
          },
          {
            "name": "Arborist.Benchmarks.BreadthFirstWhere.TrivialForest_WhereNone_1M",
            "value": 1206,
            "unit": "bytes"
          },
          {
            "name": "Arborist.Benchmarks.BreadthFirstWhere.DegenerateTree_WhereAll_1M",
            "value": 8392290,
            "unit": "bytes"
          },
          {
            "name": "Arborist.Benchmarks.BreadthFirstWhere.DegenerateTree_WhereNone_1M",
            "value": 8391811,
            "unit": "bytes"
          },
          {
            "name": "Arborist.Benchmarks.DepthFirstWhere.TriangleTree_PruneAfter_1448",
            "value": 77195,
            "unit": "bytes"
          },
          {
            "name": "Arborist.Benchmarks.DepthFirstWhere.WhereAll_TrivialForest_1M",
            "value": 1475,
            "unit": "bytes"
          },
          {
            "name": "Arborist.Benchmarks.DepthFirstWhere.WhereNone_TrivialForest_1M",
            "value": 1420,
            "unit": "bytes"
          },
          {
            "name": "Arborist.Benchmarks.DepthFirstWhere.WhereAll_DegenerateTree_1M",
            "value": 37755459,
            "unit": "bytes"
          },
          {
            "name": "Arborist.Benchmarks.DepthFirstWhere.WhereNone_DegenerateTree_1M",
            "value": 1679,
            "unit": "bytes"
          },
          {
            "name": "Arborist.Benchmarks.Invert.TriangleTree_1448",
            "value": 42062747,
            "unit": "bytes"
          },
          {
            "name": "Arborist.Benchmarks.Invert.DegenerateTree_1M",
            "value": 41559538,
            "unit": "bytes"
          },
          {
            "name": "Arborist.Benchmarks.LeaffixAggregate.TriangleTree_1448",
            "value": 33690030,
            "unit": "bytes"
          },
          {
            "name": "Arborist.Benchmarks.LeaffixScan.TriangleTree_1448",
            "value": 42095213,
            "unit": "bytes"
          },
          {
            "name": "Arborist.Benchmarks.LeaffixAggregate.DegenerateTree_1M",
            "value": 58724537,
            "unit": "bytes"
          },
          {
            "name": "Arborist.Benchmarks.LeaffixScan.DegenerateTree_1M",
            "value": 66724729,
            "unit": "bytes"
          },
          {
            "name": "Arborist.Benchmarks.LeaffixAggregate.TrivialForest_1M",
            "value": 687,
            "unit": "bytes"
          },
          {
            "name": "Arborist.Benchmarks.Materialize.TriangleTree_1448",
            "value": 42046107,
            "unit": "bytes"
          },
          {
            "name": "Arborist.Benchmarks.Materialize.DegenerateTree_1M",
            "value": 41559545,
            "unit": "bytes"
          },
          {
            "name": "Arborist.Benchmarks.Select.SelectComposition",
            "value": 871,
            "unit": "bytes"
          },
          {
            "name": "Arborist.Benchmarks.PruneAfter.Bft_TrivialForest_1M",
            "value": 596,
            "unit": "bytes"
          },
          {
            "name": "Arborist.Benchmarks.PruneBefore.Bft_TrivialForest_1M",
            "value": 1142,
            "unit": "bytes"
          },
          {
            "name": "Arborist.Benchmarks.PruneAfter.Dft_TrivialForest_1M",
            "value": 596,
            "unit": "bytes"
          },
          {
            "name": "Arborist.Benchmarks.PruneBefore.Dft_TrivialForest_1M",
            "value": 1484,
            "unit": "bytes"
          },
          {
            "name": "Arborist.Benchmarks.AllNodes.Bft_CompleteBinaryTree_PruneBefore_19",
            "value": 29370555,
            "unit": "bytes"
          },
          {
            "name": "Arborist.Benchmarks.AnyNodes.Bft_CompleteBinaryTree_PruneBefore_19",
            "value": 29370467,
            "unit": "bytes"
          },
          {
            "name": "Arborist.Benchmarks.CountNodes.DeepTree",
            "value": 12589649,
            "unit": "bytes"
          },
          {
            "name": "Arborist.Benchmarks.GetLeaves.DeepTree",
            "value": 1050656,
            "unit": "bytes"
          },
          {
            "name": "Arborist.Benchmarks.AllNodes.Dft_CompleteBinaryTree_PruneBefore_19",
            "value": 3282,
            "unit": "bytes"
          },
          {
            "name": "Arborist.Benchmarks.AnyNodes.Dft_CompleteBinaryTree_PruneBefore_19",
            "value": 3194,
            "unit": "bytes"
          },
          {
            "name": "Arborist.Benchmarks.CountNodes.TriangleTree_PruneAfter_2048",
            "value": 133929,
            "unit": "bytes"
          },
          {
            "name": "Arborist.Benchmarks.GetLeaves.CompleteBinaryTree_PruneBefore_20",
            "value": 3315,
            "unit": "bytes"
          },
          {
            "name": "Arborist.Benchmarks.AllNodes.Bft_TriangleTree_PruneBefore_19",
            "value": 512304,
            "unit": "bytes"
          },
          {
            "name": "Arborist.Benchmarks.AnyNodes.Bft_TriangleTree_PruneBefore_19",
            "value": 512216,
            "unit": "bytes"
          },
          {
            "name": "Arborist.Benchmarks.CountNodes.CompleteBinaryTree_PruneAfter_20",
            "value": 2731,
            "unit": "bytes"
          },
          {
            "name": "Arborist.Benchmarks.AllNodes.Dft_TriangleTree_PruneBefore_19",
            "value": 84937,
            "unit": "bytes"
          },
          {
            "name": "Arborist.Benchmarks.AnyNodes.Dft_TriangleTree_PruneBefore_19",
            "value": 84836,
            "unit": "bytes"
          },
          {
            "name": "Arborist.Benchmarks.SkipAllNodes.Bft_TriangleTree_1448",
            "value": 68190,
            "unit": "bytes"
          },
          {
            "name": "Arborist.Benchmarks.SkipAllNodes.Dft_TriangleTree_1448",
            "value": 26183,
            "unit": "bytes"
          }
        ]
      },
      {
        "commit": {
          "author": {
            "email": "jason.boyd.ce@gmail.com",
            "name": "Jason Boyd",
            "username": "jasonmcboyd"
          },
          "committer": {
            "email": "jason.boyd.ce@gmail.com",
            "name": "Jason Boyd",
            "username": "jasonmcboyd"
          },
          "distinct": true,
          "id": "cf2c06864e8fdc1e6c24df1f1437f0dae1da26b0",
          "message": "Add 2-param Treenumerable base; drop redundant identity maps (map cleanup stage 1)\n\nThe sample trees whose node IS their surfaced value (TriangleTree, CollatzTree, CompleteBinaryTree, NDecrementTree, DeepTree) were each passing a redundant 'node => node' nodeToValueMap and carrying a duplicate type parameter. Add a 2-param Treenumerable<TNode, TChildEnumerator> convenience base (identity map) and migrate them to it. Public surfaces unchanged. The 3-param base + map remain for PreorderTree's genuine index->value dereference.\n\nCo-Authored-By: Claude Opus 4.8 (1M context) <noreply@anthropic.com>\nClaude-Session: https://claude.ai/code/session_01Wg3xArL4FATQaXQMBvhXdg",
          "timestamp": "2026-06-23T22:35:56Z",
          "tree_id": "005c51f9d7f00116b4e04d1cdb2a5fd0680fc86e",
          "url": "https://github.com/jasonmcboyd/Arborist/commit/cf2c06864e8fdc1e6c24df1f1437f0dae1da26b0"
        },
        "date": 1782257438632,
        "tool": "customSmallerIsBetter",
        "benches": [
          {
            "name": "Arborist.Benchmarks.BreadthFirstWhere.TriangleTree_1448",
            "value": 512531,
            "unit": "bytes"
          },
          {
            "name": "Arborist.Benchmarks.BreadthFirstWhere.TrivialForest_WhereAll_1M",
            "value": 1563,
            "unit": "bytes"
          },
          {
            "name": "Arborist.Benchmarks.BreadthFirstWhere.TrivialForest_WhereNone_1M",
            "value": 1206,
            "unit": "bytes"
          },
          {
            "name": "Arborist.Benchmarks.BreadthFirstWhere.DegenerateTree_WhereAll_1M",
            "value": 8392287,
            "unit": "bytes"
          },
          {
            "name": "Arborist.Benchmarks.BreadthFirstWhere.DegenerateTree_WhereNone_1M",
            "value": 8391811,
            "unit": "bytes"
          },
          {
            "name": "Arborist.Benchmarks.DepthFirstWhere.TriangleTree_PruneAfter_1448",
            "value": 77177,
            "unit": "bytes"
          },
          {
            "name": "Arborist.Benchmarks.DepthFirstWhere.WhereAll_TrivialForest_1M",
            "value": 1465,
            "unit": "bytes"
          },
          {
            "name": "Arborist.Benchmarks.DepthFirstWhere.WhereNone_TrivialForest_1M",
            "value": 1420,
            "unit": "bytes"
          },
          {
            "name": "Arborist.Benchmarks.DepthFirstWhere.WhereAll_DegenerateTree_1M",
            "value": 37755096,
            "unit": "bytes"
          },
          {
            "name": "Arborist.Benchmarks.DepthFirstWhere.WhereNone_DegenerateTree_1M",
            "value": 1679,
            "unit": "bytes"
          },
          {
            "name": "Arborist.Benchmarks.Invert.TriangleTree_1448",
            "value": 42062747,
            "unit": "bytes"
          },
          {
            "name": "Arborist.Benchmarks.Invert.DegenerateTree_1M",
            "value": 41559286,
            "unit": "bytes"
          },
          {
            "name": "Arborist.Benchmarks.LeaffixAggregate.TriangleTree_1448",
            "value": 33689877,
            "unit": "bytes"
          },
          {
            "name": "Arborist.Benchmarks.LeaffixScan.TriangleTree_1448",
            "value": 42095211,
            "unit": "bytes"
          },
          {
            "name": "Arborist.Benchmarks.LeaffixAggregate.DegenerateTree_1M",
            "value": 58724751,
            "unit": "bytes"
          },
          {
            "name": "Arborist.Benchmarks.LeaffixScan.DegenerateTree_1M",
            "value": 66724915,
            "unit": "bytes"
          },
          {
            "name": "Arborist.Benchmarks.LeaffixAggregate.TrivialForest_1M",
            "value": 687,
            "unit": "bytes"
          },
          {
            "name": "Arborist.Benchmarks.Materialize.TriangleTree_1448",
            "value": 42046105,
            "unit": "bytes"
          },
          {
            "name": "Arborist.Benchmarks.Materialize.DegenerateTree_1M",
            "value": 41560922,
            "unit": "bytes"
          },
          {
            "name": "Arborist.Benchmarks.Select.SelectComposition",
            "value": 871,
            "unit": "bytes"
          },
          {
            "name": "Arborist.Benchmarks.PruneAfter.Bft_TrivialForest_1M",
            "value": 596,
            "unit": "bytes"
          },
          {
            "name": "Arborist.Benchmarks.PruneBefore.Bft_TrivialForest_1M",
            "value": 1142,
            "unit": "bytes"
          },
          {
            "name": "Arborist.Benchmarks.PruneAfter.Dft_TrivialForest_1M",
            "value": 596,
            "unit": "bytes"
          },
          {
            "name": "Arborist.Benchmarks.PruneBefore.Dft_TrivialForest_1M",
            "value": 1484,
            "unit": "bytes"
          },
          {
            "name": "Arborist.Benchmarks.AllNodes.Bft_CompleteBinaryTree_PruneBefore_19",
            "value": 29367861,
            "unit": "bytes"
          },
          {
            "name": "Arborist.Benchmarks.AnyNodes.Bft_CompleteBinaryTree_PruneBefore_19",
            "value": 29367773,
            "unit": "bytes"
          },
          {
            "name": "Arborist.Benchmarks.CountNodes.DeepTree",
            "value": 12588089,
            "unit": "bytes"
          },
          {
            "name": "Arborist.Benchmarks.GetLeaves.DeepTree",
            "value": 1050656,
            "unit": "bytes"
          },
          {
            "name": "Arborist.Benchmarks.AllNodes.Dft_CompleteBinaryTree_PruneBefore_19",
            "value": 3274,
            "unit": "bytes"
          },
          {
            "name": "Arborist.Benchmarks.AnyNodes.Dft_CompleteBinaryTree_PruneBefore_19",
            "value": 3186,
            "unit": "bytes"
          },
          {
            "name": "Arborist.Benchmarks.CountNodes.TriangleTree_PruneAfter_2048",
            "value": 133916,
            "unit": "bytes"
          },
          {
            "name": "Arborist.Benchmarks.GetLeaves.CompleteBinaryTree_PruneBefore_20",
            "value": 3315,
            "unit": "bytes"
          },
          {
            "name": "Arborist.Benchmarks.AllNodes.Bft_TriangleTree_PruneBefore_19",
            "value": 512267,
            "unit": "bytes"
          },
          {
            "name": "Arborist.Benchmarks.AnyNodes.Bft_TriangleTree_PruneBefore_19",
            "value": 512179,
            "unit": "bytes"
          },
          {
            "name": "Arborist.Benchmarks.CountNodes.CompleteBinaryTree_PruneAfter_20",
            "value": 2713,
            "unit": "bytes"
          },
          {
            "name": "Arborist.Benchmarks.AllNodes.Dft_TriangleTree_PruneBefore_19",
            "value": 84914,
            "unit": "bytes"
          },
          {
            "name": "Arborist.Benchmarks.AnyNodes.Dft_TriangleTree_PruneBefore_19",
            "value": 84826,
            "unit": "bytes"
          },
          {
            "name": "Arborist.Benchmarks.SkipAllNodes.Bft_TriangleTree_1448",
            "value": 68167,
            "unit": "bytes"
          },
          {
            "name": "Arborist.Benchmarks.SkipAllNodes.Dft_TriangleTree_1448",
            "value": 26183,
            "unit": "bytes"
          }
        ]
      },
      {
        "commit": {
          "author": {
            "email": "jason.boyd.ce@gmail.com",
            "name": "Jason Boyd",
            "username": "jasonmcboyd"
          },
          "committer": {
            "email": "jason.boyd.ce@gmail.com",
            "name": "Jason Boyd",
            "username": "jasonmcboyd"
          },
          "distinct": true,
          "id": "ca567e0f439d6d1f0f5c2143add771a6c13fd41c",
          "message": "Fix BFT Where O(depth) memory regression: tail-carry skip prefix\n\nf7eae61's O(N)-time prefix carry stored _PredSkipPrefix as a List<int>\nindexed by absolute inner depth, grown to the current depth on every\nscheduled node -- so a 1M-deep degenerate Where(_=>true) chain allocated\n~8.39 MB even though nothing is predicate-skipped (every entry 0).\n\nReplace it with a tail-carry: the prefix is monotonic non-decreasing in\ndepth and constant (= total skips on the path) beyond the deepest skipped\nancestor, so store only up to the deepest skip and serve reads past the\nend from a scalar tail. New _PrefixStored + _PrefixStoredCount +\n_PrefixTail; PrefixRead/PrefixWriteScheduled (no-op when value == tail, so\nzero allocation in the accepted region); PrefixAnchor truncates the stored\ncount to frontDepth-1 on front-advance.\n\nWhereAll 8.39 MB -> ~1.9 KB (O(1) in depth); WhereNone byte-identical\n(inherent O(depth) preserved). Visit stream byte-identical: validated\nagainst Where2InProcessScan (full c..i, 891k cases) + WhereTests (218/0),\nnet48 + net8.0 clean. Add WhereBreadthFirstAllocationTests as a hard\nmemory-bound regression guard (the gh-pages benchmark only soft-alerts).\n\nCo-Authored-By: Claude Opus 4.8 (1M context) <noreply@anthropic.com>\nClaude-Session: https://claude.ai/code/session_01Wg3xArL4FATQaXQMBvhXdg",
          "timestamp": "2026-06-25T18:13:44Z",
          "tree_id": "6d5c2256d2c8574537a4127f73a8de04fe0b4011",
          "url": "https://github.com/jasonmcboyd/Arborist/commit/ca567e0f439d6d1f0f5c2143add771a6c13fd41c"
        },
        "date": 1782413421273,
        "tool": "customSmallerIsBetter",
        "benches": [
          {
            "name": "Arborist.Benchmarks.BreadthFirstWhere.TriangleTree_1448",
            "value": 512539,
            "unit": "bytes"
          },
          {
            "name": "Arborist.Benchmarks.BreadthFirstWhere.TrivialForest_WhereAll_1M",
            "value": 1538,
            "unit": "bytes"
          },
          {
            "name": "Arborist.Benchmarks.BreadthFirstWhere.TrivialForest_WhereNone_1M",
            "value": 1214,
            "unit": "bytes"
          },
          {
            "name": "Arborist.Benchmarks.BreadthFirstWhere.DegenerateTree_WhereAll_1M",
            "value": 2035,
            "unit": "bytes"
          },
          {
            "name": "Arborist.Benchmarks.BreadthFirstWhere.DegenerateTree_WhereNone_1M",
            "value": 8391819,
            "unit": "bytes"
          },
          {
            "name": "Arborist.Benchmarks.DepthFirstWhere.TriangleTree_PruneAfter_1448",
            "value": 78939,
            "unit": "bytes"
          },
          {
            "name": "Arborist.Benchmarks.DepthFirstWhere.WhereAll_TrivialForest_1M",
            "value": 1469,
            "unit": "bytes"
          },
          {
            "name": "Arborist.Benchmarks.DepthFirstWhere.WhereNone_TrivialForest_1M",
            "value": 1420,
            "unit": "bytes"
          },
          {
            "name": "Arborist.Benchmarks.DepthFirstWhere.WhereAll_DegenerateTree_1M",
            "value": 37755096,
            "unit": "bytes"
          },
          {
            "name": "Arborist.Benchmarks.DepthFirstWhere.WhereNone_DegenerateTree_1M",
            "value": 1679,
            "unit": "bytes"
          },
          {
            "name": "Arborist.Benchmarks.Invert.TriangleTree_1448",
            "value": 42062747,
            "unit": "bytes"
          },
          {
            "name": "Arborist.Benchmarks.Invert.DegenerateTree_1M",
            "value": 41559286,
            "unit": "bytes"
          },
          {
            "name": "Arborist.Benchmarks.LeaffixAggregate.TriangleTree_1448",
            "value": 33689877,
            "unit": "bytes"
          },
          {
            "name": "Arborist.Benchmarks.LeaffixScan.TriangleTree_1448",
            "value": 42095211,
            "unit": "bytes"
          },
          {
            "name": "Arborist.Benchmarks.LeaffixAggregate.DegenerateTree_1M",
            "value": 58724343,
            "unit": "bytes"
          },
          {
            "name": "Arborist.Benchmarks.LeaffixScan.DegenerateTree_1M",
            "value": 66724812,
            "unit": "bytes"
          },
          {
            "name": "Arborist.Benchmarks.LeaffixAggregate.TrivialForest_1M",
            "value": 687,
            "unit": "bytes"
          },
          {
            "name": "Arborist.Benchmarks.Materialize.TriangleTree_1448",
            "value": 42046107,
            "unit": "bytes"
          },
          {
            "name": "Arborist.Benchmarks.Materialize.DegenerateTree_1M",
            "value": 41560921,
            "unit": "bytes"
          },
          {
            "name": "Arborist.Benchmarks.Select.SelectComposition",
            "value": 871,
            "unit": "bytes"
          },
          {
            "name": "Arborist.Benchmarks.PruneAfter.Bft_TrivialForest_1M",
            "value": 596,
            "unit": "bytes"
          },
          {
            "name": "Arborist.Benchmarks.PruneBefore.Bft_TrivialForest_1M",
            "value": 1150,
            "unit": "bytes"
          },
          {
            "name": "Arborist.Benchmarks.PruneAfter.Dft_TrivialForest_1M",
            "value": 596,
            "unit": "bytes"
          },
          {
            "name": "Arborist.Benchmarks.PruneBefore.Dft_TrivialForest_1M",
            "value": 1484,
            "unit": "bytes"
          },
          {
            "name": "Arborist.Benchmarks.AllNodes.Bft_CompleteBinaryTree_PruneBefore_19",
            "value": 29367869,
            "unit": "bytes"
          },
          {
            "name": "Arborist.Benchmarks.AnyNodes.Bft_CompleteBinaryTree_PruneBefore_19",
            "value": 29369072,
            "unit": "bytes"
          },
          {
            "name": "Arborist.Benchmarks.CountNodes.DeepTree",
            "value": 12588540,
            "unit": "bytes"
          },
          {
            "name": "Arborist.Benchmarks.GetLeaves.DeepTree",
            "value": 1050656,
            "unit": "bytes"
          },
          {
            "name": "Arborist.Benchmarks.AllNodes.Dft_CompleteBinaryTree_PruneBefore_19",
            "value": 3282,
            "unit": "bytes"
          },
          {
            "name": "Arborist.Benchmarks.AnyNodes.Dft_CompleteBinaryTree_PruneBefore_19",
            "value": 3194,
            "unit": "bytes"
          },
          {
            "name": "Arborist.Benchmarks.CountNodes.TriangleTree_PruneAfter_2048",
            "value": 133929,
            "unit": "bytes"
          },
          {
            "name": "Arborist.Benchmarks.GetLeaves.CompleteBinaryTree_PruneBefore_20",
            "value": 3315,
            "unit": "bytes"
          },
          {
            "name": "Arborist.Benchmarks.AllNodes.Bft_TriangleTree_PruneBefore_19",
            "value": 512312,
            "unit": "bytes"
          },
          {
            "name": "Arborist.Benchmarks.AnyNodes.Bft_TriangleTree_PruneBefore_19",
            "value": 512187,
            "unit": "bytes"
          },
          {
            "name": "Arborist.Benchmarks.CountNodes.CompleteBinaryTree_PruneAfter_20",
            "value": 2713,
            "unit": "bytes"
          },
          {
            "name": "Arborist.Benchmarks.AllNodes.Dft_TriangleTree_PruneBefore_19",
            "value": 84937,
            "unit": "bytes"
          },
          {
            "name": "Arborist.Benchmarks.AnyNodes.Dft_TriangleTree_PruneBefore_19",
            "value": 84836,
            "unit": "bytes"
          },
          {
            "name": "Arborist.Benchmarks.SkipAllNodes.Bft_TriangleTree_1448",
            "value": 68190,
            "unit": "bytes"
          },
          {
            "name": "Arborist.Benchmarks.SkipAllNodes.Dft_TriangleTree_1448",
            "value": 26183,
            "unit": "bytes"
          }
        ]
      },
      {
        "commit": {
          "author": {
            "email": "jason.boyd.ce@gmail.com",
            "name": "Jason Boyd",
            "username": "jasonmcboyd"
          },
          "committer": {
            "email": "jason.boyd.ce@gmail.com",
            "name": "Jason Boyd",
            "username": "jasonmcboyd"
          },
          "distinct": true,
          "id": "6a5d01f5e6181c1c89a0993a6360bab3a4b0bac7",
          "message": "Rewrite BreadthFirstTreenumerator with a structural visit cadence\n\nThe base BFT engine emitted parent visits reactively (when a child was\naccepted), which broke whenever a child was filtered and forced a tangle\nof deferred parent-visit bookkeeping to recover the swallowed visits.\n\nReplace it with a structural cadence: a single FIFO _VisitQueue whose\nfront is the active parent, plus a LIFO _ScheduleStack for the SkipNode\npromotion descent. A parent is visited once when it reaches the front,\nthen once after every child slot that enqueues at least one accepted\nnode -- a single bool, _CurrentSlotEnqueuedNode. Roots are scheduled\nfirst as the children of an implicit no-visit forest sentinel.\n\nThis deletes _OwesPromotedParentVisit, _HasDeferredScheduledChild,\n_DepthOfLastActedOnNode, PayOwedParentVisitAndDeferChild, and the\nOnScheduling/OnVisiting/PromoteChildren/SkipSubtree/Backtrack web in\nfavor of Advance/ApplyStrategy/SkipRemainingSiblings. The now-unused\nOwesPromotedParentVisit field comes off the shared InternalNodeVisitState\nstruct, shrinking every deque slot.\n\nNo behavior change: same (Mode, Node, VisitCount, Position) visit stream.\n\nValidation:\n- Exhaustive BFT-vs-DFT oracle, up to 6 concurrent skips x 27 trees\n- Where2InProcessScan: 891,056 Where-wrapper-vs-oracle cases (groups c..i)\n- Curated exact-order traversal + 14,759 Where/allocation tests\n- Benchmarks: allocations -12% to -14%, time within ShortRun noise\n\nCo-Authored-By: Claude Opus 4.8 (1M context) <noreply@anthropic.com>\nClaude-Session: https://claude.ai/code/session_01Wg3xArL4FATQaXQMBvhXdg",
          "timestamp": "2026-06-26T00:47:42Z",
          "tree_id": "39f01ca28e49bf3bbc704caca59e8d4bb815b334",
          "url": "https://github.com/jasonmcboyd/Arborist/commit/6a5d01f5e6181c1c89a0993a6360bab3a4b0bac7"
        },
        "date": 1782436872394,
        "tool": "customSmallerIsBetter",
        "benches": [
          {
            "name": "Arborist.Benchmarks.BreadthFirstWhere.TriangleTree_1448",
            "value": 479755,
            "unit": "bytes"
          },
          {
            "name": "Arborist.Benchmarks.BreadthFirstWhere.TrivialForest_WhereAll_1M",
            "value": 1538,
            "unit": "bytes"
          },
          {
            "name": "Arborist.Benchmarks.BreadthFirstWhere.TrivialForest_WhereNone_1M",
            "value": 1214,
            "unit": "bytes"
          },
          {
            "name": "Arborist.Benchmarks.BreadthFirstWhere.DegenerateTree_WhereAll_1M",
            "value": 2035,
            "unit": "bytes"
          },
          {
            "name": "Arborist.Benchmarks.BreadthFirstWhere.DegenerateTree_WhereNone_1M",
            "value": 8391819,
            "unit": "bytes"
          },
          {
            "name": "Arborist.Benchmarks.DepthFirstWhere.TriangleTree_PruneAfter_1448",
            "value": 73081,
            "unit": "bytes"
          },
          {
            "name": "Arborist.Benchmarks.DepthFirstWhere.WhereAll_TrivialForest_1M",
            "value": 1469,
            "unit": "bytes"
          },
          {
            "name": "Arborist.Benchmarks.DepthFirstWhere.WhereNone_TrivialForest_1M",
            "value": 1420,
            "unit": "bytes"
          },
          {
            "name": "Arborist.Benchmarks.DepthFirstWhere.WhereAll_DegenerateTree_1M",
            "value": 37755096,
            "unit": "bytes"
          },
          {
            "name": "Arborist.Benchmarks.DepthFirstWhere.WhereNone_DegenerateTree_1M",
            "value": 1679,
            "unit": "bytes"
          },
          {
            "name": "Arborist.Benchmarks.Invert.TriangleTree_1448",
            "value": 42054555,
            "unit": "bytes"
          },
          {
            "name": "Arborist.Benchmarks.Invert.DegenerateTree_1M",
            "value": 41559286,
            "unit": "bytes"
          },
          {
            "name": "Arborist.Benchmarks.LeaffixAggregate.TriangleTree_1448",
            "value": 33681685,
            "unit": "bytes"
          },
          {
            "name": "Arborist.Benchmarks.LeaffixScan.TriangleTree_1448",
            "value": 42087019,
            "unit": "bytes"
          },
          {
            "name": "Arborist.Benchmarks.LeaffixAggregate.DegenerateTree_1M",
            "value": 58724471,
            "unit": "bytes"
          },
          {
            "name": "Arborist.Benchmarks.LeaffixScan.DegenerateTree_1M",
            "value": 66724768,
            "unit": "bytes"
          },
          {
            "name": "Arborist.Benchmarks.LeaffixAggregate.TrivialForest_1M",
            "value": 687,
            "unit": "bytes"
          },
          {
            "name": "Arborist.Benchmarks.Materialize.TriangleTree_1448",
            "value": 42037915,
            "unit": "bytes"
          },
          {
            "name": "Arborist.Benchmarks.Materialize.DegenerateTree_1M",
            "value": 41560921,
            "unit": "bytes"
          },
          {
            "name": "Arborist.Benchmarks.Select.SelectComposition",
            "value": 871,
            "unit": "bytes"
          },
          {
            "name": "Arborist.Benchmarks.PruneAfter.Bft_TrivialForest_1M",
            "value": 596,
            "unit": "bytes"
          },
          {
            "name": "Arborist.Benchmarks.PruneBefore.Bft_TrivialForest_1M",
            "value": 1150,
            "unit": "bytes"
          },
          {
            "name": "Arborist.Benchmarks.PruneAfter.Dft_TrivialForest_1M",
            "value": 596,
            "unit": "bytes"
          },
          {
            "name": "Arborist.Benchmarks.PruneBefore.Dft_TrivialForest_1M",
            "value": 1484,
            "unit": "bytes"
          },
          {
            "name": "Arborist.Benchmarks.AllNodes.Bft_CompleteBinaryTree_PruneBefore_19",
            "value": 27270677,
            "unit": "bytes"
          },
          {
            "name": "Arborist.Benchmarks.AnyNodes.Bft_CompleteBinaryTree_PruneBefore_19",
            "value": 27271892,
            "unit": "bytes"
          },
          {
            "name": "Arborist.Benchmarks.CountNodes.DeepTree",
            "value": 10491602,
            "unit": "bytes"
          },
          {
            "name": "Arborist.Benchmarks.GetLeaves.DeepTree",
            "value": 1050624,
            "unit": "bytes"
          },
          {
            "name": "Arborist.Benchmarks.AllNodes.Dft_CompleteBinaryTree_PruneBefore_19",
            "value": 3250,
            "unit": "bytes"
          },
          {
            "name": "Arborist.Benchmarks.AnyNodes.Dft_CompleteBinaryTree_PruneBefore_19",
            "value": 3162,
            "unit": "bytes"
          },
          {
            "name": "Arborist.Benchmarks.CountNodes.TriangleTree_PruneAfter_2048",
            "value": 117482,
            "unit": "bytes"
          },
          {
            "name": "Arborist.Benchmarks.GetLeaves.CompleteBinaryTree_PruneBefore_20",
            "value": 3283,
            "unit": "bytes"
          },
          {
            "name": "Arborist.Benchmarks.AllNodes.Bft_TriangleTree_PruneBefore_19",
            "value": 479504,
            "unit": "bytes"
          },
          {
            "name": "Arborist.Benchmarks.AnyNodes.Bft_TriangleTree_PruneBefore_19",
            "value": 479379,
            "unit": "bytes"
          },
          {
            "name": "Arborist.Benchmarks.CountNodes.CompleteBinaryTree_PruneAfter_20",
            "value": 2545,
            "unit": "bytes"
          },
          {
            "name": "Arborist.Benchmarks.AllNodes.Dft_TriangleTree_PruneBefore_19",
            "value": 84905,
            "unit": "bytes"
          },
          {
            "name": "Arborist.Benchmarks.AnyNodes.Dft_TriangleTree_PruneBefore_19",
            "value": 84804,
            "unit": "bytes"
          },
          {
            "name": "Arborist.Benchmarks.SkipAllNodes.Bft_TriangleTree_1448",
            "value": 59935,
            "unit": "bytes"
          },
          {
            "name": "Arborist.Benchmarks.SkipAllNodes.Dft_TriangleTree_1448",
            "value": 26151,
            "unit": "bytes"
          }
        ]
      },
      {
        "commit": {
          "author": {
            "email": "jason.boyd.ce@gmail.com",
            "name": "Jason Boyd",
            "username": "jasonmcboyd"
          },
          "committer": {
            "email": "jason.boyd.ce@gmail.com",
            "name": "Jason Boyd",
            "username": "jasonmcboyd"
          },
          "distinct": true,
          "id": "ce22f8e1055cf2b7bf6486f1da78d2058d8c69aa",
          "message": "Cap RefSemiDeque partition size to bound LOH allocation and overshoot\n\nGeometric partition growth sized each new partition to the running total\nCapacity, so the largest partition reached ~half the deque's peak element\ncount -- a multi-MB Large Object Heap allocation on wide/deep trees, plus\nup to ~2x peak over-allocation at power-of-two boundaries. Cap partition\nsize at 4096 elements (Math.Min(Capacity, MaxPartitionSize)) to bound both.\n\nBFT CompleteBinaryTree_21 (peak frontier ~2^21, the worst-case boundary):\n96 MB -> 48 MB allocated per traversal, throughput unchanged.\n\nFixed element count rather than a byte budget that would force partitions\nsub-LOH: forcing a 64 B node's partitions sub-LOH measured ~40% slower with\n~7x the Gen0 collections, because large long-lived blocks belong on the LOH.\n\nAdd RefSemiDeque regression tests crossing the cap (heterogeneous-partition\nordering, out-of-order recycling, GetFromBack/RemoveLast across boundaries)\nand an Add_Block64_1M benchmark. Remove unused InitialCapacity.\n\nCo-Authored-By: Claude Opus 4.8 (1M context) <noreply@anthropic.com>",
          "timestamp": "2026-06-26T04:13:38Z",
          "tree_id": "e35c9314063b1f6eda6dfccd1d7349907af73e32",
          "url": "https://github.com/jasonmcboyd/Arborist/commit/ce22f8e1055cf2b7bf6486f1da78d2058d8c69aa"
        },
        "date": 1782449422189,
        "tool": "customSmallerIsBetter",
        "benches": [
          {
            "name": "Arborist.Benchmarks.BreadthFirstWhere.TriangleTree_1448",
            "value": 479715,
            "unit": "bytes"
          },
          {
            "name": "Arborist.Benchmarks.BreadthFirstWhere.TrivialForest_WhereAll_1M",
            "value": 1530,
            "unit": "bytes"
          },
          {
            "name": "Arborist.Benchmarks.BreadthFirstWhere.TrivialForest_WhereNone_1M",
            "value": 1206,
            "unit": "bytes"
          },
          {
            "name": "Arborist.Benchmarks.BreadthFirstWhere.DegenerateTree_WhereAll_1M",
            "value": 2019,
            "unit": "bytes"
          },
          {
            "name": "Arborist.Benchmarks.BreadthFirstWhere.DegenerateTree_WhereNone_1M",
            "value": 8391803,
            "unit": "bytes"
          },
          {
            "name": "Arborist.Benchmarks.DepthFirstWhere.TriangleTree_PruneAfter_1448",
            "value": 73049,
            "unit": "bytes"
          },
          {
            "name": "Arborist.Benchmarks.DepthFirstWhere.WhereAll_TrivialForest_1M",
            "value": 1449,
            "unit": "bytes"
          },
          {
            "name": "Arborist.Benchmarks.DepthFirstWhere.WhereNone_TrivialForest_1M",
            "value": 1404,
            "unit": "bytes"
          },
          {
            "name": "Arborist.Benchmarks.DepthFirstWhere.WhereAll_DegenerateTree_1M",
            "value": 36173528,
            "unit": "bytes"
          },
          {
            "name": "Arborist.Benchmarks.DepthFirstWhere.WhereNone_DegenerateTree_1M",
            "value": 1655,
            "unit": "bytes"
          },
          {
            "name": "Arborist.Benchmarks.Invert.TriangleTree_1448",
            "value": 42054539,
            "unit": "bytes"
          },
          {
            "name": "Arborist.Benchmarks.Invert.DegenerateTree_1M",
            "value": 41215185,
            "unit": "bytes"
          },
          {
            "name": "Arborist.Benchmarks.LeaffixAggregate.TriangleTree_1448",
            "value": 33681822,
            "unit": "bytes"
          },
          {
            "name": "Arborist.Benchmarks.LeaffixScan.TriangleTree_1448",
            "value": 42087003,
            "unit": "bytes"
          },
          {
            "name": "Arborist.Benchmarks.LeaffixAggregate.DegenerateTree_1M",
            "value": 58381899,
            "unit": "bytes"
          },
          {
            "name": "Arborist.Benchmarks.LeaffixScan.DegenerateTree_1M",
            "value": 66381805,
            "unit": "bytes"
          },
          {
            "name": "Arborist.Benchmarks.LeaffixAggregate.TrivialForest_1M",
            "value": 687,
            "unit": "bytes"
          },
          {
            "name": "Arborist.Benchmarks.Materialize.TriangleTree_1448",
            "value": 42037899,
            "unit": "bytes"
          },
          {
            "name": "Arborist.Benchmarks.Materialize.DegenerateTree_1M",
            "value": 41215359,
            "unit": "bytes"
          },
          {
            "name": "Arborist.Benchmarks.Select.SelectComposition",
            "value": 871,
            "unit": "bytes"
          },
          {
            "name": "Arborist.Benchmarks.PruneAfter.Bft_TrivialForest_1M",
            "value": 596,
            "unit": "bytes"
          },
          {
            "name": "Arborist.Benchmarks.PruneBefore.Bft_TrivialForest_1M",
            "value": 1142,
            "unit": "bytes"
          },
          {
            "name": "Arborist.Benchmarks.PruneAfter.Dft_TrivialForest_1M",
            "value": 596,
            "unit": "bytes"
          },
          {
            "name": "Arborist.Benchmarks.PruneBefore.Dft_TrivialForest_1M",
            "value": 1468,
            "unit": "bytes"
          },
          {
            "name": "Arborist.Benchmarks.AllNodes.Bft_CompleteBinaryTree_PruneBefore_19",
            "value": 13869992,
            "unit": "bytes"
          },
          {
            "name": "Arborist.Benchmarks.AnyNodes.Bft_CompleteBinaryTree_PruneBefore_19",
            "value": 13869904,
            "unit": "bytes"
          },
          {
            "name": "Arborist.Benchmarks.CountNodes.DeepTree",
            "value": 10506423,
            "unit": "bytes"
          },
          {
            "name": "Arborist.Benchmarks.GetLeaves.DeepTree",
            "value": 1054524,
            "unit": "bytes"
          },
          {
            "name": "Arborist.Benchmarks.AllNodes.Dft_CompleteBinaryTree_PruneBefore_19",
            "value": 3218,
            "unit": "bytes"
          },
          {
            "name": "Arborist.Benchmarks.AnyNodes.Dft_CompleteBinaryTree_PruneBefore_19",
            "value": 3130,
            "unit": "bytes"
          },
          {
            "name": "Arborist.Benchmarks.CountNodes.TriangleTree_PruneAfter_2048",
            "value": 117450,
            "unit": "bytes"
          },
          {
            "name": "Arborist.Benchmarks.GetLeaves.CompleteBinaryTree_PruneBefore_20",
            "value": 3251,
            "unit": "bytes"
          },
          {
            "name": "Arborist.Benchmarks.AllNodes.Bft_TriangleTree_PruneBefore_19",
            "value": 479464,
            "unit": "bytes"
          },
          {
            "name": "Arborist.Benchmarks.AnyNodes.Bft_TriangleTree_PruneBefore_19",
            "value": 479339,
            "unit": "bytes"
          },
          {
            "name": "Arborist.Benchmarks.CountNodes.CompleteBinaryTree_PruneAfter_20",
            "value": 2513,
            "unit": "bytes"
          },
          {
            "name": "Arborist.Benchmarks.AllNodes.Dft_TriangleTree_PruneBefore_19",
            "value": 84873,
            "unit": "bytes"
          },
          {
            "name": "Arborist.Benchmarks.AnyNodes.Dft_TriangleTree_PruneBefore_19",
            "value": 84772,
            "unit": "bytes"
          },
          {
            "name": "Arborist.Benchmarks.SkipAllNodes.Bft_TriangleTree_1448",
            "value": 59903,
            "unit": "bytes"
          },
          {
            "name": "Arborist.Benchmarks.SkipAllNodes.Dft_TriangleTree_1448",
            "value": 26135,
            "unit": "bytes"
          }
        ]
      },
      {
        "commit": {
          "author": {
            "email": "jason.boyd.ce@gmail.com",
            "name": "Jason Boyd",
            "username": "jasonmcboyd"
          },
          "committer": {
            "email": "jason.boyd.ce@gmail.com",
            "name": "Jason Boyd",
            "username": "jasonmcboyd"
          },
          "distinct": true,
          "id": "81b46c42702e71ad313d08c636eb3e3c3c35b140",
          "message": "Cohere BreadthFirstTreenumerator's four deques into one frame stack\n\nThe BFT kept each node's visit state and its child enumerator in FOUR parallel\nRefSemiDeques -- a state deque and an enumerator deque for each of the visit\nqueue and the schedule stack -- relying on keeping each pair in lockstep.\n\nFold each pair into one RefSemiDeque<Frame>, where Frame bundles\n{Node, Position, VisitCount, ChildEnumerator}, driven only by ref so the\nenumerator is never copied. Accepting a node becomes a single whole-frame move\nfrom the schedule stack to the visit queue, which structurally prevents a node\nand its enumerator from desynchronizing. The algorithm and visit cadence are\nunchanged. Mirrors the depth-first engine's frame stack, and removes the\nnow-unreferenced shared InternalNodeVisitState struct.\n\nNo behavior change: same (Mode, Node, VisitCount, Position) visit stream;\ndisposal (including the deliberate idempotent double-dispose on\nSkipDescendants/SkipSiblings) is at exact parity with the original.\n\nValidation:\n- Exact-order traversal + exhaustive DFT-vs-BFT multiset scan (438/0)\n- Full Arborist.Linq suite incl. Where (14,759/0)\n- Benchmarks (Release/Job.Default, clean tree): TriangleTree 289->255ms (-12%),\n  CompleteBinaryTree 337->281ms (-16%), TrivialForest/DegenerateTree 4M within\n  noise; allocation neutral, still zero per-node heap allocation. Timing\n  variance also dropped (StdDev roughly halved on the dense trees).\n\nCo-Authored-By: Claude Opus 4.8 (1M context) <noreply@anthropic.com>\nClaude-Session: https://claude.ai/code/session_01Wg3xArL4FATQaXQMBvhXdg",
          "timestamp": "2026-06-26T21:37:21Z",
          "tree_id": "05aa22f3461078c05e96518d52da4d56655c250f",
          "url": "https://github.com/jasonmcboyd/Arborist/commit/81b46c42702e71ad313d08c636eb3e3c3c35b140"
        },
        "date": 1782511428420,
        "tool": "customSmallerIsBetter",
        "benches": [
          {
            "name": "Arborist.Benchmarks.BreadthFirstWhere.TriangleTree_1448",
            "value": 478635,
            "unit": "bytes"
          },
          {
            "name": "Arborist.Benchmarks.BreadthFirstWhere.TrivialForest_WhereAll_1M",
            "value": 1530,
            "unit": "bytes"
          },
          {
            "name": "Arborist.Benchmarks.BreadthFirstWhere.TrivialForest_WhereNone_1M",
            "value": 1206,
            "unit": "bytes"
          },
          {
            "name": "Arborist.Benchmarks.BreadthFirstWhere.DegenerateTree_WhereAll_1M",
            "value": 2043,
            "unit": "bytes"
          },
          {
            "name": "Arborist.Benchmarks.BreadthFirstWhere.DegenerateTree_WhereNone_1M",
            "value": 8391803,
            "unit": "bytes"
          },
          {
            "name": "Arborist.Benchmarks.DepthFirstWhere.TriangleTree_PruneAfter_1448",
            "value": 96945,
            "unit": "bytes"
          },
          {
            "name": "Arborist.Benchmarks.DepthFirstWhere.WhereAll_TrivialForest_1M",
            "value": 1449,
            "unit": "bytes"
          },
          {
            "name": "Arborist.Benchmarks.DepthFirstWhere.WhereNone_TrivialForest_1M",
            "value": 1404,
            "unit": "bytes"
          },
          {
            "name": "Arborist.Benchmarks.DepthFirstWhere.WhereAll_DegenerateTree_1M",
            "value": 36173535,
            "unit": "bytes"
          },
          {
            "name": "Arborist.Benchmarks.DepthFirstWhere.WhereNone_DegenerateTree_1M",
            "value": 1655,
            "unit": "bytes"
          },
          {
            "name": "Arborist.Benchmarks.Invert.TriangleTree_1448",
            "value": 42061979,
            "unit": "bytes"
          },
          {
            "name": "Arborist.Benchmarks.Invert.DegenerateTree_1M",
            "value": 41215313,
            "unit": "bytes"
          },
          {
            "name": "Arborist.Benchmarks.LeaffixAggregate.TriangleTree_1448",
            "value": 33689062,
            "unit": "bytes"
          },
          {
            "name": "Arborist.Benchmarks.LeaffixScan.TriangleTree_1448",
            "value": 42094443,
            "unit": "bytes"
          },
          {
            "name": "Arborist.Benchmarks.LeaffixAggregate.DegenerateTree_1M",
            "value": 58384506,
            "unit": "bytes"
          },
          {
            "name": "Arborist.Benchmarks.LeaffixScan.DegenerateTree_1M",
            "value": 66385510,
            "unit": "bytes"
          },
          {
            "name": "Arborist.Benchmarks.LeaffixAggregate.TrivialForest_1M",
            "value": 687,
            "unit": "bytes"
          },
          {
            "name": "Arborist.Benchmarks.Materialize.TriangleTree_1448",
            "value": 42045337,
            "unit": "bytes"
          },
          {
            "name": "Arborist.Benchmarks.Materialize.DegenerateTree_1M",
            "value": 41215186,
            "unit": "bytes"
          },
          {
            "name": "Arborist.Benchmarks.Select.SelectComposition",
            "value": 871,
            "unit": "bytes"
          },
          {
            "name": "Arborist.Benchmarks.PruneAfter.Bft_TrivialForest_1M",
            "value": 596,
            "unit": "bytes"
          },
          {
            "name": "Arborist.Benchmarks.PruneBefore.Bft_TrivialForest_1M",
            "value": 1142,
            "unit": "bytes"
          },
          {
            "name": "Arborist.Benchmarks.PruneAfter.Dft_TrivialForest_1M",
            "value": 596,
            "unit": "bytes"
          },
          {
            "name": "Arborist.Benchmarks.PruneBefore.Dft_TrivialForest_1M",
            "value": 1468,
            "unit": "bytes"
          },
          {
            "name": "Arborist.Benchmarks.AllNodes.Bft_CompleteBinaryTree_PruneBefore_19",
            "value": 13861680,
            "unit": "bytes"
          },
          {
            "name": "Arborist.Benchmarks.AnyNodes.Bft_CompleteBinaryTree_PruneBefore_19",
            "value": 13860905,
            "unit": "bytes"
          },
          {
            "name": "Arborist.Benchmarks.CountNodes.DeepTree",
            "value": 10496295,
            "unit": "bytes"
          },
          {
            "name": "Arborist.Benchmarks.GetLeaves.DeepTree",
            "value": 6302068,
            "unit": "bytes"
          },
          {
            "name": "Arborist.Benchmarks.AllNodes.Dft_CompleteBinaryTree_PruneBefore_19",
            "value": 3554,
            "unit": "bytes"
          },
          {
            "name": "Arborist.Benchmarks.AnyNodes.Dft_CompleteBinaryTree_PruneBefore_19",
            "value": 3458,
            "unit": "bytes"
          },
          {
            "name": "Arborist.Benchmarks.CountNodes.TriangleTree_PruneAfter_2048",
            "value": 116451,
            "unit": "bytes"
          },
          {
            "name": "Arborist.Benchmarks.GetLeaves.CompleteBinaryTree_PruneBefore_20",
            "value": 3587,
            "unit": "bytes"
          },
          {
            "name": "Arborist.Benchmarks.AllNodes.Bft_TriangleTree_PruneBefore_19",
            "value": 478371,
            "unit": "bytes"
          },
          {
            "name": "Arborist.Benchmarks.AnyNodes.Bft_TriangleTree_PruneBefore_19",
            "value": 478283,
            "unit": "bytes"
          },
          {
            "name": "Arborist.Benchmarks.CountNodes.CompleteBinaryTree_PruneAfter_20",
            "value": 2020,
            "unit": "bytes"
          },
          {
            "name": "Arborist.Benchmarks.AllNodes.Dft_TriangleTree_PruneBefore_19",
            "value": 125506,
            "unit": "bytes"
          },
          {
            "name": "Arborist.Benchmarks.AnyNodes.Dft_TriangleTree_PruneBefore_19",
            "value": 125418,
            "unit": "bytes"
          },
          {
            "name": "Arborist.Benchmarks.SkipAllNodes.Bft_TriangleTree_1448",
            "value": 58991,
            "unit": "bytes"
          },
          {
            "name": "Arborist.Benchmarks.SkipAllNodes.Dft_TriangleTree_1448",
            "value": 66791,
            "unit": "bytes"
          }
        ]
      },
      {
        "commit": {
          "author": {
            "email": "jason.boyd.ce@gmail.com",
            "name": "Jason Boyd",
            "username": "jasonmcboyd"
          },
          "committer": {
            "email": "jason.boyd.ce@gmail.com",
            "name": "Jason Boyd",
            "username": "jasonmcboyd"
          },
          "distinct": true,
          "id": "ab94b3983058299be15f1b4d38b05b21509cb8ad",
          "message": "Encapsulate BFT state in BreadthFirstPath; mirror the DFT driver/state split\n\nMove the breadth-first engine's state -- the visit queue, the schedule stack,\nthe owed-return-visit carry, and the root sibling counter -- into a new\nBreadthFirstPath, leaving BreadthFirstTreenumerator a thin driver, mirroring the\ndepth-first DepthFirstPath split. The cohesive Frame (visit state + child\nenumerator) is kept -- the BFT keeps full state for every resident node anyway,\nso it costs no memory -- so allocation is unchanged from the cohesion engine.\n\nLike DepthFirstPath, BreadthFirstPath is \"sans-I/O\": it never pulls a child; it\nexposes the two active enumerators (the schedule-stack top and the visit-queue\nfront) by ref for the driver to advance. That isolates the engine's asynchronous\noperations to those seams, so a future async BFT can share this class and differ\nonly there. It is a mutable struct embedded as the driver's single _Path field\n(never copied; refs it returns point into the heap deques), keeping dense\ntraversal at the cohesion engine's speed. The two child-pull sites collapse into\none TryScheduleNextChildOf(ref parent).\n\nNo behavior change: same (Mode, Node, VisitCount, Position) visit stream.\n\nValidation:\n- Exact-order traversal + exhaustive DFT-vs-BFT multiset scan (438/0)\n- Full Arborist.Linq suite incl. Where (14,759/0)\n- Benchmarks (Release/Job.Default, same session): TriangleTree 194.5 vs cohesion\n  193.9 ms; CompleteBinaryTree 208.1 vs 213.0 ms (parity). Allocation identical\n  to the cohesion engine (same Frame).\n\nCo-Authored-By: Claude Opus 4.8 (1M context) <noreply@anthropic.com>\nClaude-Session: https://claude.ai/code/session_01Wg3xArL4FATQaXQMBvhXdg",
          "timestamp": "2026-06-27T00:09:54Z",
          "tree_id": "8703f7198030df75b83a7026b75dc74f78b17190",
          "url": "https://github.com/jasonmcboyd/Arborist/commit/ab94b3983058299be15f1b4d38b05b21509cb8ad"
        },
        "date": 1782521636503,
        "tool": "customSmallerIsBetter",
        "benches": [
          {
            "name": "Arborist.Benchmarks.BreadthFirstWhere.TriangleTree_1448",
            "value": 478625,
            "unit": "bytes"
          },
          {
            "name": "Arborist.Benchmarks.BreadthFirstWhere.TrivialForest_WhereAll_1M",
            "value": 1530,
            "unit": "bytes"
          },
          {
            "name": "Arborist.Benchmarks.BreadthFirstWhere.TrivialForest_WhereNone_1M",
            "value": 1206,
            "unit": "bytes"
          },
          {
            "name": "Arborist.Benchmarks.BreadthFirstWhere.DegenerateTree_WhereAll_1M",
            "value": 2019,
            "unit": "bytes"
          },
          {
            "name": "Arborist.Benchmarks.BreadthFirstWhere.DegenerateTree_WhereNone_1M",
            "value": 8391803,
            "unit": "bytes"
          },
          {
            "name": "Arborist.Benchmarks.DepthFirstWhere.TriangleTree_PruneAfter_1448",
            "value": 73049,
            "unit": "bytes"
          },
          {
            "name": "Arborist.Benchmarks.DepthFirstWhere.WhereAll_TrivialForest_1M",
            "value": 1453,
            "unit": "bytes"
          },
          {
            "name": "Arborist.Benchmarks.DepthFirstWhere.WhereNone_TrivialForest_1M",
            "value": 1404,
            "unit": "bytes"
          },
          {
            "name": "Arborist.Benchmarks.DepthFirstWhere.WhereAll_DegenerateTree_1M",
            "value": 36173545,
            "unit": "bytes"
          },
          {
            "name": "Arborist.Benchmarks.DepthFirstWhere.WhereNone_DegenerateTree_1M",
            "value": 1655,
            "unit": "bytes"
          },
          {
            "name": "Arborist.Benchmarks.Invert.TriangleTree_1448",
            "value": 42054539,
            "unit": "bytes"
          },
          {
            "name": "Arborist.Benchmarks.Invert.DegenerateTree_1M",
            "value": 41215258,
            "unit": "bytes"
          },
          {
            "name": "Arborist.Benchmarks.LeaffixAggregate.TriangleTree_1448",
            "value": 33681669,
            "unit": "bytes"
          },
          {
            "name": "Arborist.Benchmarks.LeaffixScan.TriangleTree_1448",
            "value": 42087003,
            "unit": "bytes"
          },
          {
            "name": "Arborist.Benchmarks.LeaffixAggregate.DegenerateTree_1M",
            "value": 58384506,
            "unit": "bytes"
          },
          {
            "name": "Arborist.Benchmarks.LeaffixScan.DegenerateTree_1M",
            "value": 66385560,
            "unit": "bytes"
          },
          {
            "name": "Arborist.Benchmarks.LeaffixAggregate.TrivialForest_1M",
            "value": 687,
            "unit": "bytes"
          },
          {
            "name": "Arborist.Benchmarks.Materialize.TriangleTree_1448",
            "value": 42037899,
            "unit": "bytes"
          },
          {
            "name": "Arborist.Benchmarks.Materialize.DegenerateTree_1M",
            "value": 41215338,
            "unit": "bytes"
          },
          {
            "name": "Arborist.Benchmarks.Select.SelectComposition",
            "value": 871,
            "unit": "bytes"
          },
          {
            "name": "Arborist.Benchmarks.PruneAfter.Bft_TrivialForest_1M",
            "value": 596,
            "unit": "bytes"
          },
          {
            "name": "Arborist.Benchmarks.PruneBefore.Bft_TrivialForest_1M",
            "value": 1142,
            "unit": "bytes"
          },
          {
            "name": "Arborist.Benchmarks.PruneAfter.Dft_TrivialForest_1M",
            "value": 596,
            "unit": "bytes"
          },
          {
            "name": "Arborist.Benchmarks.PruneBefore.Dft_TrivialForest_1M",
            "value": 1468,
            "unit": "bytes"
          },
          {
            "name": "Arborist.Benchmarks.AllNodes.Bft_CompleteBinaryTree_PruneBefore_19",
            "value": 13861679,
            "unit": "bytes"
          },
          {
            "name": "Arborist.Benchmarks.AnyNodes.Bft_CompleteBinaryTree_PruneBefore_19",
            "value": 13860912,
            "unit": "bytes"
          },
          {
            "name": "Arborist.Benchmarks.CountNodes.DeepTree",
            "value": 10496303,
            "unit": "bytes"
          },
          {
            "name": "Arborist.Benchmarks.GetLeaves.DeepTree",
            "value": 1054524,
            "unit": "bytes"
          },
          {
            "name": "Arborist.Benchmarks.AllNodes.Dft_CompleteBinaryTree_PruneBefore_19",
            "value": 3210,
            "unit": "bytes"
          },
          {
            "name": "Arborist.Benchmarks.AnyNodes.Dft_CompleteBinaryTree_PruneBefore_19",
            "value": 3122,
            "unit": "bytes"
          },
          {
            "name": "Arborist.Benchmarks.CountNodes.TriangleTree_PruneAfter_2048",
            "value": 116459,
            "unit": "bytes"
          },
          {
            "name": "Arborist.Benchmarks.GetLeaves.CompleteBinaryTree_PruneBefore_20",
            "value": 3251,
            "unit": "bytes"
          },
          {
            "name": "Arborist.Benchmarks.AllNodes.Bft_TriangleTree_PruneBefore_19",
            "value": 478379,
            "unit": "bytes"
          },
          {
            "name": "Arborist.Benchmarks.AnyNodes.Bft_TriangleTree_PruneBefore_19",
            "value": 478291,
            "unit": "bytes"
          },
          {
            "name": "Arborist.Benchmarks.CountNodes.CompleteBinaryTree_PruneAfter_20",
            "value": 2041,
            "unit": "bytes"
          },
          {
            "name": "Arborist.Benchmarks.AllNodes.Dft_TriangleTree_PruneBefore_19",
            "value": 84860,
            "unit": "bytes"
          },
          {
            "name": "Arborist.Benchmarks.AnyNodes.Dft_TriangleTree_PruneBefore_19",
            "value": 84772,
            "unit": "bytes"
          },
          {
            "name": "Arborist.Benchmarks.SkipAllNodes.Bft_TriangleTree_1448",
            "value": 58999,
            "unit": "bytes"
          },
          {
            "name": "Arborist.Benchmarks.SkipAllNodes.Dft_TriangleTree_1448",
            "value": 26194,
            "unit": "bytes"
          }
        ]
      },
      {
        "commit": {
          "author": {
            "email": "jason.boyd.ce@gmail.com",
            "name": "Jason Boyd",
            "username": "jasonmcboyd"
          },
          "committer": {
            "email": "jason.boyd.ce@gmail.com",
            "name": "Jason Boyd",
            "username": "jasonmcboyd"
          },
          "distinct": true,
          "id": "6ebd5d0e1d60592672f966eaa4ab81a302c56999",
          "message": "Fix DFT skip-heavy regression: keep TryPushNextChild out-of-line\n\nThe encapsulated DepthFirstPath DFT (14f8393) ran ~1.7-1.9x slower than the\noriginal two-stack on promotion-heavy skip traversal (SkipAllNodes / Preorder /\nPostorder on wide trees), despite identical allocation. JIT disassembly\n(DOTNET_JitDisasm) pinned the cause: [AggressiveInlining] on TryPushNextChild\ninlined the entire promote body (pull + push) into OnMoveNext/OnScheduling,\ninflating their frames to 6 callee-saved registers + sub rsp,72 + vzeroupper and\ndowngrading OnMoveNext's branch dispatch from a tail-jmp to a call+teardown paid\non EVERY node. The original two-stack stayed fast precisely because its promote\nwas a separate, out-of-line method.\n\nMark TryPushNextChild [NoInlining] so the drivers stay thin tail-dispatchers,\nwhile keeping the push chain (PushChild/PushLevel) force-inlined INTO it so the\npush itself is still call-free. Also fold Backtrack's pop + three predicate\nchecks into one DepthFirstPath.PopFinishedLevelAndClassify call: the original\ninline-predicate form is O(1) per level but its repeated struct round-trips cost\n~2x on the deep-unwind path (GetLeaves.DeepTree); folding restores it.\n\nNet (Release/Job.Default, local, vs original two-stack): SkipAllNodes.Dft\n41 -> 22.6 ms, Preorder 42.8 -> 25.7, Postorder 47 -> 32.9 -- all back at the\noriginal; dense traversal and GetLeaves.DeepTree within noise; allocation\nunchanged. The sans-I/O encapsulation (path never pulls; one ref seam) is intact.\n\nValidation: 438/0 oracle (exact-order + exhaustive DFT-vs-BFT scan), 14,759/0\nfull Linq suite.\n\nCo-Authored-By: Claude Opus 4.8 (1M context) <noreply@anthropic.com>\nClaude-Session: https://claude.ai/code/session_01Wg3xArL4FATQaXQMBvhXdg",
          "timestamp": "2026-06-27T02:26:54Z",
          "tree_id": "5301780378953d2f586361d71d9e7e7b8dc1e6d3",
          "url": "https://github.com/jasonmcboyd/Arborist/commit/6ebd5d0e1d60592672f966eaa4ab81a302c56999"
        },
        "date": 1782529880773,
        "tool": "customSmallerIsBetter",
        "benches": [
          {
            "name": "Arborist.Benchmarks.BreadthFirstWhere.TriangleTree_1448",
            "value": 478643,
            "unit": "bytes"
          },
          {
            "name": "Arborist.Benchmarks.BreadthFirstWhere.TrivialForest_WhereAll_1M",
            "value": 1530,
            "unit": "bytes"
          },
          {
            "name": "Arborist.Benchmarks.BreadthFirstWhere.TrivialForest_WhereNone_1M",
            "value": 1206,
            "unit": "bytes"
          },
          {
            "name": "Arborist.Benchmarks.BreadthFirstWhere.DegenerateTree_WhereAll_1M",
            "value": 2019,
            "unit": "bytes"
          },
          {
            "name": "Arborist.Benchmarks.BreadthFirstWhere.DegenerateTree_WhereNone_1M",
            "value": 8391803,
            "unit": "bytes"
          },
          {
            "name": "Arborist.Benchmarks.DepthFirstWhere.TriangleTree_PruneAfter_1448",
            "value": 73049,
            "unit": "bytes"
          },
          {
            "name": "Arborist.Benchmarks.DepthFirstWhere.WhereAll_TrivialForest_1M",
            "value": 1453,
            "unit": "bytes"
          },
          {
            "name": "Arborist.Benchmarks.DepthFirstWhere.WhereNone_TrivialForest_1M",
            "value": 1404,
            "unit": "bytes"
          },
          {
            "name": "Arborist.Benchmarks.DepthFirstWhere.WhereAll_DegenerateTree_1M",
            "value": 36173545,
            "unit": "bytes"
          },
          {
            "name": "Arborist.Benchmarks.DepthFirstWhere.WhereNone_DegenerateTree_1M",
            "value": 1655,
            "unit": "bytes"
          },
          {
            "name": "Arborist.Benchmarks.Invert.TriangleTree_1448",
            "value": 42054539,
            "unit": "bytes"
          },
          {
            "name": "Arborist.Benchmarks.Invert.DegenerateTree_1M",
            "value": 41215313,
            "unit": "bytes"
          },
          {
            "name": "Arborist.Benchmarks.LeaffixAggregate.TriangleTree_1448",
            "value": 33681669,
            "unit": "bytes"
          },
          {
            "name": "Arborist.Benchmarks.LeaffixScan.TriangleTree_1448",
            "value": 42087003,
            "unit": "bytes"
          },
          {
            "name": "Arborist.Benchmarks.LeaffixAggregate.DegenerateTree_1M",
            "value": 58381949,
            "unit": "bytes"
          },
          {
            "name": "Arborist.Benchmarks.LeaffixScan.DegenerateTree_1M",
            "value": 66381249,
            "unit": "bytes"
          },
          {
            "name": "Arborist.Benchmarks.LeaffixAggregate.TrivialForest_1M",
            "value": 687,
            "unit": "bytes"
          },
          {
            "name": "Arborist.Benchmarks.Materialize.TriangleTree_1448",
            "value": 42037899,
            "unit": "bytes"
          },
          {
            "name": "Arborist.Benchmarks.Materialize.DegenerateTree_1M",
            "value": 41215398,
            "unit": "bytes"
          },
          {
            "name": "Arborist.Benchmarks.Select.SelectComposition",
            "value": 871,
            "unit": "bytes"
          },
          {
            "name": "Arborist.Benchmarks.PruneAfter.Bft_TrivialForest_1M",
            "value": 596,
            "unit": "bytes"
          },
          {
            "name": "Arborist.Benchmarks.PruneBefore.Bft_TrivialForest_1M",
            "value": 1142,
            "unit": "bytes"
          },
          {
            "name": "Arborist.Benchmarks.PruneAfter.Dft_TrivialForest_1M",
            "value": 596,
            "unit": "bytes"
          },
          {
            "name": "Arborist.Benchmarks.PruneBefore.Dft_TrivialForest_1M",
            "value": 1468,
            "unit": "bytes"
          },
          {
            "name": "Arborist.Benchmarks.AllNodes.Bft_CompleteBinaryTree_PruneBefore_19",
            "value": 13861687,
            "unit": "bytes"
          },
          {
            "name": "Arborist.Benchmarks.AnyNodes.Bft_CompleteBinaryTree_PruneBefore_19",
            "value": 13861626,
            "unit": "bytes"
          },
          {
            "name": "Arborist.Benchmarks.CountNodes.DeepTree",
            "value": 10496303,
            "unit": "bytes"
          },
          {
            "name": "Arborist.Benchmarks.GetLeaves.DeepTree",
            "value": 1054535,
            "unit": "bytes"
          },
          {
            "name": "Arborist.Benchmarks.AllNodes.Dft_CompleteBinaryTree_PruneBefore_19",
            "value": 3218,
            "unit": "bytes"
          },
          {
            "name": "Arborist.Benchmarks.AnyNodes.Dft_CompleteBinaryTree_PruneBefore_19",
            "value": 3130,
            "unit": "bytes"
          },
          {
            "name": "Arborist.Benchmarks.CountNodes.TriangleTree_PruneAfter_2048",
            "value": 116459,
            "unit": "bytes"
          },
          {
            "name": "Arborist.Benchmarks.GetLeaves.CompleteBinaryTree_PruneBefore_20",
            "value": 3251,
            "unit": "bytes"
          },
          {
            "name": "Arborist.Benchmarks.AllNodes.Bft_TriangleTree_PruneBefore_19",
            "value": 478379,
            "unit": "bytes"
          },
          {
            "name": "Arborist.Benchmarks.AnyNodes.Bft_TriangleTree_PruneBefore_19",
            "value": 478291,
            "unit": "bytes"
          },
          {
            "name": "Arborist.Benchmarks.CountNodes.CompleteBinaryTree_PruneAfter_20",
            "value": 2041,
            "unit": "bytes"
          },
          {
            "name": "Arborist.Benchmarks.AllNodes.Dft_TriangleTree_PruneBefore_19",
            "value": 84860,
            "unit": "bytes"
          },
          {
            "name": "Arborist.Benchmarks.AnyNodes.Dft_TriangleTree_PruneBefore_19",
            "value": 84772,
            "unit": "bytes"
          },
          {
            "name": "Arborist.Benchmarks.SkipAllNodes.Bft_TriangleTree_1448",
            "value": 58999,
            "unit": "bytes"
          },
          {
            "name": "Arborist.Benchmarks.SkipAllNodes.Dft_TriangleTree_1448",
            "value": 26158,
            "unit": "bytes"
          }
        ]
      }
    ],
    "Conversion Memory": [
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
        "date": 1769975446430,
        "tool": "customSmallerIsBetter",
        "benches": [
          {
            "name": "Arborist.Benchmarks.EnumerableToTree.ToDegenerateTree",
            "value": 556,
            "unit": "bytes"
          },
          {
            "name": "Arborist.Benchmarks.EnumerableToTree.ToDegenerateTreeUsingToTree",
            "value": 41951674,
            "unit": "bytes"
          },
          {
            "name": "Arborist.Benchmarks.EnumerableToTree.ToTrivialForest",
            "value": 275,
            "unit": "bytes"
          },
          {
            "name": "Arborist.Benchmarks.EnumerableToTree.ToTrivialForestUsingToTree",
            "value": 1853,
            "unit": "bytes"
          }
        ]
      },
      {
        "commit": {
          "author": {
            "email": "jason.boyd.ce@gmail.com",
            "name": "Jason Boyd",
            "username": "jasonmcboyd"
          },
          "committer": {
            "email": "jason.boyd.ce@gmail.com",
            "name": "Jason Boyd",
            "username": "jasonmcboyd"
          },
          "distinct": true,
          "id": "5993e5762d3c0ffa1bbacbb40b35307cff9a3f87",
          "message": "Docs: document BFT Where O(N) prefix-carry design\n\nRewrite the WhereBreadthFirstTreenumerator section to match the implementation (queue + incremental _PredSkipPrefix, predicate-skip vs consumer-SkipNode, the real parent-visit fields, front-anchored sibling index). Update the DFT-vs-BFT table (BFT now O(N)) and fix stale _ExtraParentVisitsEmitted/_LastRemovedSkippedNodePosition/_SkippedStack references.\n\nCo-Authored-By: Claude Opus 4.8 (1M context) <noreply@anthropic.com>\nClaude-Session: https://claude.ai/code/session_01Wg3xArL4FATQaXQMBvhXdg",
          "timestamp": "2026-06-23T04:33:54Z",
          "tree_id": "a2615b3ce8e91df5baffd5f8f08e3c6ac9df2e3b",
          "url": "https://github.com/jasonmcboyd/Arborist/commit/5993e5762d3c0ffa1bbacbb40b35307cff9a3f87"
        },
        "date": 1782190986052,
        "tool": "customSmallerIsBetter",
        "benches": [
          {
            "name": "Arborist.Benchmarks.EnumerableToTree.ToDegenerateTree",
            "value": 564,
            "unit": "bytes"
          },
          {
            "name": "Arborist.Benchmarks.EnumerableToTree.ToDegenerateTreeUsingToTree",
            "value": 46145913,
            "unit": "bytes"
          },
          {
            "name": "Arborist.Benchmarks.EnumerableToTree.ToTrivialForest",
            "value": 275,
            "unit": "bytes"
          },
          {
            "name": "Arborist.Benchmarks.EnumerableToTree.ToTrivialForestUsingToTree",
            "value": 1929,
            "unit": "bytes"
          }
        ]
      },
      {
        "commit": {
          "author": {
            "email": "jason.boyd.ce@gmail.com",
            "name": "Jason Boyd",
            "username": "jasonmcboyd"
          },
          "committer": {
            "email": "jason.boyd.ce@gmail.com",
            "name": "Jason Boyd",
            "username": "jasonmcboyd"
          },
          "distinct": true,
          "id": "c3ab5ef05fc7c7304bb98cd14e97bed3a8f91e62",
          "message": "Align LeaffixAggregate with LeaffixScan (flat, lazy, zero per-node alloc)\n\nLeaffixAggregate now uses the same flat forward DFS + no-copy ChildAccumulations view as LeaffixScan, yielding each root's accumulated value. Accumulator changes TAccumulate[] -> ChildAccumulations<TAccumulate>. Per-root lazy: a root is emitted the moment its subtree completes and the buffers are reused for the next root, so peak memory is the largest root subtree (not the whole forest) and early-terminating consumers traverse fewer roots -- matching the previous LeaffixAggregator behavior. Zero per-node heap allocation. Adds LeaffixAggregateTests (it previously had none and no callers).\n\nLeaffixAggregator is retained -- still used by Invert (TreeInverter); it'll be removed when Invert is redesigned onto a flat structure.\n\nCo-Authored-By: Claude Opus 4.8 (1M context) <noreply@anthropic.com>\nClaude-Session: https://claude.ai/code/session_01Wg3xArL4FATQaXQMBvhXdg",
          "timestamp": "2026-06-23T21:57:19Z",
          "tree_id": "7dacc032f0faefb81f1e004514e835a60b5bf81a",
          "url": "https://github.com/jasonmcboyd/Arborist/commit/c3ab5ef05fc7c7304bb98cd14e97bed3a8f91e62"
        },
        "date": 1782253174845,
        "tool": "customSmallerIsBetter",
        "benches": [
          {
            "name": "Arborist.Benchmarks.EnumerableToTree.ToDegenerateTree",
            "value": 564,
            "unit": "bytes"
          },
          {
            "name": "Arborist.Benchmarks.EnumerableToTree.ToDegenerateTreeUsingToTree",
            "value": 46145913,
            "unit": "bytes"
          },
          {
            "name": "Arborist.Benchmarks.EnumerableToTree.ToTrivialForest",
            "value": 275,
            "unit": "bytes"
          },
          {
            "name": "Arborist.Benchmarks.EnumerableToTree.ToTrivialForestUsingToTree",
            "value": 1929,
            "unit": "bytes"
          }
        ]
      },
      {
        "commit": {
          "author": {
            "email": "jason.boyd.ce@gmail.com",
            "name": "Jason Boyd",
            "username": "jasonmcboyd"
          },
          "committer": {
            "email": "jason.boyd.ce@gmail.com",
            "name": "Jason Boyd",
            "username": "jasonmcboyd"
          },
          "distinct": true,
          "id": "96f3cc44e2208f47258c2887c2a011530d708864",
          "message": "Rewrite Invert flat; retire SimpleNode and LeaffixAggregator\n\nInvert now materializes the source into flat pre-order arrays and emits the mirror with a stack -- pushing roots/children in forward order so they pop in reverse (the mirror's pre-order). Subtree sizes are invariant under mirroring; result is a PreorderTree. O(N), zero per-node allocation (was a SimpleNode object per node via LeaffixAggregator).\n\nInvert was the last user of both SimpleNode and LeaffixAggregator, so this deletes five files: TreeInverter, LeaffixAggregator (+NodeContextAndResult), SimpleNode, SimpleNodeChildEnumerator, SimpleNodeTreenumerable. The SimpleNode retirement is complete, clearing the runway for the nodeToValueMap elimination. (A level-order/LOUDS mirror -- reverse each child-run in place -- remains a future refinement if a LevelOrderTree is ever built.)\n\nCo-Authored-By: Claude Opus 4.8 (1M context) <noreply@anthropic.com>\nClaude-Session: https://claude.ai/code/session_01Wg3xArL4FATQaXQMBvhXdg",
          "timestamp": "2026-06-23T22:21:16Z",
          "tree_id": "aafd024b9cc4740d5e6c4fe63bc7cf95158cbb5e",
          "url": "https://github.com/jasonmcboyd/Arborist/commit/96f3cc44e2208f47258c2887c2a011530d708864"
        },
        "date": 1782254669146,
        "tool": "customSmallerIsBetter",
        "benches": [
          {
            "name": "Arborist.Benchmarks.EnumerableToTree.ToDegenerateTree",
            "value": 564,
            "unit": "bytes"
          },
          {
            "name": "Arborist.Benchmarks.EnumerableToTree.ToDegenerateTreeUsingToTree",
            "value": 46145913,
            "unit": "bytes"
          },
          {
            "name": "Arborist.Benchmarks.EnumerableToTree.ToTrivialForest",
            "value": 275,
            "unit": "bytes"
          },
          {
            "name": "Arborist.Benchmarks.EnumerableToTree.ToTrivialForestUsingToTree",
            "value": 1921,
            "unit": "bytes"
          }
        ]
      },
      {
        "commit": {
          "author": {
            "email": "jason.boyd.ce@gmail.com",
            "name": "Jason Boyd",
            "username": "jasonmcboyd"
          },
          "committer": {
            "email": "jason.boyd.ce@gmail.com",
            "name": "Jason Boyd",
            "username": "jasonmcboyd"
          },
          "distinct": true,
          "id": "7c795cd25f5b93fbac1993cf6a71485ef8c2de74",
          "message": "Add MemoryDiagnoser benchmark for Invert\n\nCovers TriangleTree depth-1448 (~1M) and a 1M-deep DegenerateTree, tagged LINQ for the dashboard. Completes benchmark coverage of the now-flat transform/aggregation ops (Materialize, LeaffixScan, LeaffixAggregate, Invert).\n\nCo-Authored-By: Claude Opus 4.8 (1M context) <noreply@anthropic.com>\nClaude-Session: https://claude.ai/code/session_01Wg3xArL4FATQaXQMBvhXdg",
          "timestamp": "2026-06-23T22:22:47Z",
          "tree_id": "547984a2584ebd974bf3f4a1790b68fc9b328cfc",
          "url": "https://github.com/jasonmcboyd/Arborist/commit/7c795cd25f5b93fbac1993cf6a71485ef8c2de74"
        },
        "date": 1782256079720,
        "tool": "customSmallerIsBetter",
        "benches": [
          {
            "name": "Arborist.Benchmarks.EnumerableToTree.ToDegenerateTree",
            "value": 575,
            "unit": "bytes"
          },
          {
            "name": "Arborist.Benchmarks.EnumerableToTree.ToDegenerateTreeUsingToTree",
            "value": 46145913,
            "unit": "bytes"
          },
          {
            "name": "Arborist.Benchmarks.EnumerableToTree.ToTrivialForest",
            "value": 275,
            "unit": "bytes"
          },
          {
            "name": "Arborist.Benchmarks.EnumerableToTree.ToTrivialForestUsingToTree",
            "value": 1929,
            "unit": "bytes"
          }
        ]
      },
      {
        "commit": {
          "author": {
            "email": "jason.boyd.ce@gmail.com",
            "name": "Jason Boyd",
            "username": "jasonmcboyd"
          },
          "committer": {
            "email": "jason.boyd.ce@gmail.com",
            "name": "Jason Boyd",
            "username": "jasonmcboyd"
          },
          "distinct": true,
          "id": "cf2c06864e8fdc1e6c24df1f1437f0dae1da26b0",
          "message": "Add 2-param Treenumerable base; drop redundant identity maps (map cleanup stage 1)\n\nThe sample trees whose node IS their surfaced value (TriangleTree, CollatzTree, CompleteBinaryTree, NDecrementTree, DeepTree) were each passing a redundant 'node => node' nodeToValueMap and carrying a duplicate type parameter. Add a 2-param Treenumerable<TNode, TChildEnumerator> convenience base (identity map) and migrate them to it. Public surfaces unchanged. The 3-param base + map remain for PreorderTree's genuine index->value dereference.\n\nCo-Authored-By: Claude Opus 4.8 (1M context) <noreply@anthropic.com>\nClaude-Session: https://claude.ai/code/session_01Wg3xArL4FATQaXQMBvhXdg",
          "timestamp": "2026-06-23T22:35:56Z",
          "tree_id": "005c51f9d7f00116b4e04d1cdb2a5fd0680fc86e",
          "url": "https://github.com/jasonmcboyd/Arborist/commit/cf2c06864e8fdc1e6c24df1f1437f0dae1da26b0"
        },
        "date": 1782257438799,
        "tool": "customSmallerIsBetter",
        "benches": [
          {
            "name": "Arborist.Benchmarks.EnumerableToTree.ToDegenerateTree",
            "value": 564,
            "unit": "bytes"
          },
          {
            "name": "Arborist.Benchmarks.EnumerableToTree.ToDegenerateTreeUsingToTree",
            "value": 46145913,
            "unit": "bytes"
          },
          {
            "name": "Arborist.Benchmarks.EnumerableToTree.ToTrivialForest",
            "value": 275,
            "unit": "bytes"
          },
          {
            "name": "Arborist.Benchmarks.EnumerableToTree.ToTrivialForestUsingToTree",
            "value": 1921,
            "unit": "bytes"
          }
        ]
      },
      {
        "commit": {
          "author": {
            "email": "jason.boyd.ce@gmail.com",
            "name": "Jason Boyd",
            "username": "jasonmcboyd"
          },
          "committer": {
            "email": "jason.boyd.ce@gmail.com",
            "name": "Jason Boyd",
            "username": "jasonmcboyd"
          },
          "distinct": true,
          "id": "ca567e0f439d6d1f0f5c2143add771a6c13fd41c",
          "message": "Fix BFT Where O(depth) memory regression: tail-carry skip prefix\n\nf7eae61's O(N)-time prefix carry stored _PredSkipPrefix as a List<int>\nindexed by absolute inner depth, grown to the current depth on every\nscheduled node -- so a 1M-deep degenerate Where(_=>true) chain allocated\n~8.39 MB even though nothing is predicate-skipped (every entry 0).\n\nReplace it with a tail-carry: the prefix is monotonic non-decreasing in\ndepth and constant (= total skips on the path) beyond the deepest skipped\nancestor, so store only up to the deepest skip and serve reads past the\nend from a scalar tail. New _PrefixStored + _PrefixStoredCount +\n_PrefixTail; PrefixRead/PrefixWriteScheduled (no-op when value == tail, so\nzero allocation in the accepted region); PrefixAnchor truncates the stored\ncount to frontDepth-1 on front-advance.\n\nWhereAll 8.39 MB -> ~1.9 KB (O(1) in depth); WhereNone byte-identical\n(inherent O(depth) preserved). Visit stream byte-identical: validated\nagainst Where2InProcessScan (full c..i, 891k cases) + WhereTests (218/0),\nnet48 + net8.0 clean. Add WhereBreadthFirstAllocationTests as a hard\nmemory-bound regression guard (the gh-pages benchmark only soft-alerts).\n\nCo-Authored-By: Claude Opus 4.8 (1M context) <noreply@anthropic.com>\nClaude-Session: https://claude.ai/code/session_01Wg3xArL4FATQaXQMBvhXdg",
          "timestamp": "2026-06-25T18:13:44Z",
          "tree_id": "6d5c2256d2c8574537a4127f73a8de04fe0b4011",
          "url": "https://github.com/jasonmcboyd/Arborist/commit/ca567e0f439d6d1f0f5c2143add771a6c13fd41c"
        },
        "date": 1782413421467,
        "tool": "customSmallerIsBetter",
        "benches": [
          {
            "name": "Arborist.Benchmarks.EnumerableToTree.ToDegenerateTree",
            "value": 564,
            "unit": "bytes"
          },
          {
            "name": "Arborist.Benchmarks.EnumerableToTree.ToDegenerateTreeUsingToTree",
            "value": 46145913,
            "unit": "bytes"
          },
          {
            "name": "Arborist.Benchmarks.EnumerableToTree.ToTrivialForest",
            "value": 275,
            "unit": "bytes"
          },
          {
            "name": "Arborist.Benchmarks.EnumerableToTree.ToTrivialForestUsingToTree",
            "value": 1933,
            "unit": "bytes"
          }
        ]
      },
      {
        "commit": {
          "author": {
            "email": "jason.boyd.ce@gmail.com",
            "name": "Jason Boyd",
            "username": "jasonmcboyd"
          },
          "committer": {
            "email": "jason.boyd.ce@gmail.com",
            "name": "Jason Boyd",
            "username": "jasonmcboyd"
          },
          "distinct": true,
          "id": "6a5d01f5e6181c1c89a0993a6360bab3a4b0bac7",
          "message": "Rewrite BreadthFirstTreenumerator with a structural visit cadence\n\nThe base BFT engine emitted parent visits reactively (when a child was\naccepted), which broke whenever a child was filtered and forced a tangle\nof deferred parent-visit bookkeeping to recover the swallowed visits.\n\nReplace it with a structural cadence: a single FIFO _VisitQueue whose\nfront is the active parent, plus a LIFO _ScheduleStack for the SkipNode\npromotion descent. A parent is visited once when it reaches the front,\nthen once after every child slot that enqueues at least one accepted\nnode -- a single bool, _CurrentSlotEnqueuedNode. Roots are scheduled\nfirst as the children of an implicit no-visit forest sentinel.\n\nThis deletes _OwesPromotedParentVisit, _HasDeferredScheduledChild,\n_DepthOfLastActedOnNode, PayOwedParentVisitAndDeferChild, and the\nOnScheduling/OnVisiting/PromoteChildren/SkipSubtree/Backtrack web in\nfavor of Advance/ApplyStrategy/SkipRemainingSiblings. The now-unused\nOwesPromotedParentVisit field comes off the shared InternalNodeVisitState\nstruct, shrinking every deque slot.\n\nNo behavior change: same (Mode, Node, VisitCount, Position) visit stream.\n\nValidation:\n- Exhaustive BFT-vs-DFT oracle, up to 6 concurrent skips x 27 trees\n- Where2InProcessScan: 891,056 Where-wrapper-vs-oracle cases (groups c..i)\n- Curated exact-order traversal + 14,759 Where/allocation tests\n- Benchmarks: allocations -12% to -14%, time within ShortRun noise\n\nCo-Authored-By: Claude Opus 4.8 (1M context) <noreply@anthropic.com>\nClaude-Session: https://claude.ai/code/session_01Wg3xArL4FATQaXQMBvhXdg",
          "timestamp": "2026-06-26T00:47:42Z",
          "tree_id": "39f01ca28e49bf3bbc704caca59e8d4bb815b334",
          "url": "https://github.com/jasonmcboyd/Arborist/commit/6a5d01f5e6181c1c89a0993a6360bab3a4b0bac7"
        },
        "date": 1782436872595,
        "tool": "customSmallerIsBetter",
        "benches": [
          {
            "name": "Arborist.Benchmarks.EnumerableToTree.ToDegenerateTree",
            "value": 564,
            "unit": "bytes"
          },
          {
            "name": "Arborist.Benchmarks.EnumerableToTree.ToDegenerateTreeUsingToTree",
            "value": 41952840,
            "unit": "bytes"
          },
          {
            "name": "Arborist.Benchmarks.EnumerableToTree.ToTrivialForest",
            "value": 275,
            "unit": "bytes"
          },
          {
            "name": "Arborist.Benchmarks.EnumerableToTree.ToTrivialForestUsingToTree",
            "value": 1849,
            "unit": "bytes"
          }
        ]
      },
      {
        "commit": {
          "author": {
            "email": "jason.boyd.ce@gmail.com",
            "name": "Jason Boyd",
            "username": "jasonmcboyd"
          },
          "committer": {
            "email": "jason.boyd.ce@gmail.com",
            "name": "Jason Boyd",
            "username": "jasonmcboyd"
          },
          "distinct": true,
          "id": "ce22f8e1055cf2b7bf6486f1da78d2058d8c69aa",
          "message": "Cap RefSemiDeque partition size to bound LOH allocation and overshoot\n\nGeometric partition growth sized each new partition to the running total\nCapacity, so the largest partition reached ~half the deque's peak element\ncount -- a multi-MB Large Object Heap allocation on wide/deep trees, plus\nup to ~2x peak over-allocation at power-of-two boundaries. Cap partition\nsize at 4096 elements (Math.Min(Capacity, MaxPartitionSize)) to bound both.\n\nBFT CompleteBinaryTree_21 (peak frontier ~2^21, the worst-case boundary):\n96 MB -> 48 MB allocated per traversal, throughput unchanged.\n\nFixed element count rather than a byte budget that would force partitions\nsub-LOH: forcing a 64 B node's partitions sub-LOH measured ~40% slower with\n~7x the Gen0 collections, because large long-lived blocks belong on the LOH.\n\nAdd RefSemiDeque regression tests crossing the cap (heterogeneous-partition\nordering, out-of-order recycling, GetFromBack/RemoveLast across boundaries)\nand an Add_Block64_1M benchmark. Remove unused InitialCapacity.\n\nCo-Authored-By: Claude Opus 4.8 (1M context) <noreply@anthropic.com>",
          "timestamp": "2026-06-26T04:13:38Z",
          "tree_id": "e35c9314063b1f6eda6dfccd1d7349907af73e32",
          "url": "https://github.com/jasonmcboyd/Arborist/commit/ce22f8e1055cf2b7bf6486f1da78d2058d8c69aa"
        },
        "date": 1782449422395,
        "tool": "customSmallerIsBetter",
        "benches": [
          {
            "name": "Arborist.Benchmarks.EnumerableToTree.ToDegenerateTree",
            "value": 556,
            "unit": "bytes"
          },
          {
            "name": "Arborist.Benchmarks.EnumerableToTree.ToDegenerateTreeUsingToTree",
            "value": 41987175,
            "unit": "bytes"
          },
          {
            "name": "Arborist.Benchmarks.EnumerableToTree.ToTrivialForest",
            "value": 275,
            "unit": "bytes"
          },
          {
            "name": "Arborist.Benchmarks.EnumerableToTree.ToTrivialForestUsingToTree",
            "value": 1821,
            "unit": "bytes"
          }
        ]
      },
      {
        "commit": {
          "author": {
            "email": "jason.boyd.ce@gmail.com",
            "name": "Jason Boyd",
            "username": "jasonmcboyd"
          },
          "committer": {
            "email": "jason.boyd.ce@gmail.com",
            "name": "Jason Boyd",
            "username": "jasonmcboyd"
          },
          "distinct": true,
          "id": "81b46c42702e71ad313d08c636eb3e3c3c35b140",
          "message": "Cohere BreadthFirstTreenumerator's four deques into one frame stack\n\nThe BFT kept each node's visit state and its child enumerator in FOUR parallel\nRefSemiDeques -- a state deque and an enumerator deque for each of the visit\nqueue and the schedule stack -- relying on keeping each pair in lockstep.\n\nFold each pair into one RefSemiDeque<Frame>, where Frame bundles\n{Node, Position, VisitCount, ChildEnumerator}, driven only by ref so the\nenumerator is never copied. Accepting a node becomes a single whole-frame move\nfrom the schedule stack to the visit queue, which structurally prevents a node\nand its enumerator from desynchronizing. The algorithm and visit cadence are\nunchanged. Mirrors the depth-first engine's frame stack, and removes the\nnow-unreferenced shared InternalNodeVisitState struct.\n\nNo behavior change: same (Mode, Node, VisitCount, Position) visit stream;\ndisposal (including the deliberate idempotent double-dispose on\nSkipDescendants/SkipSiblings) is at exact parity with the original.\n\nValidation:\n- Exact-order traversal + exhaustive DFT-vs-BFT multiset scan (438/0)\n- Full Arborist.Linq suite incl. Where (14,759/0)\n- Benchmarks (Release/Job.Default, clean tree): TriangleTree 289->255ms (-12%),\n  CompleteBinaryTree 337->281ms (-16%), TrivialForest/DegenerateTree 4M within\n  noise; allocation neutral, still zero per-node heap allocation. Timing\n  variance also dropped (StdDev roughly halved on the dense trees).\n\nCo-Authored-By: Claude Opus 4.8 (1M context) <noreply@anthropic.com>\nClaude-Session: https://claude.ai/code/session_01Wg3xArL4FATQaXQMBvhXdg",
          "timestamp": "2026-06-26T21:37:21Z",
          "tree_id": "05aa22f3461078c05e96518d52da4d56655c250f",
          "url": "https://github.com/jasonmcboyd/Arborist/commit/81b46c42702e71ad313d08c636eb3e3c3c35b140"
        },
        "date": 1782511429224,
        "tool": "customSmallerIsBetter",
        "benches": [
          {
            "name": "Arborist.Benchmarks.EnumerableToTree.ToDegenerateTree",
            "value": 556,
            "unit": "bytes"
          },
          {
            "name": "Arborist.Benchmarks.EnumerableToTree.ToDegenerateTreeUsingToTree",
            "value": 41969504,
            "unit": "bytes"
          },
          {
            "name": "Arborist.Benchmarks.EnumerableToTree.ToTrivialForest",
            "value": 275,
            "unit": "bytes"
          },
          {
            "name": "Arborist.Benchmarks.EnumerableToTree.ToTrivialForestUsingToTree",
            "value": 1455,
            "unit": "bytes"
          }
        ]
      },
      {
        "commit": {
          "author": {
            "email": "jason.boyd.ce@gmail.com",
            "name": "Jason Boyd",
            "username": "jasonmcboyd"
          },
          "committer": {
            "email": "jason.boyd.ce@gmail.com",
            "name": "Jason Boyd",
            "username": "jasonmcboyd"
          },
          "distinct": true,
          "id": "ab94b3983058299be15f1b4d38b05b21509cb8ad",
          "message": "Encapsulate BFT state in BreadthFirstPath; mirror the DFT driver/state split\n\nMove the breadth-first engine's state -- the visit queue, the schedule stack,\nthe owed-return-visit carry, and the root sibling counter -- into a new\nBreadthFirstPath, leaving BreadthFirstTreenumerator a thin driver, mirroring the\ndepth-first DepthFirstPath split. The cohesive Frame (visit state + child\nenumerator) is kept -- the BFT keeps full state for every resident node anyway,\nso it costs no memory -- so allocation is unchanged from the cohesion engine.\n\nLike DepthFirstPath, BreadthFirstPath is \"sans-I/O\": it never pulls a child; it\nexposes the two active enumerators (the schedule-stack top and the visit-queue\nfront) by ref for the driver to advance. That isolates the engine's asynchronous\noperations to those seams, so a future async BFT can share this class and differ\nonly there. It is a mutable struct embedded as the driver's single _Path field\n(never copied; refs it returns point into the heap deques), keeping dense\ntraversal at the cohesion engine's speed. The two child-pull sites collapse into\none TryScheduleNextChildOf(ref parent).\n\nNo behavior change: same (Mode, Node, VisitCount, Position) visit stream.\n\nValidation:\n- Exact-order traversal + exhaustive DFT-vs-BFT multiset scan (438/0)\n- Full Arborist.Linq suite incl. Where (14,759/0)\n- Benchmarks (Release/Job.Default, same session): TriangleTree 194.5 vs cohesion\n  193.9 ms; CompleteBinaryTree 208.1 vs 213.0 ms (parity). Allocation identical\n  to the cohesion engine (same Frame).\n\nCo-Authored-By: Claude Opus 4.8 (1M context) <noreply@anthropic.com>\nClaude-Session: https://claude.ai/code/session_01Wg3xArL4FATQaXQMBvhXdg",
          "timestamp": "2026-06-27T00:09:54Z",
          "tree_id": "8703f7198030df75b83a7026b75dc74f78b17190",
          "url": "https://github.com/jasonmcboyd/Arborist/commit/ab94b3983058299be15f1b4d38b05b21509cb8ad"
        },
        "date": 1782521636734,
        "tool": "customSmallerIsBetter",
        "benches": [
          {
            "name": "Arborist.Benchmarks.EnumerableToTree.ToDegenerateTree",
            "value": 556,
            "unit": "bytes"
          },
          {
            "name": "Arborist.Benchmarks.EnumerableToTree.ToDegenerateTreeUsingToTree",
            "value": 41969535,
            "unit": "bytes"
          },
          {
            "name": "Arborist.Benchmarks.EnumerableToTree.ToTrivialForest",
            "value": 275,
            "unit": "bytes"
          },
          {
            "name": "Arborist.Benchmarks.EnumerableToTree.ToTrivialForestUsingToTree",
            "value": 1463,
            "unit": "bytes"
          }
        ]
      },
      {
        "commit": {
          "author": {
            "email": "jason.boyd.ce@gmail.com",
            "name": "Jason Boyd",
            "username": "jasonmcboyd"
          },
          "committer": {
            "email": "jason.boyd.ce@gmail.com",
            "name": "Jason Boyd",
            "username": "jasonmcboyd"
          },
          "distinct": true,
          "id": "6ebd5d0e1d60592672f966eaa4ab81a302c56999",
          "message": "Fix DFT skip-heavy regression: keep TryPushNextChild out-of-line\n\nThe encapsulated DepthFirstPath DFT (14f8393) ran ~1.7-1.9x slower than the\noriginal two-stack on promotion-heavy skip traversal (SkipAllNodes / Preorder /\nPostorder on wide trees), despite identical allocation. JIT disassembly\n(DOTNET_JitDisasm) pinned the cause: [AggressiveInlining] on TryPushNextChild\ninlined the entire promote body (pull + push) into OnMoveNext/OnScheduling,\ninflating their frames to 6 callee-saved registers + sub rsp,72 + vzeroupper and\ndowngrading OnMoveNext's branch dispatch from a tail-jmp to a call+teardown paid\non EVERY node. The original two-stack stayed fast precisely because its promote\nwas a separate, out-of-line method.\n\nMark TryPushNextChild [NoInlining] so the drivers stay thin tail-dispatchers,\nwhile keeping the push chain (PushChild/PushLevel) force-inlined INTO it so the\npush itself is still call-free. Also fold Backtrack's pop + three predicate\nchecks into one DepthFirstPath.PopFinishedLevelAndClassify call: the original\ninline-predicate form is O(1) per level but its repeated struct round-trips cost\n~2x on the deep-unwind path (GetLeaves.DeepTree); folding restores it.\n\nNet (Release/Job.Default, local, vs original two-stack): SkipAllNodes.Dft\n41 -> 22.6 ms, Preorder 42.8 -> 25.7, Postorder 47 -> 32.9 -- all back at the\noriginal; dense traversal and GetLeaves.DeepTree within noise; allocation\nunchanged. The sans-I/O encapsulation (path never pulls; one ref seam) is intact.\n\nValidation: 438/0 oracle (exact-order + exhaustive DFT-vs-BFT scan), 14,759/0\nfull Linq suite.\n\nCo-Authored-By: Claude Opus 4.8 (1M context) <noreply@anthropic.com>\nClaude-Session: https://claude.ai/code/session_01Wg3xArL4FATQaXQMBvhXdg",
          "timestamp": "2026-06-27T02:26:54Z",
          "tree_id": "5301780378953d2f586361d71d9e7e7b8dc1e6d3",
          "url": "https://github.com/jasonmcboyd/Arborist/commit/6ebd5d0e1d60592672f966eaa4ab81a302c56999"
        },
        "date": 1782529880992,
        "tool": "customSmallerIsBetter",
        "benches": [
          {
            "name": "Arborist.Benchmarks.EnumerableToTree.ToDegenerateTree",
            "value": 556,
            "unit": "bytes"
          },
          {
            "name": "Arborist.Benchmarks.EnumerableToTree.ToDegenerateTreeUsingToTree",
            "value": 41971110,
            "unit": "bytes"
          },
          {
            "name": "Arborist.Benchmarks.EnumerableToTree.ToTrivialForest",
            "value": 275,
            "unit": "bytes"
          },
          {
            "name": "Arborist.Benchmarks.EnumerableToTree.ToTrivialForestUsingToTree",
            "value": 1463,
            "unit": "bytes"
          }
        ]
      }
    ],
    "DataStructures Memory": [
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
        "date": 1769975446612,
        "tool": "customSmallerIsBetter",
        "benches": [
          {
            "name": "Arborist.Benchmarks.RefSemiDeque.Add_8M",
            "value": 33557386,
            "unit": "bytes"
          },
          {
            "name": "Arborist.Benchmarks.RefSemiDeque.RemoveFirst_8M",
            "value": 33557386,
            "unit": "bytes"
          },
          {
            "name": "Arborist.Benchmarks.RefSemiDeque.RemoveLast_8M",
            "value": 33557386,
            "unit": "bytes"
          }
        ]
      },
      {
        "commit": {
          "author": {
            "email": "jason.boyd.ce@gmail.com",
            "name": "Jason Boyd",
            "username": "jasonmcboyd"
          },
          "committer": {
            "email": "jason.boyd.ce@gmail.com",
            "name": "Jason Boyd",
            "username": "jasonmcboyd"
          },
          "distinct": true,
          "id": "5993e5762d3c0ffa1bbacbb40b35307cff9a3f87",
          "message": "Docs: document BFT Where O(N) prefix-carry design\n\nRewrite the WhereBreadthFirstTreenumerator section to match the implementation (queue + incremental _PredSkipPrefix, predicate-skip vs consumer-SkipNode, the real parent-visit fields, front-anchored sibling index). Update the DFT-vs-BFT table (BFT now O(N)) and fix stale _ExtraParentVisitsEmitted/_LastRemovedSkippedNodePosition/_SkippedStack references.\n\nCo-Authored-By: Claude Opus 4.8 (1M context) <noreply@anthropic.com>\nClaude-Session: https://claude.ai/code/session_01Wg3xArL4FATQaXQMBvhXdg",
          "timestamp": "2026-06-23T04:33:54Z",
          "tree_id": "a2615b3ce8e91df5baffd5f8f08e3c6ac9df2e3b",
          "url": "https://github.com/jasonmcboyd/Arborist/commit/5993e5762d3c0ffa1bbacbb40b35307cff9a3f87"
        },
        "date": 1782190986244,
        "tool": "customSmallerIsBetter",
        "benches": [
          {
            "name": "Arborist.Benchmarks.RefSemiDeque.Add_8M",
            "value": 33557386,
            "unit": "bytes"
          },
          {
            "name": "Arborist.Benchmarks.RefSemiDeque.RemoveFirst_8M",
            "value": 33557386,
            "unit": "bytes"
          },
          {
            "name": "Arborist.Benchmarks.RefSemiDeque.RemoveLast_8M",
            "value": 33557386,
            "unit": "bytes"
          }
        ]
      },
      {
        "commit": {
          "author": {
            "email": "jason.boyd.ce@gmail.com",
            "name": "Jason Boyd",
            "username": "jasonmcboyd"
          },
          "committer": {
            "email": "jason.boyd.ce@gmail.com",
            "name": "Jason Boyd",
            "username": "jasonmcboyd"
          },
          "distinct": true,
          "id": "c3ab5ef05fc7c7304bb98cd14e97bed3a8f91e62",
          "message": "Align LeaffixAggregate with LeaffixScan (flat, lazy, zero per-node alloc)\n\nLeaffixAggregate now uses the same flat forward DFS + no-copy ChildAccumulations view as LeaffixScan, yielding each root's accumulated value. Accumulator changes TAccumulate[] -> ChildAccumulations<TAccumulate>. Per-root lazy: a root is emitted the moment its subtree completes and the buffers are reused for the next root, so peak memory is the largest root subtree (not the whole forest) and early-terminating consumers traverse fewer roots -- matching the previous LeaffixAggregator behavior. Zero per-node heap allocation. Adds LeaffixAggregateTests (it previously had none and no callers).\n\nLeaffixAggregator is retained -- still used by Invert (TreeInverter); it'll be removed when Invert is redesigned onto a flat structure.\n\nCo-Authored-By: Claude Opus 4.8 (1M context) <noreply@anthropic.com>\nClaude-Session: https://claude.ai/code/session_01Wg3xArL4FATQaXQMBvhXdg",
          "timestamp": "2026-06-23T21:57:19Z",
          "tree_id": "7dacc032f0faefb81f1e004514e835a60b5bf81a",
          "url": "https://github.com/jasonmcboyd/Arborist/commit/c3ab5ef05fc7c7304bb98cd14e97bed3a8f91e62"
        },
        "date": 1782253175026,
        "tool": "customSmallerIsBetter",
        "benches": [
          {
            "name": "Arborist.Benchmarks.RefSemiDeque.Add_8M",
            "value": 33557386,
            "unit": "bytes"
          },
          {
            "name": "Arborist.Benchmarks.RefSemiDeque.RemoveFirst_8M",
            "value": 33557386,
            "unit": "bytes"
          },
          {
            "name": "Arborist.Benchmarks.RefSemiDeque.RemoveLast_8M",
            "value": 33557386,
            "unit": "bytes"
          }
        ]
      },
      {
        "commit": {
          "author": {
            "email": "jason.boyd.ce@gmail.com",
            "name": "Jason Boyd",
            "username": "jasonmcboyd"
          },
          "committer": {
            "email": "jason.boyd.ce@gmail.com",
            "name": "Jason Boyd",
            "username": "jasonmcboyd"
          },
          "distinct": true,
          "id": "96f3cc44e2208f47258c2887c2a011530d708864",
          "message": "Rewrite Invert flat; retire SimpleNode and LeaffixAggregator\n\nInvert now materializes the source into flat pre-order arrays and emits the mirror with a stack -- pushing roots/children in forward order so they pop in reverse (the mirror's pre-order). Subtree sizes are invariant under mirroring; result is a PreorderTree. O(N), zero per-node allocation (was a SimpleNode object per node via LeaffixAggregator).\n\nInvert was the last user of both SimpleNode and LeaffixAggregator, so this deletes five files: TreeInverter, LeaffixAggregator (+NodeContextAndResult), SimpleNode, SimpleNodeChildEnumerator, SimpleNodeTreenumerable. The SimpleNode retirement is complete, clearing the runway for the nodeToValueMap elimination. (A level-order/LOUDS mirror -- reverse each child-run in place -- remains a future refinement if a LevelOrderTree is ever built.)\n\nCo-Authored-By: Claude Opus 4.8 (1M context) <noreply@anthropic.com>\nClaude-Session: https://claude.ai/code/session_01Wg3xArL4FATQaXQMBvhXdg",
          "timestamp": "2026-06-23T22:21:16Z",
          "tree_id": "aafd024b9cc4740d5e6c4fe63bc7cf95158cbb5e",
          "url": "https://github.com/jasonmcboyd/Arborist/commit/96f3cc44e2208f47258c2887c2a011530d708864"
        },
        "date": 1782254669313,
        "tool": "customSmallerIsBetter",
        "benches": [
          {
            "name": "Arborist.Benchmarks.RefSemiDeque.Add_8M",
            "value": 33557386,
            "unit": "bytes"
          },
          {
            "name": "Arborist.Benchmarks.RefSemiDeque.RemoveFirst_8M",
            "value": 33557389,
            "unit": "bytes"
          },
          {
            "name": "Arborist.Benchmarks.RefSemiDeque.RemoveLast_8M",
            "value": 33557388,
            "unit": "bytes"
          }
        ]
      },
      {
        "commit": {
          "author": {
            "email": "jason.boyd.ce@gmail.com",
            "name": "Jason Boyd",
            "username": "jasonmcboyd"
          },
          "committer": {
            "email": "jason.boyd.ce@gmail.com",
            "name": "Jason Boyd",
            "username": "jasonmcboyd"
          },
          "distinct": true,
          "id": "7c795cd25f5b93fbac1993cf6a71485ef8c2de74",
          "message": "Add MemoryDiagnoser benchmark for Invert\n\nCovers TriangleTree depth-1448 (~1M) and a 1M-deep DegenerateTree, tagged LINQ for the dashboard. Completes benchmark coverage of the now-flat transform/aggregation ops (Materialize, LeaffixScan, LeaffixAggregate, Invert).\n\nCo-Authored-By: Claude Opus 4.8 (1M context) <noreply@anthropic.com>\nClaude-Session: https://claude.ai/code/session_01Wg3xArL4FATQaXQMBvhXdg",
          "timestamp": "2026-06-23T22:22:47Z",
          "tree_id": "547984a2584ebd974bf3f4a1790b68fc9b328cfc",
          "url": "https://github.com/jasonmcboyd/Arborist/commit/7c795cd25f5b93fbac1993cf6a71485ef8c2de74"
        },
        "date": 1782256079919,
        "tool": "customSmallerIsBetter",
        "benches": [
          {
            "name": "Arborist.Benchmarks.RefSemiDeque.Add_8M",
            "value": 33557386,
            "unit": "bytes"
          },
          {
            "name": "Arborist.Benchmarks.RefSemiDeque.RemoveFirst_8M",
            "value": 33557386,
            "unit": "bytes"
          },
          {
            "name": "Arborist.Benchmarks.RefSemiDeque.RemoveLast_8M",
            "value": 33557386,
            "unit": "bytes"
          }
        ]
      },
      {
        "commit": {
          "author": {
            "email": "jason.boyd.ce@gmail.com",
            "name": "Jason Boyd",
            "username": "jasonmcboyd"
          },
          "committer": {
            "email": "jason.boyd.ce@gmail.com",
            "name": "Jason Boyd",
            "username": "jasonmcboyd"
          },
          "distinct": true,
          "id": "cf2c06864e8fdc1e6c24df1f1437f0dae1da26b0",
          "message": "Add 2-param Treenumerable base; drop redundant identity maps (map cleanup stage 1)\n\nThe sample trees whose node IS their surfaced value (TriangleTree, CollatzTree, CompleteBinaryTree, NDecrementTree, DeepTree) were each passing a redundant 'node => node' nodeToValueMap and carrying a duplicate type parameter. Add a 2-param Treenumerable<TNode, TChildEnumerator> convenience base (identity map) and migrate them to it. Public surfaces unchanged. The 3-param base + map remain for PreorderTree's genuine index->value dereference.\n\nCo-Authored-By: Claude Opus 4.8 (1M context) <noreply@anthropic.com>\nClaude-Session: https://claude.ai/code/session_01Wg3xArL4FATQaXQMBvhXdg",
          "timestamp": "2026-06-23T22:35:56Z",
          "tree_id": "005c51f9d7f00116b4e04d1cdb2a5fd0680fc86e",
          "url": "https://github.com/jasonmcboyd/Arborist/commit/cf2c06864e8fdc1e6c24df1f1437f0dae1da26b0"
        },
        "date": 1782257438958,
        "tool": "customSmallerIsBetter",
        "benches": [
          {
            "name": "Arborist.Benchmarks.RefSemiDeque.Add_8M",
            "value": 33557386,
            "unit": "bytes"
          },
          {
            "name": "Arborist.Benchmarks.RefSemiDeque.RemoveFirst_8M",
            "value": 33557389,
            "unit": "bytes"
          },
          {
            "name": "Arborist.Benchmarks.RefSemiDeque.RemoveLast_8M",
            "value": 33557388,
            "unit": "bytes"
          }
        ]
      },
      {
        "commit": {
          "author": {
            "email": "jason.boyd.ce@gmail.com",
            "name": "Jason Boyd",
            "username": "jasonmcboyd"
          },
          "committer": {
            "email": "jason.boyd.ce@gmail.com",
            "name": "Jason Boyd",
            "username": "jasonmcboyd"
          },
          "distinct": true,
          "id": "ca567e0f439d6d1f0f5c2143add771a6c13fd41c",
          "message": "Fix BFT Where O(depth) memory regression: tail-carry skip prefix\n\nf7eae61's O(N)-time prefix carry stored _PredSkipPrefix as a List<int>\nindexed by absolute inner depth, grown to the current depth on every\nscheduled node -- so a 1M-deep degenerate Where(_=>true) chain allocated\n~8.39 MB even though nothing is predicate-skipped (every entry 0).\n\nReplace it with a tail-carry: the prefix is monotonic non-decreasing in\ndepth and constant (= total skips on the path) beyond the deepest skipped\nancestor, so store only up to the deepest skip and serve reads past the\nend from a scalar tail. New _PrefixStored + _PrefixStoredCount +\n_PrefixTail; PrefixRead/PrefixWriteScheduled (no-op when value == tail, so\nzero allocation in the accepted region); PrefixAnchor truncates the stored\ncount to frontDepth-1 on front-advance.\n\nWhereAll 8.39 MB -> ~1.9 KB (O(1) in depth); WhereNone byte-identical\n(inherent O(depth) preserved). Visit stream byte-identical: validated\nagainst Where2InProcessScan (full c..i, 891k cases) + WhereTests (218/0),\nnet48 + net8.0 clean. Add WhereBreadthFirstAllocationTests as a hard\nmemory-bound regression guard (the gh-pages benchmark only soft-alerts).\n\nCo-Authored-By: Claude Opus 4.8 (1M context) <noreply@anthropic.com>\nClaude-Session: https://claude.ai/code/session_01Wg3xArL4FATQaXQMBvhXdg",
          "timestamp": "2026-06-25T18:13:44Z",
          "tree_id": "6d5c2256d2c8574537a4127f73a8de04fe0b4011",
          "url": "https://github.com/jasonmcboyd/Arborist/commit/ca567e0f439d6d1f0f5c2143add771a6c13fd41c"
        },
        "date": 1782413421662,
        "tool": "customSmallerIsBetter",
        "benches": [
          {
            "name": "Arborist.Benchmarks.RefSemiDeque.Add_8M",
            "value": 33557386,
            "unit": "bytes"
          },
          {
            "name": "Arborist.Benchmarks.RefSemiDeque.RemoveFirst_8M",
            "value": 33557386,
            "unit": "bytes"
          },
          {
            "name": "Arborist.Benchmarks.RefSemiDeque.RemoveLast_8M",
            "value": 33557386,
            "unit": "bytes"
          }
        ]
      },
      {
        "commit": {
          "author": {
            "email": "jason.boyd.ce@gmail.com",
            "name": "Jason Boyd",
            "username": "jasonmcboyd"
          },
          "committer": {
            "email": "jason.boyd.ce@gmail.com",
            "name": "Jason Boyd",
            "username": "jasonmcboyd"
          },
          "distinct": true,
          "id": "6a5d01f5e6181c1c89a0993a6360bab3a4b0bac7",
          "message": "Rewrite BreadthFirstTreenumerator with a structural visit cadence\n\nThe base BFT engine emitted parent visits reactively (when a child was\naccepted), which broke whenever a child was filtered and forced a tangle\nof deferred parent-visit bookkeeping to recover the swallowed visits.\n\nReplace it with a structural cadence: a single FIFO _VisitQueue whose\nfront is the active parent, plus a LIFO _ScheduleStack for the SkipNode\npromotion descent. A parent is visited once when it reaches the front,\nthen once after every child slot that enqueues at least one accepted\nnode -- a single bool, _CurrentSlotEnqueuedNode. Roots are scheduled\nfirst as the children of an implicit no-visit forest sentinel.\n\nThis deletes _OwesPromotedParentVisit, _HasDeferredScheduledChild,\n_DepthOfLastActedOnNode, PayOwedParentVisitAndDeferChild, and the\nOnScheduling/OnVisiting/PromoteChildren/SkipSubtree/Backtrack web in\nfavor of Advance/ApplyStrategy/SkipRemainingSiblings. The now-unused\nOwesPromotedParentVisit field comes off the shared InternalNodeVisitState\nstruct, shrinking every deque slot.\n\nNo behavior change: same (Mode, Node, VisitCount, Position) visit stream.\n\nValidation:\n- Exhaustive BFT-vs-DFT oracle, up to 6 concurrent skips x 27 trees\n- Where2InProcessScan: 891,056 Where-wrapper-vs-oracle cases (groups c..i)\n- Curated exact-order traversal + 14,759 Where/allocation tests\n- Benchmarks: allocations -12% to -14%, time within ShortRun noise\n\nCo-Authored-By: Claude Opus 4.8 (1M context) <noreply@anthropic.com>\nClaude-Session: https://claude.ai/code/session_01Wg3xArL4FATQaXQMBvhXdg",
          "timestamp": "2026-06-26T00:47:42Z",
          "tree_id": "39f01ca28e49bf3bbc704caca59e8d4bb815b334",
          "url": "https://github.com/jasonmcboyd/Arborist/commit/6a5d01f5e6181c1c89a0993a6360bab3a4b0bac7"
        },
        "date": 1782436872794,
        "tool": "customSmallerIsBetter",
        "benches": [
          {
            "name": "Arborist.Benchmarks.RefSemiDeque.Add_8M",
            "value": 33557386,
            "unit": "bytes"
          },
          {
            "name": "Arborist.Benchmarks.RefSemiDeque.RemoveFirst_8M",
            "value": 33557386,
            "unit": "bytes"
          },
          {
            "name": "Arborist.Benchmarks.RefSemiDeque.RemoveLast_8M",
            "value": 33557386,
            "unit": "bytes"
          }
        ]
      },
      {
        "commit": {
          "author": {
            "email": "jason.boyd.ce@gmail.com",
            "name": "Jason Boyd",
            "username": "jasonmcboyd"
          },
          "committer": {
            "email": "jason.boyd.ce@gmail.com",
            "name": "Jason Boyd",
            "username": "jasonmcboyd"
          },
          "distinct": true,
          "id": "ce22f8e1055cf2b7bf6486f1da78d2058d8c69aa",
          "message": "Cap RefSemiDeque partition size to bound LOH allocation and overshoot\n\nGeometric partition growth sized each new partition to the running total\nCapacity, so the largest partition reached ~half the deque's peak element\ncount -- a multi-MB Large Object Heap allocation on wide/deep trees, plus\nup to ~2x peak over-allocation at power-of-two boundaries. Cap partition\nsize at 4096 elements (Math.Min(Capacity, MaxPartitionSize)) to bound both.\n\nBFT CompleteBinaryTree_21 (peak frontier ~2^21, the worst-case boundary):\n96 MB -> 48 MB allocated per traversal, throughput unchanged.\n\nFixed element count rather than a byte budget that would force partitions\nsub-LOH: forcing a 64 B node's partitions sub-LOH measured ~40% slower with\n~7x the Gen0 collections, because large long-lived blocks belong on the LOH.\n\nAdd RefSemiDeque regression tests crossing the cap (heterogeneous-partition\nordering, out-of-order recycling, GetFromBack/RemoveLast across boundaries)\nand an Add_Block64_1M benchmark. Remove unused InitialCapacity.\n\nCo-Authored-By: Claude Opus 4.8 (1M context) <noreply@anthropic.com>",
          "timestamp": "2026-06-26T04:13:38Z",
          "tree_id": "e35c9314063b1f6eda6dfccd1d7349907af73e32",
          "url": "https://github.com/jasonmcboyd/Arborist/commit/ce22f8e1055cf2b7bf6486f1da78d2058d8c69aa"
        },
        "date": 1782449422598,
        "tool": "customSmallerIsBetter",
        "benches": [
          {
            "name": "Arborist.Benchmarks.RefSemiDeque.Add_8M",
            "value": 32160764,
            "unit": "bytes"
          },
          {
            "name": "Arborist.Benchmarks.RefSemiDeque.RemoveFirst_8M",
            "value": 32160515,
            "unit": "bytes"
          },
          {
            "name": "Arborist.Benchmarks.RefSemiDeque.RemoveLast_8M",
            "value": 32160620,
            "unit": "bytes"
          },
          {
            "name": "Arborist.Benchmarks.RefSemiDeque.Add_Block64_1M",
            "value": 64249958,
            "unit": "bytes"
          }
        ]
      },
      {
        "commit": {
          "author": {
            "email": "jason.boyd.ce@gmail.com",
            "name": "Jason Boyd",
            "username": "jasonmcboyd"
          },
          "committer": {
            "email": "jason.boyd.ce@gmail.com",
            "name": "Jason Boyd",
            "username": "jasonmcboyd"
          },
          "distinct": true,
          "id": "81b46c42702e71ad313d08c636eb3e3c3c35b140",
          "message": "Cohere BreadthFirstTreenumerator's four deques into one frame stack\n\nThe BFT kept each node's visit state and its child enumerator in FOUR parallel\nRefSemiDeques -- a state deque and an enumerator deque for each of the visit\nqueue and the schedule stack -- relying on keeping each pair in lockstep.\n\nFold each pair into one RefSemiDeque<Frame>, where Frame bundles\n{Node, Position, VisitCount, ChildEnumerator}, driven only by ref so the\nenumerator is never copied. Accepting a node becomes a single whole-frame move\nfrom the schedule stack to the visit queue, which structurally prevents a node\nand its enumerator from desynchronizing. The algorithm and visit cadence are\nunchanged. Mirrors the depth-first engine's frame stack, and removes the\nnow-unreferenced shared InternalNodeVisitState struct.\n\nNo behavior change: same (Mode, Node, VisitCount, Position) visit stream;\ndisposal (including the deliberate idempotent double-dispose on\nSkipDescendants/SkipSiblings) is at exact parity with the original.\n\nValidation:\n- Exact-order traversal + exhaustive DFT-vs-BFT multiset scan (438/0)\n- Full Arborist.Linq suite incl. Where (14,759/0)\n- Benchmarks (Release/Job.Default, clean tree): TriangleTree 289->255ms (-12%),\n  CompleteBinaryTree 337->281ms (-16%), TrivialForest/DegenerateTree 4M within\n  noise; allocation neutral, still zero per-node heap allocation. Timing\n  variance also dropped (StdDev roughly halved on the dense trees).\n\nCo-Authored-By: Claude Opus 4.8 (1M context) <noreply@anthropic.com>\nClaude-Session: https://claude.ai/code/session_01Wg3xArL4FATQaXQMBvhXdg",
          "timestamp": "2026-06-26T21:37:21Z",
          "tree_id": "05aa22f3461078c05e96518d52da4d56655c250f",
          "url": "https://github.com/jasonmcboyd/Arborist/commit/81b46c42702e71ad313d08c636eb3e3c3c35b140"
        },
        "date": 1782511429441,
        "tool": "customSmallerIsBetter",
        "benches": [
          {
            "name": "Arborist.Benchmarks.RefSemiDeque.Add_8M",
            "value": 32160779,
            "unit": "bytes"
          },
          {
            "name": "Arborist.Benchmarks.RefSemiDeque.RemoveFirst_8M",
            "value": 32160788,
            "unit": "bytes"
          },
          {
            "name": "Arborist.Benchmarks.RefSemiDeque.RemoveLast_8M",
            "value": 32160854,
            "unit": "bytes"
          },
          {
            "name": "Arborist.Benchmarks.RefSemiDeque.Add_Block64_1M",
            "value": 64249317,
            "unit": "bytes"
          }
        ]
      },
      {
        "commit": {
          "author": {
            "email": "jason.boyd.ce@gmail.com",
            "name": "Jason Boyd",
            "username": "jasonmcboyd"
          },
          "committer": {
            "email": "jason.boyd.ce@gmail.com",
            "name": "Jason Boyd",
            "username": "jasonmcboyd"
          },
          "distinct": true,
          "id": "ab94b3983058299be15f1b4d38b05b21509cb8ad",
          "message": "Encapsulate BFT state in BreadthFirstPath; mirror the DFT driver/state split\n\nMove the breadth-first engine's state -- the visit queue, the schedule stack,\nthe owed-return-visit carry, and the root sibling counter -- into a new\nBreadthFirstPath, leaving BreadthFirstTreenumerator a thin driver, mirroring the\ndepth-first DepthFirstPath split. The cohesive Frame (visit state + child\nenumerator) is kept -- the BFT keeps full state for every resident node anyway,\nso it costs no memory -- so allocation is unchanged from the cohesion engine.\n\nLike DepthFirstPath, BreadthFirstPath is \"sans-I/O\": it never pulls a child; it\nexposes the two active enumerators (the schedule-stack top and the visit-queue\nfront) by ref for the driver to advance. That isolates the engine's asynchronous\noperations to those seams, so a future async BFT can share this class and differ\nonly there. It is a mutable struct embedded as the driver's single _Path field\n(never copied; refs it returns point into the heap deques), keeping dense\ntraversal at the cohesion engine's speed. The two child-pull sites collapse into\none TryScheduleNextChildOf(ref parent).\n\nNo behavior change: same (Mode, Node, VisitCount, Position) visit stream.\n\nValidation:\n- Exact-order traversal + exhaustive DFT-vs-BFT multiset scan (438/0)\n- Full Arborist.Linq suite incl. Where (14,759/0)\n- Benchmarks (Release/Job.Default, same session): TriangleTree 194.5 vs cohesion\n  193.9 ms; CompleteBinaryTree 208.1 vs 213.0 ms (parity). Allocation identical\n  to the cohesion engine (same Frame).\n\nCo-Authored-By: Claude Opus 4.8 (1M context) <noreply@anthropic.com>\nClaude-Session: https://claude.ai/code/session_01Wg3xArL4FATQaXQMBvhXdg",
          "timestamp": "2026-06-27T00:09:54Z",
          "tree_id": "8703f7198030df75b83a7026b75dc74f78b17190",
          "url": "https://github.com/jasonmcboyd/Arborist/commit/ab94b3983058299be15f1b4d38b05b21509cb8ad"
        },
        "date": 1782521636961,
        "tool": "customSmallerIsBetter",
        "benches": [
          {
            "name": "Arborist.Benchmarks.RefSemiDeque.Add_8M",
            "value": 32160799,
            "unit": "bytes"
          },
          {
            "name": "Arborist.Benchmarks.RefSemiDeque.RemoveFirst_8M",
            "value": 32160791,
            "unit": "bytes"
          },
          {
            "name": "Arborist.Benchmarks.RefSemiDeque.RemoveLast_8M",
            "value": 32160686,
            "unit": "bytes"
          },
          {
            "name": "Arborist.Benchmarks.RefSemiDeque.Add_Block64_1M",
            "value": 64249204,
            "unit": "bytes"
          }
        ]
      },
      {
        "commit": {
          "author": {
            "email": "jason.boyd.ce@gmail.com",
            "name": "Jason Boyd",
            "username": "jasonmcboyd"
          },
          "committer": {
            "email": "jason.boyd.ce@gmail.com",
            "name": "Jason Boyd",
            "username": "jasonmcboyd"
          },
          "distinct": true,
          "id": "6ebd5d0e1d60592672f966eaa4ab81a302c56999",
          "message": "Fix DFT skip-heavy regression: keep TryPushNextChild out-of-line\n\nThe encapsulated DepthFirstPath DFT (14f8393) ran ~1.7-1.9x slower than the\noriginal two-stack on promotion-heavy skip traversal (SkipAllNodes / Preorder /\nPostorder on wide trees), despite identical allocation. JIT disassembly\n(DOTNET_JitDisasm) pinned the cause: [AggressiveInlining] on TryPushNextChild\ninlined the entire promote body (pull + push) into OnMoveNext/OnScheduling,\ninflating their frames to 6 callee-saved registers + sub rsp,72 + vzeroupper and\ndowngrading OnMoveNext's branch dispatch from a tail-jmp to a call+teardown paid\non EVERY node. The original two-stack stayed fast precisely because its promote\nwas a separate, out-of-line method.\n\nMark TryPushNextChild [NoInlining] so the drivers stay thin tail-dispatchers,\nwhile keeping the push chain (PushChild/PushLevel) force-inlined INTO it so the\npush itself is still call-free. Also fold Backtrack's pop + three predicate\nchecks into one DepthFirstPath.PopFinishedLevelAndClassify call: the original\ninline-predicate form is O(1) per level but its repeated struct round-trips cost\n~2x on the deep-unwind path (GetLeaves.DeepTree); folding restores it.\n\nNet (Release/Job.Default, local, vs original two-stack): SkipAllNodes.Dft\n41 -> 22.6 ms, Preorder 42.8 -> 25.7, Postorder 47 -> 32.9 -- all back at the\noriginal; dense traversal and GetLeaves.DeepTree within noise; allocation\nunchanged. The sans-I/O encapsulation (path never pulls; one ref seam) is intact.\n\nValidation: 438/0 oracle (exact-order + exhaustive DFT-vs-BFT scan), 14,759/0\nfull Linq suite.\n\nCo-Authored-By: Claude Opus 4.8 (1M context) <noreply@anthropic.com>\nClaude-Session: https://claude.ai/code/session_01Wg3xArL4FATQaXQMBvhXdg",
          "timestamp": "2026-06-27T02:26:54Z",
          "tree_id": "5301780378953d2f586361d71d9e7e7b8dc1e6d3",
          "url": "https://github.com/jasonmcboyd/Arborist/commit/6ebd5d0e1d60592672f966eaa4ab81a302c56999"
        },
        "date": 1782529881198,
        "tool": "customSmallerIsBetter",
        "benches": [
          {
            "name": "Arborist.Benchmarks.RefSemiDeque.Add_8M",
            "value": 32160826,
            "unit": "bytes"
          },
          {
            "name": "Arborist.Benchmarks.RefSemiDeque.RemoveFirst_8M",
            "value": 32160791,
            "unit": "bytes"
          },
          {
            "name": "Arborist.Benchmarks.RefSemiDeque.RemoveLast_8M",
            "value": 32160793,
            "unit": "bytes"
          },
          {
            "name": "Arborist.Benchmarks.RefSemiDeque.Add_Block64_1M",
            "value": 64249185,
            "unit": "bytes"
          }
        ]
      }
    ],
    "Serialization Benchmarks": [
      {
        "commit": {
          "author": {
            "email": "jason.boyd.ce@gmail.com",
            "name": "Jason Boyd",
            "username": "jasonmcboyd"
          },
          "committer": {
            "email": "jason.boyd.ce@gmail.com",
            "name": "Jason Boyd",
            "username": "jasonmcboyd"
          },
          "distinct": true,
          "id": "5993e5762d3c0ffa1bbacbb40b35307cff9a3f87",
          "message": "Docs: document BFT Where O(N) prefix-carry design\n\nRewrite the WhereBreadthFirstTreenumerator section to match the implementation (queue + incremental _PredSkipPrefix, predicate-skip vs consumer-SkipNode, the real parent-visit fields, front-anchored sibling index). Update the DFT-vs-BFT table (BFT now O(N)) and fix stale _ExtraParentVisitsEmitted/_LastRemovedSkippedNodePosition/_SkippedStack references.\n\nCo-Authored-By: Claude Opus 4.8 (1M context) <noreply@anthropic.com>\nClaude-Session: https://claude.ai/code/session_01Wg3xArL4FATQaXQMBvhXdg",
          "timestamp": "2026-06-23T04:33:54Z",
          "tree_id": "a2615b3ce8e91df5baffd5f8f08e3c6ac9df2e3b",
          "url": "https://github.com/jasonmcboyd/Arborist/commit/5993e5762d3c0ffa1bbacbb40b35307cff9a3f87"
        },
        "date": 1782190984822,
        "tool": "benchmarkdotnet",
        "benches": [
          {
            "name": "Arborist.Benchmarks.Serialization.Serialize_Wide_1M",
            "value": 96309076.57142858,
            "unit": "ns",
            "range": "± 499417.1936913687"
          },
          {
            "name": "Arborist.Benchmarks.Serialization.Serialize_Deep_100K",
            "value": 14041141.992788462,
            "unit": "ns",
            "range": "± 102829.17190503677"
          },
          {
            "name": "Arborist.Benchmarks.Serialization.Deserialize_Wide_1M",
            "value": 105147658.47692308,
            "unit": "ns",
            "range": "± 482802.0083067216"
          },
          {
            "name": "Arborist.Benchmarks.Serialization.Deserialize_Deep_100K",
            "value": 36871060.395238094,
            "unit": "ns",
            "range": "± 233226.20077309402"
          },
          {
            "name": "Arborist.Benchmarks.Serialization.Deserialize_Wide_ToInt_StringMap",
            "value": 46779427.44155844,
            "unit": "ns",
            "range": "± 330357.09642540134"
          },
          {
            "name": "Arborist.Benchmarks.Serialization.Deserialize_Wide_ToInt_SpanMap",
            "value": 25772004.015625,
            "unit": "ns",
            "range": "± 133502.5122178053"
          }
        ]
      },
      {
        "commit": {
          "author": {
            "email": "jason.boyd.ce@gmail.com",
            "name": "Jason Boyd",
            "username": "jasonmcboyd"
          },
          "committer": {
            "email": "jason.boyd.ce@gmail.com",
            "name": "Jason Boyd",
            "username": "jasonmcboyd"
          },
          "distinct": true,
          "id": "c3ab5ef05fc7c7304bb98cd14e97bed3a8f91e62",
          "message": "Align LeaffixAggregate with LeaffixScan (flat, lazy, zero per-node alloc)\n\nLeaffixAggregate now uses the same flat forward DFS + no-copy ChildAccumulations view as LeaffixScan, yielding each root's accumulated value. Accumulator changes TAccumulate[] -> ChildAccumulations<TAccumulate>. Per-root lazy: a root is emitted the moment its subtree completes and the buffers are reused for the next root, so peak memory is the largest root subtree (not the whole forest) and early-terminating consumers traverse fewer roots -- matching the previous LeaffixAggregator behavior. Zero per-node heap allocation. Adds LeaffixAggregateTests (it previously had none and no callers).\n\nLeaffixAggregator is retained -- still used by Invert (TreeInverter); it'll be removed when Invert is redesigned onto a flat structure.\n\nCo-Authored-By: Claude Opus 4.8 (1M context) <noreply@anthropic.com>\nClaude-Session: https://claude.ai/code/session_01Wg3xArL4FATQaXQMBvhXdg",
          "timestamp": "2026-06-23T21:57:19Z",
          "tree_id": "7dacc032f0faefb81f1e004514e835a60b5bf81a",
          "url": "https://github.com/jasonmcboyd/Arborist/commit/c3ab5ef05fc7c7304bb98cd14e97bed3a8f91e62"
        },
        "date": 1782253174282,
        "tool": "benchmarkdotnet",
        "benches": [
          {
            "name": "Arborist.Benchmarks.Serialization.Serialize_Wide_1M",
            "value": 54180103.60666667,
            "unit": "ns",
            "range": "± 181564.93945820563"
          },
          {
            "name": "Arborist.Benchmarks.Serialization.Serialize_Deep_100K",
            "value": 9007627.263392856,
            "unit": "ns",
            "range": "± 67804.25189079349"
          },
          {
            "name": "Arborist.Benchmarks.Serialization.Deserialize_Wide_1M",
            "value": 85085046,
            "unit": "ns",
            "range": "± 466593.89929605083"
          },
          {
            "name": "Arborist.Benchmarks.Serialization.Deserialize_Deep_100K",
            "value": 10253018.090820312,
            "unit": "ns",
            "range": "± 193513.63174871227"
          },
          {
            "name": "Arborist.Benchmarks.Serialization.Deserialize_Wide_ToInt_StringMap",
            "value": 43635703.17187501,
            "unit": "ns",
            "range": "± 814285.0553764924"
          },
          {
            "name": "Arborist.Benchmarks.Serialization.Deserialize_Wide_ToInt_SpanMap",
            "value": 23882518.49107143,
            "unit": "ns",
            "range": "± 138584.07336015263"
          }
        ]
      },
      {
        "commit": {
          "author": {
            "email": "jason.boyd.ce@gmail.com",
            "name": "Jason Boyd",
            "username": "jasonmcboyd"
          },
          "committer": {
            "email": "jason.boyd.ce@gmail.com",
            "name": "Jason Boyd",
            "username": "jasonmcboyd"
          },
          "distinct": true,
          "id": "96f3cc44e2208f47258c2887c2a011530d708864",
          "message": "Rewrite Invert flat; retire SimpleNode and LeaffixAggregator\n\nInvert now materializes the source into flat pre-order arrays and emits the mirror with a stack -- pushing roots/children in forward order so they pop in reverse (the mirror's pre-order). Subtree sizes are invariant under mirroring; result is a PreorderTree. O(N), zero per-node allocation (was a SimpleNode object per node via LeaffixAggregator).\n\nInvert was the last user of both SimpleNode and LeaffixAggregator, so this deletes five files: TreeInverter, LeaffixAggregator (+NodeContextAndResult), SimpleNode, SimpleNodeChildEnumerator, SimpleNodeTreenumerable. The SimpleNode retirement is complete, clearing the runway for the nodeToValueMap elimination. (A level-order/LOUDS mirror -- reverse each child-run in place -- remains a future refinement if a LevelOrderTree is ever built.)\n\nCo-Authored-By: Claude Opus 4.8 (1M context) <noreply@anthropic.com>\nClaude-Session: https://claude.ai/code/session_01Wg3xArL4FATQaXQMBvhXdg",
          "timestamp": "2026-06-23T22:21:16Z",
          "tree_id": "aafd024b9cc4740d5e6c4fe63bc7cf95158cbb5e",
          "url": "https://github.com/jasonmcboyd/Arborist/commit/96f3cc44e2208f47258c2887c2a011530d708864"
        },
        "date": 1782254668648,
        "tool": "benchmarkdotnet",
        "benches": [
          {
            "name": "Arborist.Benchmarks.Serialization.Serialize_Wide_1M",
            "value": 52626835.557142854,
            "unit": "ns",
            "range": "± 311495.7808148656"
          },
          {
            "name": "Arborist.Benchmarks.Serialization.Serialize_Deep_100K",
            "value": 7600805.435416667,
            "unit": "ns",
            "range": "± 35630.674812277044"
          },
          {
            "name": "Arborist.Benchmarks.Serialization.Deserialize_Wide_1M",
            "value": 88476575.73611109,
            "unit": "ns",
            "range": "± 715454.1504625806"
          },
          {
            "name": "Arborist.Benchmarks.Serialization.Deserialize_Deep_100K",
            "value": 8422532.638221154,
            "unit": "ns",
            "range": "± 72379.20809009434"
          },
          {
            "name": "Arborist.Benchmarks.Serialization.Deserialize_Wide_ToInt_StringMap",
            "value": 41978506.02197802,
            "unit": "ns",
            "range": "± 327857.1147989337"
          },
          {
            "name": "Arborist.Benchmarks.Serialization.Deserialize_Wide_ToInt_SpanMap",
            "value": 22505473.42410714,
            "unit": "ns",
            "range": "± 73539.0897777294"
          }
        ]
      },
      {
        "commit": {
          "author": {
            "email": "jason.boyd.ce@gmail.com",
            "name": "Jason Boyd",
            "username": "jasonmcboyd"
          },
          "committer": {
            "email": "jason.boyd.ce@gmail.com",
            "name": "Jason Boyd",
            "username": "jasonmcboyd"
          },
          "distinct": true,
          "id": "7c795cd25f5b93fbac1993cf6a71485ef8c2de74",
          "message": "Add MemoryDiagnoser benchmark for Invert\n\nCovers TriangleTree depth-1448 (~1M) and a 1M-deep DegenerateTree, tagged LINQ for the dashboard. Completes benchmark coverage of the now-flat transform/aggregation ops (Materialize, LeaffixScan, LeaffixAggregate, Invert).\n\nCo-Authored-By: Claude Opus 4.8 (1M context) <noreply@anthropic.com>\nClaude-Session: https://claude.ai/code/session_01Wg3xArL4FATQaXQMBvhXdg",
          "timestamp": "2026-06-23T22:22:47Z",
          "tree_id": "547984a2584ebd974bf3f4a1790b68fc9b328cfc",
          "url": "https://github.com/jasonmcboyd/Arborist/commit/7c795cd25f5b93fbac1993cf6a71485ef8c2de74"
        },
        "date": 1782256079130,
        "tool": "benchmarkdotnet",
        "benches": [
          {
            "name": "Arborist.Benchmarks.Serialization.Serialize_Wide_1M",
            "value": 57834860.08888889,
            "unit": "ns",
            "range": "± 218936.5498409"
          },
          {
            "name": "Arborist.Benchmarks.Serialization.Serialize_Deep_100K",
            "value": 8628452.651442308,
            "unit": "ns",
            "range": "± 65394.37633313503"
          },
          {
            "name": "Arborist.Benchmarks.Serialization.Deserialize_Wide_1M",
            "value": 86495501.02380954,
            "unit": "ns",
            "range": "± 664872.7265829525"
          },
          {
            "name": "Arborist.Benchmarks.Serialization.Deserialize_Deep_100K",
            "value": 10891130.74375,
            "unit": "ns",
            "range": "± 190412.00160489746"
          },
          {
            "name": "Arborist.Benchmarks.Serialization.Deserialize_Wide_ToInt_StringMap",
            "value": 39927358.862637356,
            "unit": "ns",
            "range": "± 173280.9881377964"
          },
          {
            "name": "Arborist.Benchmarks.Serialization.Deserialize_Wide_ToInt_SpanMap",
            "value": 24707852.889583334,
            "unit": "ns",
            "range": "± 170561.64815042607"
          }
        ]
      },
      {
        "commit": {
          "author": {
            "email": "jason.boyd.ce@gmail.com",
            "name": "Jason Boyd",
            "username": "jasonmcboyd"
          },
          "committer": {
            "email": "jason.boyd.ce@gmail.com",
            "name": "Jason Boyd",
            "username": "jasonmcboyd"
          },
          "distinct": true,
          "id": "cf2c06864e8fdc1e6c24df1f1437f0dae1da26b0",
          "message": "Add 2-param Treenumerable base; drop redundant identity maps (map cleanup stage 1)\n\nThe sample trees whose node IS their surfaced value (TriangleTree, CollatzTree, CompleteBinaryTree, NDecrementTree, DeepTree) were each passing a redundant 'node => node' nodeToValueMap and carrying a duplicate type parameter. Add a 2-param Treenumerable<TNode, TChildEnumerator> convenience base (identity map) and migrate them to it. Public surfaces unchanged. The 3-param base + map remain for PreorderTree's genuine index->value dereference.\n\nCo-Authored-By: Claude Opus 4.8 (1M context) <noreply@anthropic.com>\nClaude-Session: https://claude.ai/code/session_01Wg3xArL4FATQaXQMBvhXdg",
          "timestamp": "2026-06-23T22:35:56Z",
          "tree_id": "005c51f9d7f00116b4e04d1cdb2a5fd0680fc86e",
          "url": "https://github.com/jasonmcboyd/Arborist/commit/cf2c06864e8fdc1e6c24df1f1437f0dae1da26b0"
        },
        "date": 1782257438305,
        "tool": "benchmarkdotnet",
        "benches": [
          {
            "name": "Arborist.Benchmarks.Serialization.Serialize_Wide_1M",
            "value": 50509021.821428575,
            "unit": "ns",
            "range": "± 126410.02142034033"
          },
          {
            "name": "Arborist.Benchmarks.Serialization.Serialize_Deep_100K",
            "value": 8051387.878605769,
            "unit": "ns",
            "range": "± 37277.03571789875"
          },
          {
            "name": "Arborist.Benchmarks.Serialization.Deserialize_Wide_1M",
            "value": 88039473.025641,
            "unit": "ns",
            "range": "± 707074.6565377567"
          },
          {
            "name": "Arborist.Benchmarks.Serialization.Deserialize_Deep_100K",
            "value": 8475822.016666668,
            "unit": "ns",
            "range": "± 65360.29120500822"
          },
          {
            "name": "Arborist.Benchmarks.Serialization.Deserialize_Wide_ToInt_StringMap",
            "value": 42046276.45555556,
            "unit": "ns",
            "range": "± 264044.75766859227"
          },
          {
            "name": "Arborist.Benchmarks.Serialization.Deserialize_Wide_ToInt_SpanMap",
            "value": 22357840.47767857,
            "unit": "ns",
            "range": "± 77722.94101120695"
          }
        ]
      },
      {
        "commit": {
          "author": {
            "email": "jason.boyd.ce@gmail.com",
            "name": "Jason Boyd",
            "username": "jasonmcboyd"
          },
          "committer": {
            "email": "jason.boyd.ce@gmail.com",
            "name": "Jason Boyd",
            "username": "jasonmcboyd"
          },
          "distinct": true,
          "id": "ca567e0f439d6d1f0f5c2143add771a6c13fd41c",
          "message": "Fix BFT Where O(depth) memory regression: tail-carry skip prefix\n\nf7eae61's O(N)-time prefix carry stored _PredSkipPrefix as a List<int>\nindexed by absolute inner depth, grown to the current depth on every\nscheduled node -- so a 1M-deep degenerate Where(_=>true) chain allocated\n~8.39 MB even though nothing is predicate-skipped (every entry 0).\n\nReplace it with a tail-carry: the prefix is monotonic non-decreasing in\ndepth and constant (= total skips on the path) beyond the deepest skipped\nancestor, so store only up to the deepest skip and serve reads past the\nend from a scalar tail. New _PrefixStored + _PrefixStoredCount +\n_PrefixTail; PrefixRead/PrefixWriteScheduled (no-op when value == tail, so\nzero allocation in the accepted region); PrefixAnchor truncates the stored\ncount to frontDepth-1 on front-advance.\n\nWhereAll 8.39 MB -> ~1.9 KB (O(1) in depth); WhereNone byte-identical\n(inherent O(depth) preserved). Visit stream byte-identical: validated\nagainst Where2InProcessScan (full c..i, 891k cases) + WhereTests (218/0),\nnet48 + net8.0 clean. Add WhereBreadthFirstAllocationTests as a hard\nmemory-bound regression guard (the gh-pages benchmark only soft-alerts).\n\nCo-Authored-By: Claude Opus 4.8 (1M context) <noreply@anthropic.com>\nClaude-Session: https://claude.ai/code/session_01Wg3xArL4FATQaXQMBvhXdg",
          "timestamp": "2026-06-25T18:13:44Z",
          "tree_id": "6d5c2256d2c8574537a4127f73a8de04fe0b4011",
          "url": "https://github.com/jasonmcboyd/Arborist/commit/ca567e0f439d6d1f0f5c2143add771a6c13fd41c"
        },
        "date": 1782413420880,
        "tool": "benchmarkdotnet",
        "benches": [
          {
            "name": "Arborist.Benchmarks.Serialization.Serialize_Wide_1M",
            "value": 55075226.157142855,
            "unit": "ns",
            "range": "± 125801.33912622063"
          },
          {
            "name": "Arborist.Benchmarks.Serialization.Serialize_Deep_100K",
            "value": 8990333.81138393,
            "unit": "ns",
            "range": "± 101313.44312883371"
          },
          {
            "name": "Arborist.Benchmarks.Serialization.Deserialize_Wide_1M",
            "value": 84420121.79761906,
            "unit": "ns",
            "range": "± 374683.10021590436"
          },
          {
            "name": "Arborist.Benchmarks.Serialization.Deserialize_Deep_100K",
            "value": 10178453.19485294,
            "unit": "ns",
            "range": "± 197710.42967945713"
          },
          {
            "name": "Arborist.Benchmarks.Serialization.Deserialize_Wide_ToInt_StringMap",
            "value": 42466854.96000001,
            "unit": "ns",
            "range": "± 340901.6474137874"
          },
          {
            "name": "Arborist.Benchmarks.Serialization.Deserialize_Wide_ToInt_SpanMap",
            "value": 23887439.239583332,
            "unit": "ns",
            "range": "± 152742.80977921298"
          }
        ]
      },
      {
        "commit": {
          "author": {
            "email": "jason.boyd.ce@gmail.com",
            "name": "Jason Boyd",
            "username": "jasonmcboyd"
          },
          "committer": {
            "email": "jason.boyd.ce@gmail.com",
            "name": "Jason Boyd",
            "username": "jasonmcboyd"
          },
          "distinct": true,
          "id": "6a5d01f5e6181c1c89a0993a6360bab3a4b0bac7",
          "message": "Rewrite BreadthFirstTreenumerator with a structural visit cadence\n\nThe base BFT engine emitted parent visits reactively (when a child was\naccepted), which broke whenever a child was filtered and forced a tangle\nof deferred parent-visit bookkeeping to recover the swallowed visits.\n\nReplace it with a structural cadence: a single FIFO _VisitQueue whose\nfront is the active parent, plus a LIFO _ScheduleStack for the SkipNode\npromotion descent. A parent is visited once when it reaches the front,\nthen once after every child slot that enqueues at least one accepted\nnode -- a single bool, _CurrentSlotEnqueuedNode. Roots are scheduled\nfirst as the children of an implicit no-visit forest sentinel.\n\nThis deletes _OwesPromotedParentVisit, _HasDeferredScheduledChild,\n_DepthOfLastActedOnNode, PayOwedParentVisitAndDeferChild, and the\nOnScheduling/OnVisiting/PromoteChildren/SkipSubtree/Backtrack web in\nfavor of Advance/ApplyStrategy/SkipRemainingSiblings. The now-unused\nOwesPromotedParentVisit field comes off the shared InternalNodeVisitState\nstruct, shrinking every deque slot.\n\nNo behavior change: same (Mode, Node, VisitCount, Position) visit stream.\n\nValidation:\n- Exhaustive BFT-vs-DFT oracle, up to 6 concurrent skips x 27 trees\n- Where2InProcessScan: 891,056 Where-wrapper-vs-oracle cases (groups c..i)\n- Curated exact-order traversal + 14,759 Where/allocation tests\n- Benchmarks: allocations -12% to -14%, time within ShortRun noise\n\nCo-Authored-By: Claude Opus 4.8 (1M context) <noreply@anthropic.com>\nClaude-Session: https://claude.ai/code/session_01Wg3xArL4FATQaXQMBvhXdg",
          "timestamp": "2026-06-26T00:47:42Z",
          "tree_id": "39f01ca28e49bf3bbc704caca59e8d4bb815b334",
          "url": "https://github.com/jasonmcboyd/Arborist/commit/6a5d01f5e6181c1c89a0993a6360bab3a4b0bac7"
        },
        "date": 1782436871988,
        "tool": "benchmarkdotnet",
        "benches": [
          {
            "name": "Arborist.Benchmarks.Serialization.Serialize_Wide_1M",
            "value": 53265714.20666668,
            "unit": "ns",
            "range": "± 321468.6699096187"
          },
          {
            "name": "Arborist.Benchmarks.Serialization.Serialize_Deep_100K",
            "value": 8967331.246875,
            "unit": "ns",
            "range": "± 80105.7299710597"
          },
          {
            "name": "Arborist.Benchmarks.Serialization.Deserialize_Wide_1M",
            "value": 88005538.31111112,
            "unit": "ns",
            "range": "± 555189.8299242326"
          },
          {
            "name": "Arborist.Benchmarks.Serialization.Deserialize_Deep_100K",
            "value": 10673493.48515625,
            "unit": "ns",
            "range": "± 236074.3360800768"
          },
          {
            "name": "Arborist.Benchmarks.Serialization.Deserialize_Wide_ToInt_StringMap",
            "value": 43172357.10555556,
            "unit": "ns",
            "range": "± 469190.71018271585"
          },
          {
            "name": "Arborist.Benchmarks.Serialization.Deserialize_Wide_ToInt_SpanMap",
            "value": 24480667.28348214,
            "unit": "ns",
            "range": "± 377479.1412320001"
          }
        ]
      },
      {
        "commit": {
          "author": {
            "email": "jason.boyd.ce@gmail.com",
            "name": "Jason Boyd",
            "username": "jasonmcboyd"
          },
          "committer": {
            "email": "jason.boyd.ce@gmail.com",
            "name": "Jason Boyd",
            "username": "jasonmcboyd"
          },
          "distinct": true,
          "id": "ce22f8e1055cf2b7bf6486f1da78d2058d8c69aa",
          "message": "Cap RefSemiDeque partition size to bound LOH allocation and overshoot\n\nGeometric partition growth sized each new partition to the running total\nCapacity, so the largest partition reached ~half the deque's peak element\ncount -- a multi-MB Large Object Heap allocation on wide/deep trees, plus\nup to ~2x peak over-allocation at power-of-two boundaries. Cap partition\nsize at 4096 elements (Math.Min(Capacity, MaxPartitionSize)) to bound both.\n\nBFT CompleteBinaryTree_21 (peak frontier ~2^21, the worst-case boundary):\n96 MB -> 48 MB allocated per traversal, throughput unchanged.\n\nFixed element count rather than a byte budget that would force partitions\nsub-LOH: forcing a 64 B node's partitions sub-LOH measured ~40% slower with\n~7x the Gen0 collections, because large long-lived blocks belong on the LOH.\n\nAdd RefSemiDeque regression tests crossing the cap (heterogeneous-partition\nordering, out-of-order recycling, GetFromBack/RemoveLast across boundaries)\nand an Add_Block64_1M benchmark. Remove unused InitialCapacity.\n\nCo-Authored-By: Claude Opus 4.8 (1M context) <noreply@anthropic.com>",
          "timestamp": "2026-06-26T04:13:38Z",
          "tree_id": "e35c9314063b1f6eda6dfccd1d7349907af73e32",
          "url": "https://github.com/jasonmcboyd/Arborist/commit/ce22f8e1055cf2b7bf6486f1da78d2058d8c69aa"
        },
        "date": 1782449421769,
        "tool": "benchmarkdotnet",
        "benches": [
          {
            "name": "Arborist.Benchmarks.Serialization.Serialize_Wide_1M",
            "value": 53257613.800000004,
            "unit": "ns",
            "range": "± 151967.51485185063"
          },
          {
            "name": "Arborist.Benchmarks.Serialization.Serialize_Deep_100K",
            "value": 7440135.095052083,
            "unit": "ns",
            "range": "± 48545.81211426212"
          },
          {
            "name": "Arborist.Benchmarks.Serialization.Deserialize_Wide_1M",
            "value": 87286477.1923077,
            "unit": "ns",
            "range": "± 624753.1088275225"
          },
          {
            "name": "Arborist.Benchmarks.Serialization.Deserialize_Deep_100K",
            "value": 10526969.905729167,
            "unit": "ns",
            "range": "± 147070.12165818506"
          },
          {
            "name": "Arborist.Benchmarks.Serialization.Deserialize_Wide_ToInt_StringMap",
            "value": 41847228.49743589,
            "unit": "ns",
            "range": "± 271604.12371858855"
          },
          {
            "name": "Arborist.Benchmarks.Serialization.Deserialize_Wide_ToInt_SpanMap",
            "value": 23859821.355769232,
            "unit": "ns",
            "range": "± 165824.13303448504"
          }
        ]
      },
      {
        "commit": {
          "author": {
            "email": "jason.boyd.ce@gmail.com",
            "name": "Jason Boyd",
            "username": "jasonmcboyd"
          },
          "committer": {
            "email": "jason.boyd.ce@gmail.com",
            "name": "Jason Boyd",
            "username": "jasonmcboyd"
          },
          "distinct": true,
          "id": "81b46c42702e71ad313d08c636eb3e3c3c35b140",
          "message": "Cohere BreadthFirstTreenumerator's four deques into one frame stack\n\nThe BFT kept each node's visit state and its child enumerator in FOUR parallel\nRefSemiDeques -- a state deque and an enumerator deque for each of the visit\nqueue and the schedule stack -- relying on keeping each pair in lockstep.\n\nFold each pair into one RefSemiDeque<Frame>, where Frame bundles\n{Node, Position, VisitCount, ChildEnumerator}, driven only by ref so the\nenumerator is never copied. Accepting a node becomes a single whole-frame move\nfrom the schedule stack to the visit queue, which structurally prevents a node\nand its enumerator from desynchronizing. The algorithm and visit cadence are\nunchanged. Mirrors the depth-first engine's frame stack, and removes the\nnow-unreferenced shared InternalNodeVisitState struct.\n\nNo behavior change: same (Mode, Node, VisitCount, Position) visit stream;\ndisposal (including the deliberate idempotent double-dispose on\nSkipDescendants/SkipSiblings) is at exact parity with the original.\n\nValidation:\n- Exact-order traversal + exhaustive DFT-vs-BFT multiset scan (438/0)\n- Full Arborist.Linq suite incl. Where (14,759/0)\n- Benchmarks (Release/Job.Default, clean tree): TriangleTree 289->255ms (-12%),\n  CompleteBinaryTree 337->281ms (-16%), TrivialForest/DegenerateTree 4M within\n  noise; allocation neutral, still zero per-node heap allocation. Timing\n  variance also dropped (StdDev roughly halved on the dense trees).\n\nCo-Authored-By: Claude Opus 4.8 (1M context) <noreply@anthropic.com>\nClaude-Session: https://claude.ai/code/session_01Wg3xArL4FATQaXQMBvhXdg",
          "timestamp": "2026-06-26T21:37:21Z",
          "tree_id": "05aa22f3461078c05e96518d52da4d56655c250f",
          "url": "https://github.com/jasonmcboyd/Arborist/commit/81b46c42702e71ad313d08c636eb3e3c3c35b140"
        },
        "date": 1782511427335,
        "tool": "benchmarkdotnet",
        "benches": [
          {
            "name": "Arborist.Benchmarks.Serialization.Serialize_Wide_1M",
            "value": 74121696.425,
            "unit": "ns",
            "range": "± 294684.11352785205"
          },
          {
            "name": "Arborist.Benchmarks.Serialization.Serialize_Deep_100K",
            "value": 9026740.429166667,
            "unit": "ns",
            "range": "± 62460.70353676081"
          },
          {
            "name": "Arborist.Benchmarks.Serialization.Deserialize_Wide_1M",
            "value": 88546943.2,
            "unit": "ns",
            "range": "± 1126266.4610441031"
          },
          {
            "name": "Arborist.Benchmarks.Serialization.Deserialize_Deep_100K",
            "value": 10388031.65736607,
            "unit": "ns",
            "range": "± 158680.5957777884"
          },
          {
            "name": "Arborist.Benchmarks.Serialization.Deserialize_Wide_ToInt_StringMap",
            "value": 43073269.32051282,
            "unit": "ns",
            "range": "± 293721.99161473743"
          },
          {
            "name": "Arborist.Benchmarks.Serialization.Deserialize_Wide_ToInt_SpanMap",
            "value": 24128328.614583332,
            "unit": "ns",
            "range": "± 135220.71780010196"
          }
        ]
      },
      {
        "commit": {
          "author": {
            "email": "jason.boyd.ce@gmail.com",
            "name": "Jason Boyd",
            "username": "jasonmcboyd"
          },
          "committer": {
            "email": "jason.boyd.ce@gmail.com",
            "name": "Jason Boyd",
            "username": "jasonmcboyd"
          },
          "distinct": true,
          "id": "ab94b3983058299be15f1b4d38b05b21509cb8ad",
          "message": "Encapsulate BFT state in BreadthFirstPath; mirror the DFT driver/state split\n\nMove the breadth-first engine's state -- the visit queue, the schedule stack,\nthe owed-return-visit carry, and the root sibling counter -- into a new\nBreadthFirstPath, leaving BreadthFirstTreenumerator a thin driver, mirroring the\ndepth-first DepthFirstPath split. The cohesive Frame (visit state + child\nenumerator) is kept -- the BFT keeps full state for every resident node anyway,\nso it costs no memory -- so allocation is unchanged from the cohesion engine.\n\nLike DepthFirstPath, BreadthFirstPath is \"sans-I/O\": it never pulls a child; it\nexposes the two active enumerators (the schedule-stack top and the visit-queue\nfront) by ref for the driver to advance. That isolates the engine's asynchronous\noperations to those seams, so a future async BFT can share this class and differ\nonly there. It is a mutable struct embedded as the driver's single _Path field\n(never copied; refs it returns point into the heap deques), keeping dense\ntraversal at the cohesion engine's speed. The two child-pull sites collapse into\none TryScheduleNextChildOf(ref parent).\n\nNo behavior change: same (Mode, Node, VisitCount, Position) visit stream.\n\nValidation:\n- Exact-order traversal + exhaustive DFT-vs-BFT multiset scan (438/0)\n- Full Arborist.Linq suite incl. Where (14,759/0)\n- Benchmarks (Release/Job.Default, same session): TriangleTree 194.5 vs cohesion\n  193.9 ms; CompleteBinaryTree 208.1 vs 213.0 ms (parity). Allocation identical\n  to the cohesion engine (same Frame).\n\nCo-Authored-By: Claude Opus 4.8 (1M context) <noreply@anthropic.com>\nClaude-Session: https://claude.ai/code/session_01Wg3xArL4FATQaXQMBvhXdg",
          "timestamp": "2026-06-27T00:09:54Z",
          "tree_id": "8703f7198030df75b83a7026b75dc74f78b17190",
          "url": "https://github.com/jasonmcboyd/Arborist/commit/ab94b3983058299be15f1b4d38b05b21509cb8ad"
        },
        "date": 1782521636020,
        "tool": "benchmarkdotnet",
        "benches": [
          {
            "name": "Arborist.Benchmarks.Serialization.Serialize_Wide_1M",
            "value": 57238278.903703704,
            "unit": "ns",
            "range": "± 262692.79970520164"
          },
          {
            "name": "Arborist.Benchmarks.Serialization.Serialize_Deep_100K",
            "value": 7900494.033333333,
            "unit": "ns",
            "range": "± 87578.85570411733"
          },
          {
            "name": "Arborist.Benchmarks.Serialization.Deserialize_Wide_1M",
            "value": 86632644.41666666,
            "unit": "ns",
            "range": "± 467877.55514507345"
          },
          {
            "name": "Arborist.Benchmarks.Serialization.Deserialize_Deep_100K",
            "value": 10484904.008854168,
            "unit": "ns",
            "range": "± 174803.90735131313"
          },
          {
            "name": "Arborist.Benchmarks.Serialization.Deserialize_Wide_ToInt_StringMap",
            "value": 43835992.63636363,
            "unit": "ns",
            "range": "± 1073717.140314893"
          },
          {
            "name": "Arborist.Benchmarks.Serialization.Deserialize_Wide_ToInt_SpanMap",
            "value": 23943285.979166668,
            "unit": "ns",
            "range": "± 284331.5914295763"
          }
        ]
      },
      {
        "commit": {
          "author": {
            "email": "jason.boyd.ce@gmail.com",
            "name": "Jason Boyd",
            "username": "jasonmcboyd"
          },
          "committer": {
            "email": "jason.boyd.ce@gmail.com",
            "name": "Jason Boyd",
            "username": "jasonmcboyd"
          },
          "distinct": true,
          "id": "6ebd5d0e1d60592672f966eaa4ab81a302c56999",
          "message": "Fix DFT skip-heavy regression: keep TryPushNextChild out-of-line\n\nThe encapsulated DepthFirstPath DFT (14f8393) ran ~1.7-1.9x slower than the\noriginal two-stack on promotion-heavy skip traversal (SkipAllNodes / Preorder /\nPostorder on wide trees), despite identical allocation. JIT disassembly\n(DOTNET_JitDisasm) pinned the cause: [AggressiveInlining] on TryPushNextChild\ninlined the entire promote body (pull + push) into OnMoveNext/OnScheduling,\ninflating their frames to 6 callee-saved registers + sub rsp,72 + vzeroupper and\ndowngrading OnMoveNext's branch dispatch from a tail-jmp to a call+teardown paid\non EVERY node. The original two-stack stayed fast precisely because its promote\nwas a separate, out-of-line method.\n\nMark TryPushNextChild [NoInlining] so the drivers stay thin tail-dispatchers,\nwhile keeping the push chain (PushChild/PushLevel) force-inlined INTO it so the\npush itself is still call-free. Also fold Backtrack's pop + three predicate\nchecks into one DepthFirstPath.PopFinishedLevelAndClassify call: the original\ninline-predicate form is O(1) per level but its repeated struct round-trips cost\n~2x on the deep-unwind path (GetLeaves.DeepTree); folding restores it.\n\nNet (Release/Job.Default, local, vs original two-stack): SkipAllNodes.Dft\n41 -> 22.6 ms, Preorder 42.8 -> 25.7, Postorder 47 -> 32.9 -- all back at the\noriginal; dense traversal and GetLeaves.DeepTree within noise; allocation\nunchanged. The sans-I/O encapsulation (path never pulls; one ref seam) is intact.\n\nValidation: 438/0 oracle (exact-order + exhaustive DFT-vs-BFT scan), 14,759/0\nfull Linq suite.\n\nCo-Authored-By: Claude Opus 4.8 (1M context) <noreply@anthropic.com>\nClaude-Session: https://claude.ai/code/session_01Wg3xArL4FATQaXQMBvhXdg",
          "timestamp": "2026-06-27T02:26:54Z",
          "tree_id": "5301780378953d2f586361d71d9e7e7b8dc1e6d3",
          "url": "https://github.com/jasonmcboyd/Arborist/commit/6ebd5d0e1d60592672f966eaa4ab81a302c56999"
        },
        "date": 1782529880343,
        "tool": "benchmarkdotnet",
        "benches": [
          {
            "name": "Arborist.Benchmarks.Serialization.Serialize_Wide_1M",
            "value": 56077121.15873016,
            "unit": "ns",
            "range": "± 190422.428325226"
          },
          {
            "name": "Arborist.Benchmarks.Serialization.Serialize_Deep_100K",
            "value": 7460765.993990385,
            "unit": "ns",
            "range": "± 16685.438090156906"
          },
          {
            "name": "Arborist.Benchmarks.Serialization.Deserialize_Wide_1M",
            "value": 86719979.25641026,
            "unit": "ns",
            "range": "± 560286.7710029094"
          },
          {
            "name": "Arborist.Benchmarks.Serialization.Deserialize_Deep_100K",
            "value": 10320752.419270834,
            "unit": "ns",
            "range": "± 134456.37518404445"
          },
          {
            "name": "Arborist.Benchmarks.Serialization.Deserialize_Wide_ToInt_StringMap",
            "value": 40723824.00666667,
            "unit": "ns",
            "range": "± 326658.76093941653"
          },
          {
            "name": "Arborist.Benchmarks.Serialization.Deserialize_Wide_ToInt_SpanMap",
            "value": 23588163.835416667,
            "unit": "ns",
            "range": "± 310607.37429593847"
          }
        ]
      },
      {
        "commit": {
          "author": {
            "email": "jason.boyd.ce@gmail.com",
            "name": "Jason Boyd",
            "username": "jasonmcboyd"
          },
          "committer": {
            "email": "jason.boyd.ce@gmail.com",
            "name": "Jason Boyd",
            "username": "jasonmcboyd"
          },
          "distinct": true,
          "id": "e8bbc30ca40332e6f77c535d2a6e5edf419feb67",
          "message": "Revert DFT Backtrack consolidation (GetLeaves.DeepTree deep-unwind cost)\n\n6ebd5d0 fixed the wide-skip-traversal regression by making TryPushNextChild\nout-of-line, but it also folded Backtrack's pop + three predicate checks into one\nDepthFirstPath.PopFinishedLevelAndClassify call. That consolidation, fine on wide\ntrees, added one call per unwound level -- ~262K calls on the deep-unwind path --\nand regressed GetLeaves.DeepTree from ~10ms to ~24ms on the CI runner (a cost\nlocal benchmarks under-reported, due to cache differences).\n\nRevert the consolidation back to the original two-stack's inline-predicate\nBacktrack, keeping the out-of-line TryPushNextChild that fixed wide skip. The DFT\nis now structurally identical to the original two-stack (inline Backtrack +\nseparate promote method), just encapsulated in DepthFirstPath -- the form the CI\nshows is fast on every tree shape.\n\nNet (vs original two-stack, local same-session): SkipAllNodes.Dft 22.7 vs 22.0;\nGetLeaves.DeepTree 10.7 vs 8.8 (the residual +22% is the out-of-line promote's\nper-node call -- the irreducible cost of keeping wide skip fast; CI will confirm\nGetLeaves is well below 6ebd5d0's 24ms). Allocation unchanged.\n\nValidation: 438/0 oracle (exact-order + exhaustive DFT-vs-BFT scan), 14,759/0 Linq.\n\nCo-Authored-By: Claude Opus 4.8 (1M context) <noreply@anthropic.com>\nClaude-Session: https://claude.ai/code/session_01Wg3xArL4FATQaXQMBvhXdg",
          "timestamp": "2026-06-27T03:58:38Z",
          "tree_id": "a3f9dd64f7a624f48c68d521c40c1a096d4fa152",
          "url": "https://github.com/jasonmcboyd/Arborist/commit/e8bbc30ca40332e6f77c535d2a6e5edf419feb67"
        },
        "date": 1782534426222,
        "tool": "benchmarkdotnet",
        "benches": [
          {
            "name": "Arborist.Benchmarks.Serialization.Serialize_Wide_1M",
            "value": 56409195.14814814,
            "unit": "ns",
            "range": "± 489190.6527341422"
          },
          {
            "name": "Arborist.Benchmarks.Serialization.Serialize_Deep_100K",
            "value": 7513473.872767857,
            "unit": "ns",
            "range": "± 74642.60774948799"
          },
          {
            "name": "Arborist.Benchmarks.Serialization.Deserialize_Wide_1M",
            "value": 85388289.03333333,
            "unit": "ns",
            "range": "± 254405.88350269283"
          },
          {
            "name": "Arborist.Benchmarks.Serialization.Deserialize_Deep_100K",
            "value": 10179239.745833334,
            "unit": "ns",
            "range": "± 104855.89609132308"
          },
          {
            "name": "Arborist.Benchmarks.Serialization.Deserialize_Wide_ToInt_StringMap",
            "value": 42266333.85416667,
            "unit": "ns",
            "range": "± 220031.66289552444"
          },
          {
            "name": "Arborist.Benchmarks.Serialization.Deserialize_Wide_ToInt_SpanMap",
            "value": 23775936.66294643,
            "unit": "ns",
            "range": "± 191045.69787183683"
          }
        ]
      }
    ],
    "Serialization Memory": [
      {
        "commit": {
          "author": {
            "email": "jason.boyd.ce@gmail.com",
            "name": "Jason Boyd",
            "username": "jasonmcboyd"
          },
          "committer": {
            "email": "jason.boyd.ce@gmail.com",
            "name": "Jason Boyd",
            "username": "jasonmcboyd"
          },
          "distinct": true,
          "id": "5993e5762d3c0ffa1bbacbb40b35307cff9a3f87",
          "message": "Docs: document BFT Where O(N) prefix-carry design\n\nRewrite the WhereBreadthFirstTreenumerator section to match the implementation (queue + incremental _PredSkipPrefix, predicate-skip vs consumer-SkipNode, the real parent-visit fields, front-anchored sibling index). Update the DFT-vs-BFT table (BFT now O(N)) and fix stale _ExtraParentVisitsEmitted/_LastRemovedSkippedNodePosition/_SkippedStack references.\n\nCo-Authored-By: Claude Opus 4.8 (1M context) <noreply@anthropic.com>\nClaude-Session: https://claude.ai/code/session_01Wg3xArL4FATQaXQMBvhXdg",
          "timestamp": "2026-06-23T04:33:54Z",
          "tree_id": "a2615b3ce8e91df5baffd5f8f08e3c6ac9df2e3b",
          "url": "https://github.com/jasonmcboyd/Arborist/commit/5993e5762d3c0ffa1bbacbb40b35307cff9a3f87"
        },
        "date": 1782190986443,
        "tool": "customSmallerIsBetter",
        "benches": [
          {
            "name": "Arborist.Benchmarks.Serialization.Serialize_Wide_1M",
            "value": 27633883,
            "unit": "bytes"
          },
          {
            "name": "Arborist.Benchmarks.Serialization.Serialize_Deep_100K",
            "value": 9072156,
            "unit": "bytes"
          },
          {
            "name": "Arborist.Benchmarks.Serialization.Deserialize_Wide_1M",
            "value": 72755555,
            "unit": "bytes"
          },
          {
            "name": "Arborist.Benchmarks.Serialization.Deserialize_Deep_100K",
            "value": 21297755,
            "unit": "bytes"
          },
          {
            "name": "Arborist.Benchmarks.Serialization.Deserialize_Wide_ToInt_StringMap",
            "value": 72755425,
            "unit": "bytes"
          },
          {
            "name": "Arborist.Benchmarks.Serialization.Deserialize_Wide_ToInt_SpanMap",
            "value": 33555264,
            "unit": "bytes"
          }
        ]
      },
      {
        "commit": {
          "author": {
            "email": "jason.boyd.ce@gmail.com",
            "name": "Jason Boyd",
            "username": "jasonmcboyd"
          },
          "committer": {
            "email": "jason.boyd.ce@gmail.com",
            "name": "Jason Boyd",
            "username": "jasonmcboyd"
          },
          "distinct": true,
          "id": "c3ab5ef05fc7c7304bb98cd14e97bed3a8f91e62",
          "message": "Align LeaffixAggregate with LeaffixScan (flat, lazy, zero per-node alloc)\n\nLeaffixAggregate now uses the same flat forward DFS + no-copy ChildAccumulations view as LeaffixScan, yielding each root's accumulated value. Accumulator changes TAccumulate[] -> ChildAccumulations<TAccumulate>. Per-root lazy: a root is emitted the moment its subtree completes and the buffers are reused for the next root, so peak memory is the largest root subtree (not the whole forest) and early-terminating consumers traverse fewer roots -- matching the previous LeaffixAggregator behavior. Zero per-node heap allocation. Adds LeaffixAggregateTests (it previously had none and no callers).\n\nLeaffixAggregator is retained -- still used by Invert (TreeInverter); it'll be removed when Invert is redesigned onto a flat structure.\n\nCo-Authored-By: Claude Opus 4.8 (1M context) <noreply@anthropic.com>\nClaude-Session: https://claude.ai/code/session_01Wg3xArL4FATQaXQMBvhXdg",
          "timestamp": "2026-06-23T21:57:19Z",
          "tree_id": "7dacc032f0faefb81f1e004514e835a60b5bf81a",
          "url": "https://github.com/jasonmcboyd/Arborist/commit/c3ab5ef05fc7c7304bb98cd14e97bed3a8f91e62"
        },
        "date": 1782253175207,
        "tool": "customSmallerIsBetter",
        "benches": [
          {
            "name": "Arborist.Benchmarks.Serialization.Serialize_Wide_1M",
            "value": 27634636,
            "unit": "bytes"
          },
          {
            "name": "Arborist.Benchmarks.Serialization.Serialize_Deep_100K",
            "value": 8547816,
            "unit": "bytes"
          },
          {
            "name": "Arborist.Benchmarks.Serialization.Deserialize_Wide_1M",
            "value": 76368560,
            "unit": "bytes"
          },
          {
            "name": "Arborist.Benchmarks.Serialization.Deserialize_Deep_100K",
            "value": 8595960,
            "unit": "bytes"
          },
          {
            "name": "Arborist.Benchmarks.Serialization.Deserialize_Wide_ToInt_StringMap",
            "value": 63978883,
            "unit": "bytes"
          },
          {
            "name": "Arborist.Benchmarks.Serialization.Deserialize_Wide_ToInt_SpanMap",
            "value": 24779816,
            "unit": "bytes"
          }
        ]
      },
      {
        "commit": {
          "author": {
            "email": "jason.boyd.ce@gmail.com",
            "name": "Jason Boyd",
            "username": "jasonmcboyd"
          },
          "committer": {
            "email": "jason.boyd.ce@gmail.com",
            "name": "Jason Boyd",
            "username": "jasonmcboyd"
          },
          "distinct": true,
          "id": "96f3cc44e2208f47258c2887c2a011530d708864",
          "message": "Rewrite Invert flat; retire SimpleNode and LeaffixAggregator\n\nInvert now materializes the source into flat pre-order arrays and emits the mirror with a stack -- pushing roots/children in forward order so they pop in reverse (the mirror's pre-order). Subtree sizes are invariant under mirroring; result is a PreorderTree. O(N), zero per-node allocation (was a SimpleNode object per node via LeaffixAggregator).\n\nInvert was the last user of both SimpleNode and LeaffixAggregator, so this deletes five files: TreeInverter, LeaffixAggregator (+NodeContextAndResult), SimpleNode, SimpleNodeChildEnumerator, SimpleNodeTreenumerable. The SimpleNode retirement is complete, clearing the runway for the nodeToValueMap elimination. (A level-order/LOUDS mirror -- reverse each child-run in place -- remains a future refinement if a LevelOrderTree is ever built.)\n\nCo-Authored-By: Claude Opus 4.8 (1M context) <noreply@anthropic.com>\nClaude-Session: https://claude.ai/code/session_01Wg3xArL4FATQaXQMBvhXdg",
          "timestamp": "2026-06-23T22:21:16Z",
          "tree_id": "aafd024b9cc4740d5e6c4fe63bc7cf95158cbb5e",
          "url": "https://github.com/jasonmcboyd/Arborist/commit/96f3cc44e2208f47258c2887c2a011530d708864"
        },
        "date": 1782254669472,
        "tool": "customSmallerIsBetter",
        "benches": [
          {
            "name": "Arborist.Benchmarks.Serialization.Serialize_Wide_1M",
            "value": 27633821,
            "unit": "bytes"
          },
          {
            "name": "Arborist.Benchmarks.Serialization.Serialize_Deep_100K",
            "value": 8547610,
            "unit": "bytes"
          },
          {
            "name": "Arborist.Benchmarks.Serialization.Deserialize_Wide_1M",
            "value": 76367515,
            "unit": "bytes"
          },
          {
            "name": "Arborist.Benchmarks.Serialization.Deserialize_Deep_100K",
            "value": 8595986,
            "unit": "bytes"
          },
          {
            "name": "Arborist.Benchmarks.Serialization.Deserialize_Wide_ToInt_StringMap",
            "value": 63978847,
            "unit": "bytes"
          },
          {
            "name": "Arborist.Benchmarks.Serialization.Deserialize_Wide_ToInt_SpanMap",
            "value": 24779670,
            "unit": "bytes"
          }
        ]
      },
      {
        "commit": {
          "author": {
            "email": "jason.boyd.ce@gmail.com",
            "name": "Jason Boyd",
            "username": "jasonmcboyd"
          },
          "committer": {
            "email": "jason.boyd.ce@gmail.com",
            "name": "Jason Boyd",
            "username": "jasonmcboyd"
          },
          "distinct": true,
          "id": "7c795cd25f5b93fbac1993cf6a71485ef8c2de74",
          "message": "Add MemoryDiagnoser benchmark for Invert\n\nCovers TriangleTree depth-1448 (~1M) and a 1M-deep DegenerateTree, tagged LINQ for the dashboard. Completes benchmark coverage of the now-flat transform/aggregation ops (Materialize, LeaffixScan, LeaffixAggregate, Invert).\n\nCo-Authored-By: Claude Opus 4.8 (1M context) <noreply@anthropic.com>\nClaude-Session: https://claude.ai/code/session_01Wg3xArL4FATQaXQMBvhXdg",
          "timestamp": "2026-06-23T22:22:47Z",
          "tree_id": "547984a2584ebd974bf3f4a1790b68fc9b328cfc",
          "url": "https://github.com/jasonmcboyd/Arborist/commit/7c795cd25f5b93fbac1993cf6a71485ef8c2de74"
        },
        "date": 1782256080113,
        "tool": "customSmallerIsBetter",
        "benches": [
          {
            "name": "Arborist.Benchmarks.Serialization.Serialize_Wide_1M",
            "value": 27634742,
            "unit": "bytes"
          },
          {
            "name": "Arborist.Benchmarks.Serialization.Serialize_Deep_100K",
            "value": 8547821,
            "unit": "bytes"
          },
          {
            "name": "Arborist.Benchmarks.Serialization.Deserialize_Wide_1M",
            "value": 76368556,
            "unit": "bytes"
          },
          {
            "name": "Arborist.Benchmarks.Serialization.Deserialize_Deep_100K",
            "value": 8595850,
            "unit": "bytes"
          },
          {
            "name": "Arborist.Benchmarks.Serialization.Deserialize_Wide_ToInt_StringMap",
            "value": 63978854,
            "unit": "bytes"
          },
          {
            "name": "Arborist.Benchmarks.Serialization.Deserialize_Wide_ToInt_SpanMap",
            "value": 24781231,
            "unit": "bytes"
          }
        ]
      },
      {
        "commit": {
          "author": {
            "email": "jason.boyd.ce@gmail.com",
            "name": "Jason Boyd",
            "username": "jasonmcboyd"
          },
          "committer": {
            "email": "jason.boyd.ce@gmail.com",
            "name": "Jason Boyd",
            "username": "jasonmcboyd"
          },
          "distinct": true,
          "id": "cf2c06864e8fdc1e6c24df1f1437f0dae1da26b0",
          "message": "Add 2-param Treenumerable base; drop redundant identity maps (map cleanup stage 1)\n\nThe sample trees whose node IS their surfaced value (TriangleTree, CollatzTree, CompleteBinaryTree, NDecrementTree, DeepTree) were each passing a redundant 'node => node' nodeToValueMap and carrying a duplicate type parameter. Add a 2-param Treenumerable<TNode, TChildEnumerator> convenience base (identity map) and migrate them to it. Public surfaces unchanged. The 3-param base + map remain for PreorderTree's genuine index->value dereference.\n\nCo-Authored-By: Claude Opus 4.8 (1M context) <noreply@anthropic.com>\nClaude-Session: https://claude.ai/code/session_01Wg3xArL4FATQaXQMBvhXdg",
          "timestamp": "2026-06-23T22:35:56Z",
          "tree_id": "005c51f9d7f00116b4e04d1cdb2a5fd0680fc86e",
          "url": "https://github.com/jasonmcboyd/Arborist/commit/cf2c06864e8fdc1e6c24df1f1437f0dae1da26b0"
        },
        "date": 1782257439120,
        "tool": "customSmallerIsBetter",
        "benches": [
          {
            "name": "Arborist.Benchmarks.Serialization.Serialize_Wide_1M",
            "value": 27633821,
            "unit": "bytes"
          },
          {
            "name": "Arborist.Benchmarks.Serialization.Serialize_Deep_100K",
            "value": 8547423,
            "unit": "bytes"
          },
          {
            "name": "Arborist.Benchmarks.Serialization.Deserialize_Wide_1M",
            "value": 76367515,
            "unit": "bytes"
          },
          {
            "name": "Arborist.Benchmarks.Serialization.Deserialize_Deep_100K",
            "value": 8596020,
            "unit": "bytes"
          },
          {
            "name": "Arborist.Benchmarks.Serialization.Deserialize_Wide_ToInt_StringMap",
            "value": 63978823,
            "unit": "bytes"
          },
          {
            "name": "Arborist.Benchmarks.Serialization.Deserialize_Wide_ToInt_SpanMap",
            "value": 24778770,
            "unit": "bytes"
          }
        ]
      },
      {
        "commit": {
          "author": {
            "email": "jason.boyd.ce@gmail.com",
            "name": "Jason Boyd",
            "username": "jasonmcboyd"
          },
          "committer": {
            "email": "jason.boyd.ce@gmail.com",
            "name": "Jason Boyd",
            "username": "jasonmcboyd"
          },
          "distinct": true,
          "id": "ca567e0f439d6d1f0f5c2143add771a6c13fd41c",
          "message": "Fix BFT Where O(depth) memory regression: tail-carry skip prefix\n\nf7eae61's O(N)-time prefix carry stored _PredSkipPrefix as a List<int>\nindexed by absolute inner depth, grown to the current depth on every\nscheduled node -- so a 1M-deep degenerate Where(_=>true) chain allocated\n~8.39 MB even though nothing is predicate-skipped (every entry 0).\n\nReplace it with a tail-carry: the prefix is monotonic non-decreasing in\ndepth and constant (= total skips on the path) beyond the deepest skipped\nancestor, so store only up to the deepest skip and serve reads past the\nend from a scalar tail. New _PrefixStored + _PrefixStoredCount +\n_PrefixTail; PrefixRead/PrefixWriteScheduled (no-op when value == tail, so\nzero allocation in the accepted region); PrefixAnchor truncates the stored\ncount to frontDepth-1 on front-advance.\n\nWhereAll 8.39 MB -> ~1.9 KB (O(1) in depth); WhereNone byte-identical\n(inherent O(depth) preserved). Visit stream byte-identical: validated\nagainst Where2InProcessScan (full c..i, 891k cases) + WhereTests (218/0),\nnet48 + net8.0 clean. Add WhereBreadthFirstAllocationTests as a hard\nmemory-bound regression guard (the gh-pages benchmark only soft-alerts).\n\nCo-Authored-By: Claude Opus 4.8 (1M context) <noreply@anthropic.com>\nClaude-Session: https://claude.ai/code/session_01Wg3xArL4FATQaXQMBvhXdg",
          "timestamp": "2026-06-25T18:13:44Z",
          "tree_id": "6d5c2256d2c8574537a4127f73a8de04fe0b4011",
          "url": "https://github.com/jasonmcboyd/Arborist/commit/ca567e0f439d6d1f0f5c2143add771a6c13fd41c"
        },
        "date": 1782413421862,
        "tool": "customSmallerIsBetter",
        "benches": [
          {
            "name": "Arborist.Benchmarks.Serialization.Serialize_Wide_1M",
            "value": 27634636,
            "unit": "bytes"
          },
          {
            "name": "Arborist.Benchmarks.Serialization.Serialize_Deep_100K",
            "value": 8547815,
            "unit": "bytes"
          },
          {
            "name": "Arborist.Benchmarks.Serialization.Deserialize_Wide_1M",
            "value": 76368557,
            "unit": "bytes"
          },
          {
            "name": "Arborist.Benchmarks.Serialization.Deserialize_Deep_100K",
            "value": 8595974,
            "unit": "bytes"
          },
          {
            "name": "Arborist.Benchmarks.Serialization.Deserialize_Wide_ToInt_StringMap",
            "value": 63978831,
            "unit": "bytes"
          },
          {
            "name": "Arborist.Benchmarks.Serialization.Deserialize_Wide_ToInt_SpanMap",
            "value": 24781280,
            "unit": "bytes"
          }
        ]
      },
      {
        "commit": {
          "author": {
            "email": "jason.boyd.ce@gmail.com",
            "name": "Jason Boyd",
            "username": "jasonmcboyd"
          },
          "committer": {
            "email": "jason.boyd.ce@gmail.com",
            "name": "Jason Boyd",
            "username": "jasonmcboyd"
          },
          "distinct": true,
          "id": "6a5d01f5e6181c1c89a0993a6360bab3a4b0bac7",
          "message": "Rewrite BreadthFirstTreenumerator with a structural visit cadence\n\nThe base BFT engine emitted parent visits reactively (when a child was\naccepted), which broke whenever a child was filtered and forced a tangle\nof deferred parent-visit bookkeeping to recover the swallowed visits.\n\nReplace it with a structural cadence: a single FIFO _VisitQueue whose\nfront is the active parent, plus a LIFO _ScheduleStack for the SkipNode\npromotion descent. A parent is visited once when it reaches the front,\nthen once after every child slot that enqueues at least one accepted\nnode -- a single bool, _CurrentSlotEnqueuedNode. Roots are scheduled\nfirst as the children of an implicit no-visit forest sentinel.\n\nThis deletes _OwesPromotedParentVisit, _HasDeferredScheduledChild,\n_DepthOfLastActedOnNode, PayOwedParentVisitAndDeferChild, and the\nOnScheduling/OnVisiting/PromoteChildren/SkipSubtree/Backtrack web in\nfavor of Advance/ApplyStrategy/SkipRemainingSiblings. The now-unused\nOwesPromotedParentVisit field comes off the shared InternalNodeVisitState\nstruct, shrinking every deque slot.\n\nNo behavior change: same (Mode, Node, VisitCount, Position) visit stream.\n\nValidation:\n- Exhaustive BFT-vs-DFT oracle, up to 6 concurrent skips x 27 trees\n- Where2InProcessScan: 891,056 Where-wrapper-vs-oracle cases (groups c..i)\n- Curated exact-order traversal + 14,759 Where/allocation tests\n- Benchmarks: allocations -12% to -14%, time within ShortRun noise\n\nCo-Authored-By: Claude Opus 4.8 (1M context) <noreply@anthropic.com>\nClaude-Session: https://claude.ai/code/session_01Wg3xArL4FATQaXQMBvhXdg",
          "timestamp": "2026-06-26T00:47:42Z",
          "tree_id": "39f01ca28e49bf3bbc704caca59e8d4bb815b334",
          "url": "https://github.com/jasonmcboyd/Arborist/commit/6a5d01f5e6181c1c89a0993a6360bab3a4b0bac7"
        },
        "date": 1782436873003,
        "tool": "customSmallerIsBetter",
        "benches": [
          {
            "name": "Arborist.Benchmarks.Serialization.Serialize_Wide_1M",
            "value": 27634604,
            "unit": "bytes"
          },
          {
            "name": "Arborist.Benchmarks.Serialization.Serialize_Deep_100K",
            "value": 8023398,
            "unit": "bytes"
          },
          {
            "name": "Arborist.Benchmarks.Serialization.Deserialize_Wide_1M",
            "value": 76368560,
            "unit": "bytes"
          },
          {
            "name": "Arborist.Benchmarks.Serialization.Deserialize_Deep_100K",
            "value": 8595960,
            "unit": "bytes"
          },
          {
            "name": "Arborist.Benchmarks.Serialization.Deserialize_Wide_ToInt_StringMap",
            "value": 63978831,
            "unit": "bytes"
          },
          {
            "name": "Arborist.Benchmarks.Serialization.Deserialize_Wide_ToInt_SpanMap",
            "value": 24780663,
            "unit": "bytes"
          }
        ]
      },
      {
        "commit": {
          "author": {
            "email": "jason.boyd.ce@gmail.com",
            "name": "Jason Boyd",
            "username": "jasonmcboyd"
          },
          "committer": {
            "email": "jason.boyd.ce@gmail.com",
            "name": "Jason Boyd",
            "username": "jasonmcboyd"
          },
          "distinct": true,
          "id": "ce22f8e1055cf2b7bf6486f1da78d2058d8c69aa",
          "message": "Cap RefSemiDeque partition size to bound LOH allocation and overshoot\n\nGeometric partition growth sized each new partition to the running total\nCapacity, so the largest partition reached ~half the deque's peak element\ncount -- a multi-MB Large Object Heap allocation on wide/deep trees, plus\nup to ~2x peak over-allocation at power-of-two boundaries. Cap partition\nsize at 4096 elements (Math.Min(Capacity, MaxPartitionSize)) to bound both.\n\nBFT CompleteBinaryTree_21 (peak frontier ~2^21, the worst-case boundary):\n96 MB -> 48 MB allocated per traversal, throughput unchanged.\n\nFixed element count rather than a byte budget that would force partitions\nsub-LOH: forcing a 64 B node's partitions sub-LOH measured ~40% slower with\n~7x the Gen0 collections, because large long-lived blocks belong on the LOH.\n\nAdd RefSemiDeque regression tests crossing the cap (heterogeneous-partition\nordering, out-of-order recycling, GetFromBack/RemoveLast across boundaries)\nand an Add_Block64_1M benchmark. Remove unused InitialCapacity.\n\nCo-Authored-By: Claude Opus 4.8 (1M context) <noreply@anthropic.com>",
          "timestamp": "2026-06-26T04:13:38Z",
          "tree_id": "e35c9314063b1f6eda6dfccd1d7349907af73e32",
          "url": "https://github.com/jasonmcboyd/Arborist/commit/ce22f8e1055cf2b7bf6486f1da78d2058d8c69aa"
        },
        "date": 1782449422814,
        "tool": "customSmallerIsBetter",
        "benches": [
          {
            "name": "Arborist.Benchmarks.Serialization.Serialize_Wide_1M",
            "value": 27634694,
            "unit": "bytes"
          },
          {
            "name": "Arborist.Benchmarks.Serialization.Serialize_Deep_100K",
            "value": 6878812,
            "unit": "bytes"
          },
          {
            "name": "Arborist.Benchmarks.Serialization.Deserialize_Wide_1M",
            "value": 76368557,
            "unit": "bytes"
          },
          {
            "name": "Arborist.Benchmarks.Serialization.Deserialize_Deep_100K",
            "value": 8595880,
            "unit": "bytes"
          },
          {
            "name": "Arborist.Benchmarks.Serialization.Deserialize_Wide_ToInt_StringMap",
            "value": 63978854,
            "unit": "bytes"
          },
          {
            "name": "Arborist.Benchmarks.Serialization.Deserialize_Wide_ToInt_SpanMap",
            "value": 24780886,
            "unit": "bytes"
          }
        ]
      },
      {
        "commit": {
          "author": {
            "email": "jason.boyd.ce@gmail.com",
            "name": "Jason Boyd",
            "username": "jasonmcboyd"
          },
          "committer": {
            "email": "jason.boyd.ce@gmail.com",
            "name": "Jason Boyd",
            "username": "jasonmcboyd"
          },
          "distinct": true,
          "id": "81b46c42702e71ad313d08c636eb3e3c3c35b140",
          "message": "Cohere BreadthFirstTreenumerator's four deques into one frame stack\n\nThe BFT kept each node's visit state and its child enumerator in FOUR parallel\nRefSemiDeques -- a state deque and an enumerator deque for each of the visit\nqueue and the schedule stack -- relying on keeping each pair in lockstep.\n\nFold each pair into one RefSemiDeque<Frame>, where Frame bundles\n{Node, Position, VisitCount, ChildEnumerator}, driven only by ref so the\nenumerator is never copied. Accepting a node becomes a single whole-frame move\nfrom the schedule stack to the visit queue, which structurally prevents a node\nand its enumerator from desynchronizing. The algorithm and visit cadence are\nunchanged. Mirrors the depth-first engine's frame stack, and removes the\nnow-unreferenced shared InternalNodeVisitState struct.\n\nNo behavior change: same (Mode, Node, VisitCount, Position) visit stream;\ndisposal (including the deliberate idempotent double-dispose on\nSkipDescendants/SkipSiblings) is at exact parity with the original.\n\nValidation:\n- Exact-order traversal + exhaustive DFT-vs-BFT multiset scan (438/0)\n- Full Arborist.Linq suite incl. Where (14,759/0)\n- Benchmarks (Release/Job.Default, clean tree): TriangleTree 289->255ms (-12%),\n  CompleteBinaryTree 337->281ms (-16%), TrivialForest/DegenerateTree 4M within\n  noise; allocation neutral, still zero per-node heap allocation. Timing\n  variance also dropped (StdDev roughly halved on the dense trees).\n\nCo-Authored-By: Claude Opus 4.8 (1M context) <noreply@anthropic.com>\nClaude-Session: https://claude.ai/code/session_01Wg3xArL4FATQaXQMBvhXdg",
          "timestamp": "2026-06-26T21:37:21Z",
          "tree_id": "05aa22f3461078c05e96518d52da4d56655c250f",
          "url": "https://github.com/jasonmcboyd/Arborist/commit/81b46c42702e71ad313d08c636eb3e3c3c35b140"
        },
        "date": 1782511429655,
        "tool": "customSmallerIsBetter",
        "benches": [
          {
            "name": "Arborist.Benchmarks.Serialization.Serialize_Wide_1M",
            "value": 27633620,
            "unit": "bytes"
          },
          {
            "name": "Arborist.Benchmarks.Serialization.Serialize_Deep_100K",
            "value": 7695871,
            "unit": "bytes"
          },
          {
            "name": "Arborist.Benchmarks.Serialization.Deserialize_Wide_1M",
            "value": 76368556,
            "unit": "bytes"
          },
          {
            "name": "Arborist.Benchmarks.Serialization.Deserialize_Deep_100K",
            "value": 8595856,
            "unit": "bytes"
          },
          {
            "name": "Arborist.Benchmarks.Serialization.Deserialize_Wide_ToInt_StringMap",
            "value": 63978831,
            "unit": "bytes"
          },
          {
            "name": "Arborist.Benchmarks.Serialization.Deserialize_Wide_ToInt_SpanMap",
            "value": 24780423,
            "unit": "bytes"
          }
        ]
      },
      {
        "commit": {
          "author": {
            "email": "jason.boyd.ce@gmail.com",
            "name": "Jason Boyd",
            "username": "jasonmcboyd"
          },
          "committer": {
            "email": "jason.boyd.ce@gmail.com",
            "name": "Jason Boyd",
            "username": "jasonmcboyd"
          },
          "distinct": true,
          "id": "ab94b3983058299be15f1b4d38b05b21509cb8ad",
          "message": "Encapsulate BFT state in BreadthFirstPath; mirror the DFT driver/state split\n\nMove the breadth-first engine's state -- the visit queue, the schedule stack,\nthe owed-return-visit carry, and the root sibling counter -- into a new\nBreadthFirstPath, leaving BreadthFirstTreenumerator a thin driver, mirroring the\ndepth-first DepthFirstPath split. The cohesive Frame (visit state + child\nenumerator) is kept -- the BFT keeps full state for every resident node anyway,\nso it costs no memory -- so allocation is unchanged from the cohesion engine.\n\nLike DepthFirstPath, BreadthFirstPath is \"sans-I/O\": it never pulls a child; it\nexposes the two active enumerators (the schedule-stack top and the visit-queue\nfront) by ref for the driver to advance. That isolates the engine's asynchronous\noperations to those seams, so a future async BFT can share this class and differ\nonly there. It is a mutable struct embedded as the driver's single _Path field\n(never copied; refs it returns point into the heap deques), keeping dense\ntraversal at the cohesion engine's speed. The two child-pull sites collapse into\none TryScheduleNextChildOf(ref parent).\n\nNo behavior change: same (Mode, Node, VisitCount, Position) visit stream.\n\nValidation:\n- Exact-order traversal + exhaustive DFT-vs-BFT multiset scan (438/0)\n- Full Arborist.Linq suite incl. Where (14,759/0)\n- Benchmarks (Release/Job.Default, same session): TriangleTree 194.5 vs cohesion\n  193.9 ms; CompleteBinaryTree 208.1 vs 213.0 ms (parity). Allocation identical\n  to the cohesion engine (same Frame).\n\nCo-Authored-By: Claude Opus 4.8 (1M context) <noreply@anthropic.com>\nClaude-Session: https://claude.ai/code/session_01Wg3xArL4FATQaXQMBvhXdg",
          "timestamp": "2026-06-27T00:09:54Z",
          "tree_id": "8703f7198030df75b83a7026b75dc74f78b17190",
          "url": "https://github.com/jasonmcboyd/Arborist/commit/ab94b3983058299be15f1b4d38b05b21509cb8ad"
        },
        "date": 1782521637189,
        "tool": "customSmallerIsBetter",
        "benches": [
          {
            "name": "Arborist.Benchmarks.Serialization.Serialize_Wide_1M",
            "value": 27634694,
            "unit": "bytes"
          },
          {
            "name": "Arborist.Benchmarks.Serialization.Serialize_Deep_100K",
            "value": 6878939,
            "unit": "bytes"
          },
          {
            "name": "Arborist.Benchmarks.Serialization.Deserialize_Wide_1M",
            "value": 76368560,
            "unit": "bytes"
          },
          {
            "name": "Arborist.Benchmarks.Serialization.Deserialize_Deep_100K",
            "value": 8595925,
            "unit": "bytes"
          },
          {
            "name": "Arborist.Benchmarks.Serialization.Deserialize_Wide_ToInt_StringMap",
            "value": 63978831,
            "unit": "bytes"
          },
          {
            "name": "Arborist.Benchmarks.Serialization.Deserialize_Wide_ToInt_SpanMap",
            "value": 24780419,
            "unit": "bytes"
          }
        ]
      },
      {
        "commit": {
          "author": {
            "email": "jason.boyd.ce@gmail.com",
            "name": "Jason Boyd",
            "username": "jasonmcboyd"
          },
          "committer": {
            "email": "jason.boyd.ce@gmail.com",
            "name": "Jason Boyd",
            "username": "jasonmcboyd"
          },
          "distinct": true,
          "id": "6ebd5d0e1d60592672f966eaa4ab81a302c56999",
          "message": "Fix DFT skip-heavy regression: keep TryPushNextChild out-of-line\n\nThe encapsulated DepthFirstPath DFT (14f8393) ran ~1.7-1.9x slower than the\noriginal two-stack on promotion-heavy skip traversal (SkipAllNodes / Preorder /\nPostorder on wide trees), despite identical allocation. JIT disassembly\n(DOTNET_JitDisasm) pinned the cause: [AggressiveInlining] on TryPushNextChild\ninlined the entire promote body (pull + push) into OnMoveNext/OnScheduling,\ninflating their frames to 6 callee-saved registers + sub rsp,72 + vzeroupper and\ndowngrading OnMoveNext's branch dispatch from a tail-jmp to a call+teardown paid\non EVERY node. The original two-stack stayed fast precisely because its promote\nwas a separate, out-of-line method.\n\nMark TryPushNextChild [NoInlining] so the drivers stay thin tail-dispatchers,\nwhile keeping the push chain (PushChild/PushLevel) force-inlined INTO it so the\npush itself is still call-free. Also fold Backtrack's pop + three predicate\nchecks into one DepthFirstPath.PopFinishedLevelAndClassify call: the original\ninline-predicate form is O(1) per level but its repeated struct round-trips cost\n~2x on the deep-unwind path (GetLeaves.DeepTree); folding restores it.\n\nNet (Release/Job.Default, local, vs original two-stack): SkipAllNodes.Dft\n41 -> 22.6 ms, Preorder 42.8 -> 25.7, Postorder 47 -> 32.9 -- all back at the\noriginal; dense traversal and GetLeaves.DeepTree within noise; allocation\nunchanged. The sans-I/O encapsulation (path never pulls; one ref seam) is intact.\n\nValidation: 438/0 oracle (exact-order + exhaustive DFT-vs-BFT scan), 14,759/0\nfull Linq suite.\n\nCo-Authored-By: Claude Opus 4.8 (1M context) <noreply@anthropic.com>\nClaude-Session: https://claude.ai/code/session_01Wg3xArL4FATQaXQMBvhXdg",
          "timestamp": "2026-06-27T02:26:54Z",
          "tree_id": "5301780378953d2f586361d71d9e7e7b8dc1e6d3",
          "url": "https://github.com/jasonmcboyd/Arborist/commit/6ebd5d0e1d60592672f966eaa4ab81a302c56999"
        },
        "date": 1782529881404,
        "tool": "customSmallerIsBetter",
        "benches": [
          {
            "name": "Arborist.Benchmarks.Serialization.Serialize_Wide_1M",
            "value": 27636065,
            "unit": "bytes"
          },
          {
            "name": "Arborist.Benchmarks.Serialization.Serialize_Deep_100K",
            "value": 6878934,
            "unit": "bytes"
          },
          {
            "name": "Arborist.Benchmarks.Serialization.Deserialize_Wide_1M",
            "value": 76368560,
            "unit": "bytes"
          },
          {
            "name": "Arborist.Benchmarks.Serialization.Deserialize_Deep_100K",
            "value": 8595909,
            "unit": "bytes"
          },
          {
            "name": "Arborist.Benchmarks.Serialization.Deserialize_Wide_ToInt_StringMap",
            "value": 63978831,
            "unit": "bytes"
          },
          {
            "name": "Arborist.Benchmarks.Serialization.Deserialize_Wide_ToInt_SpanMap",
            "value": 24780884,
            "unit": "bytes"
          }
        ]
      }
    ]
  }
}