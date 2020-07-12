# Ellumination Combinatorics

_[![Ellumination.Combinatorics.Combinatorials NuGet version](https://img.shields.io/nuget/v/Ellumination.Combinatorics.Combinatorials.svg?style=flat&label=nuget%3A%20Ellumination.Combinatorics.Combinatorials)](https://www.nuget.org/packages/Ellumination.Combinatorics.Combinatorials)_
_[![Ellumination.Combinatorics.Permutations NuGet version](https://img.shields.io/nuget/v/Ellumination.Combinatorics.Permutations.svg?style=flat&label=nuget%3A%20Ellumination.Combinatorics.Permutations)](https://www.nuget.org/packages/Ellumination.Combinatorics.Permutations)_

Welcome to the [Combinatorics](https://en.wikipedia.org/wiki/Combinatorics) library. This solution provides a set of features helpful in the formation and discovery of Combinatorial or related combinations.

## Combiner

The [Combiner](https://github.com/mwpowellhtx/Ellumination.Combinatorics/src/Ellumination.Combinatorics.Combinatorial/Combiner.cs) is the key to forming and discovering combinations over a range of potentially different sized collections.

### Compared and Contrasted with Tally Counters

The concept is literally modeled after the *tally counter* wherein you would have some number of digits or bins, click a button or otherwise register a tally, sometimes even electro-magnetically, any more these days, and you would register a count of plus one or more. There is also opportunity for overflow from least significant bins into more significant bins.

In contrast, *Combiner* supports establishing bins of potentally different sizes, whereby the *digits* involved are rather the *indices* mapping across the range of values. Indexing the current state of these *indices* across the collections yields the *curent combination*.

I was going to illustrate a &quot;simple&quot; tally counter for comparison purposes, but it turns out there are many dozens of form factors these days, including electro-magnetic in nature, believe it or not, and some even completely digital. Suffice to say, the focus here is not on bins of digits, but rather, is entirely up to the consumer as to what elements ought to be combined.

### Convenience Operators

`CurrentCombination` is provided which yields the current `IEnumerable<object>` mapping across the participating collections.

After that we support convenience operators such as addition in terms of steps, as well as the increment operator, which is basically just addition plus one.

We also support the ability to iterate over the range of combinations as an `IEnumerable<object[]>`.

Helpful properties include both `Exhausted`, which reports whether the internal indices have reached their maximum capacity, and `Count`, which reports the total number of possible combinations given the participating collections.

### Empty Participating Collections

Technically, empty collections are supported. Language level `null` is reported for these bins. Some operations will report false negatives, such as `Count`, which will literally yield zero (0). You are better off depending on `Exhausted` in this case, although this is not full-proof at the moment, either, for certain edge cases.

### Overfow

*Combiner* supports overflow a bit awkwardly at the moment. Detecting this edge case needs to be differentiated from all indices having reached their maximum capacity. Otherwise, operation is fairly seamless.
