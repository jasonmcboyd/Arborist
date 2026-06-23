window.BENCHMARK_DATA = {
  "lastUpdate": 1782256078569,
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
      }
    ]
  }
}