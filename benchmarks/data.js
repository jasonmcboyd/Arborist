window.BENCHMARK_DATA = {
  "lastUpdate": 1769975446276,
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
      }
    ]
  }
}