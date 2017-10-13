<Query Kind="Program" />

// Sometimes it's useful to know the name of the property or method that
// calls another method.

// One way is to manually pass a string. That performs very fast but is error prone
// because the name may change over time.

// Another way is to use a StackTrace and traverse the frames. WHAT?!
// This is a technique that is extremely error-prone and slow performing.
// I won't even show an example, because it's a bad idea, unless there is no other way.

// A better way of doing this is by using the CallerMemberName attribute.
// It's as fast as hard-coded strings (because the compiler hard-codes the strings for you)
// but there's no chance of the name being wrong.

// How is this ever useful?
// Mainly you will use this for creating developer tools around logging, configuration,
// or dynamic invocation (reflection). 

// If you never need to do this, that's great. 
// But when you do, it's nice to know this method exists.

// Take a look at this simple GetCallerName() method:
static string GetCallerName([System.Runtime.CompilerServices.CallerMemberName] string callerName = null) =>
	callerName;
	
// There's a single string parameter that defaults to null.
// The name of the parameter doesn't matter at all. Change it to anything.
// The method itself does nothing more than return the string passed into it.

// How does it work?

// When you compile the code, the compiler sees the [CallerMemberName] attribute
// and it finds all the members that call it.
// For each member it finds, it transforms your code to pass the name of the member.

// Run the code to see this working.

void Main()
{
	GetCallerName().Dump();
	SomeMethod().Dump();
	SomeProperty.Dump();
	SomeField.Dump();
	this[123].Dump();
}

string SomeMethod() => GetCallerName();

string SomeProperty => GetCallerName();

static readonly string SomeField = GetCallerName();

string this[int i] => $"{ GetCallerName()} #{ i }";


