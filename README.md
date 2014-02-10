HtmlSanitizer
=============
This is the code for a blog article on a quick and dirty HTML Sanitation routine in C# which is discussed here:

[.NET HTML Sanitation for rich HTML Input](http://weblog.west-wind.com/posts/2012/Jul/19/NET-HTML-Sanitation-for-rich-HTML-Input)

The idea is that in some applications it's necessary to capture HTML as part of user input, 
and that there's a need to sanitize the HTML input to prevent malicious code to fire. This
routine parses captured HTML input and strips elements and attributes that are considered
potentially malicious. 

This code uses a blacklist approach to blocking potentially malicious input HTML text posted
and you can modify the blacklist according to your needs.

This is not meant to be a bullet proof implementation, but rather a functional and extensible one
that you can tweak for you specific requirements.

For more detailed info please refer to the Blog Post which discusses the reasoning behind
this routine in more detail.
