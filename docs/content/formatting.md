# FsReveal - Formatting

FsReveal uses [Markdown](http://daringfireball.net/projects/markdown/syntax) as source for your slides and renders these as HTML pages.
Whenever the following markdown formatting tips don't work you can always use HTML.   
This page gives you an overview about cases.

## Slides

Slides are always started with a `***` marker:

    [lang=markdown]
    ***
  
  	### My first slide
  
  	* bullet point 1
  	* bullet point 2
  	* bullet point 3
  
  	***
  
  	### My second slide
  	#### A subtitle
  
  	* bullet point 1
  	* bullet point 2
  	* bullet point 3

<a href="samples/slide1" target="_blank"><img src="img/smalllogo.png" alt="See live demo"></a>

## Vertical slides

Slides can be nested inside of each other. Use `---` to indicate a nested slide:

    [lang=markdown]
    ***
  
  	### My first slide
  
  	* bullet point 1
  	* bullet point 2
  	* bullet point 3
  
  	---
  
  	### My first nested slide
  
  	* bullet point 1
  	* bullet point 2
  	* bullet point 3

<a href="samples/slide2" target="_blank"><img src="img/smalllogo.png" alt="See live demo"></a>

## Images

You can put images into the `slides/images` folder and use them with the following syntax:

    [lang=markdown]
    ***
  
  	### My first slide
  
  	![an alternative text](images/logo.png)

## Speaker notes

Speaker notes are prefixed with a `'` marker:

    [lang=markdown]
    ***
  
  	### My first slide
  
  	* bullet point 1
  	* bullet point 2
  	* bullet point 3
 
    ' this is a speaker note
	' and here we have another one

## Styling

Create a custom stylesheet in `slides/custom.css` and then you can use these CSS classes in your slides.

