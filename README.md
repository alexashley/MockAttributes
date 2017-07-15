# MockAttributes
Mockito-like annotations for .NET

See [MovieFinderTest](https://github.com/alexashley/MockAttributes/blob/master/MockAttributes.Demo/MovieFinderInjectedPropertiesTest.cs) for a usage example


### WIP
- [x] inject mocks into test class
- [x] inject mocked objects into class-under-test (for `Moq` initially, although could be made more general)
- [x] stand-up class-under-test with mocked objects
- [ ] unit tests
- [x] suppress CS0649 ("Field is never assigned to, and will always have its default value null")
- [x] support properties
- [ ] handle classes with multiple dependencies of the same type
- [ ] fallback to property injection if no suitable constructors are found
- [ ] expand demo
