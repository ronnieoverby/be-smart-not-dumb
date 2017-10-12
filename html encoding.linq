<Query Kind="Statements" />

// let's suppose a bad user of your web app is submitting some input:
var userInput = @"
<p>I am some happy HTML. Just minding my own business. Nothin' to see here....</p> 	

<!-- ... but little will they expect ... -->

<script>	
	destroyTheWorld();
	// my sinister plan is complete!
	
	
	function destroyTheWorld() {
		alert('everyone is now dead! thank you.');
	}
</script>
"
.Dump("The nefarious original input.");

// Oh no! There's HTML and a terrible life threatening script!
// if this script somehow gets executed, life as we know it will end
// what can be done?!

// HTML ENCODING TO THE RESCUE!
// Before this input ever has the chance of being rendered by a
// web browser, it should be HTML encoded. Here's how:

string safelyEncodedHtml = System.Net.WebUtility.HtmlEncode(userInput)
	.Dump("This encoded HTML is harmless!");

// now that the world has been delivered by HTML Encoding,
// we can safely render it in the browser.

// RUN THIS SCRIPT FOR A DEMONSTRATION!








/////////////////////////////////////////////////////////////////
// The code below is supporting code
// Feel free to read, but it's not relevant to the topic

new Hyperlinq(() => DemonstrateInWebBrowser(userInput), "Click here to experience the full wrath of the unencoded HTML").Dump();
new Hyperlinq(() => DemonstrateInWebBrowser(safelyEncodedHtml), "Click here to experience saving grace of the encoded HTML").Dump();

void DemonstrateInWebBrowser(string code)
{
	var htmlFile = Path.GetTempFileName() + ".html";

	var html = $@"
	<html>
		<head><title>{Util.CurrentQueryPath}</title></head>
		<body>
			{code}
		</body>
	</html>";
	
	File.WriteAllText(htmlFile, html);
	Process.Start(htmlFile);
}