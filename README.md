Eshopworld.Tests.Core
===================

Utility unit test package used in eShopWorld.

## Utility classes

#### EnumDifferences

Checks if 2 enums have the same elements with the same enum values.

```c#
public static IEnumerable<string> EnumDifferences<T1, T2>()
```

#### TestResource

Wraps an embed resource around a class that can materialize it to the file system and delete on the call to Dispose.

#### FluentAssertionExtensions

Contains CodeContract extensions for `FluentAssertions`, validates both pre and post contracts.

```c#
// Validates if a Pre-Code Contract (Requires) violation is thrown by the `Action` invocation.
public static ExceptionAssertions<Exception> ShouldThrowPreContract(this Action action, string because = null, params object[] reasonArgs)

// Validates if a Post-Code Contract (Ensures) violation is thrown by the `Action` invocation.
public static ExceptionAssertions<Exception> ShouldThrowPostContract(this Action action, string because = null, params object[] reasonArgs)
```

#### Lorem Generator

Contains a [Lorem Ipsum generator](http://www.lipsum.com/), especially useful in integration tests to generate anything that takes the form of a `string`

```c#
public static string GetWord()
public static IEnumerable<string> GetWords(int num = 3)
public static string GetSentence(int wordCount = 4)
public static IEnumerable<string> GetSentences(int sentenceCount = 3)
public static string GetParagraph(int sentenceCount = 3)
public static IEnumerable<string> GetParagraphs(int paragraphCount = 3)
```

## Trait builders

Contains a set of Is* trait attributes that aggregate specific trait categories, to facilitate control over build test filters:

- **IsUnit** - "Unit"
- **IsIntegration** - "Integration"
- **IsIntegrationReadOnly** - "Integration" + "ReadOnly"
- **IsIntegrationHealthCheck** - "Integration" + "ReadOnly" + "HealthCheck"
- **IsWarmUpAttribute** - "WarmUp"
- **IsAutomatedUiAttribute** - "AutomatedUi"
- **IsFakes** - "Unit" + "Fakes"
- **IsCodeContract** - "Unit" + "CodeContract"
- **IsRoslyn** - "Unit" + "Roslyn"
- **IsDev** - "Dev"
- **IsProfilerCpu** - "Profiler CPU"
- **IsProfilerMemory** - "Profiler Memory"
- **IsLayer0** - "Layer0"
- **IsLayer1** - "Layer1"
- **IsLayer2** - "Layer2"
- **IsLayer3** - "Layer3"
