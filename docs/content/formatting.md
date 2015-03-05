# FsReveal - Formatting

FsReveal uses [Markdown](http://daringfireball.net/projects/markdown/syntax) as source for your slides and renders these as HTML pages.
Whenever the following markdown formatting tips don't work you can always use HTML.

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

<a href="samples/simple-slides" target="_blank"><img src="img/smalllogo.png" alt="See live demo"></a> &nbsp;&nbsp;&nbsp; Open the <a href="samples/simple-slides" target="_blank">demo</a> and use the arrow keys to navigate through the slides.

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

<a href="samples/vertical-slides" target="_blank"><img src="img/smalllogo.png" alt="See live demo"></a> &nbsp;&nbsp;&nbsp;Open the <a href="samples/vertical-slides" target="_blank">demo</a> and use the <b>Space</b> key to navigate through all slides.

## Images

You can put images into the `slides/images` folder and use them with the following syntax:

    [lang=markdown]
    ***
  
  	### My first slide
  
  	![an alternative text](images/logo.png)

<a href="samples/slides-with-images" target="_blank"><img src="img/smalllogo.png" alt="See live demo"></a> &nbsp;&nbsp;&nbsp;Open the <a href="samples/slides-with-images" target="_blank">demo</a> and see how the image is rendered.

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

<a href="samples/speaker-notes" target="_blank"><img src="img/smalllogo.png" alt="See live demo"></a> &nbsp;&nbsp;&nbsp;Open the <a href="samples/speaker-notes" target="_blank">demo</a> and press the <b>s</b> key to see the speaker notes.

## Custom stylesheets

Create a custom stylesheet in `slides/custom.css` and then you can use these CSS classes in your slides.

## Slide properties

Slide properties allow you to customize things like the background of a slide:

    [lang=markdown]
	***
	- data-background : #441111
  
	### My first slide

	* bullet point 1
	* bullet point 2
	* bullet point 3

	***
	- data-background : images/smalllogo.png
	- data-background-repeat : repeat
	- data-background-size : 100px

	### Second slide

	* bullet point 1
	* bullet point 2
	* bullet point 3

<a href="samples/slide-properties" target="_blank"><img src="img/smalllogo.png" alt="See live demo"></a> &nbsp;&nbsp;&nbsp;Open the <a href="samples/slide-properties" target="_blank">demo</a> see the effect of the slide properties.