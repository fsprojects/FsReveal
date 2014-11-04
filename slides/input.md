- title : Digit Recognizer Dojo
- description : A Gentle Introduction to Machine Learning
- author : Steffen Forkmann
- theme : Night
- transition : default

***

## Digit Recognizer Dojo
### A Gentle Introduction to Machine Learning

***

### The Goal

- Take a Kaggle data science competition

- Write some code and have fun
- Write a classifier, from scratch, using F#
- Learn some Machine Learning concepts

***

### The format

- Brief introduction to the problem
- You code in teams, I help out

***

### Kaggle Digit Recognizer contest

- http://www.kaggle.com/c/digit-recognizer
- Dataset of hand-written digits
- Goal = automatically recognize digits
- Training sample = 50,000 examples
- Contest = predict 20,000 “unknown” digits

***

### The data "looks like that"

![A Three](images/No3.png)
![A Zero](images/No0.png)

***

### Real sample

- 28 x 28 pixels
- Grayscale (0 = black, to 255 = white)
- Flattened: each record = Number + 784 pixels
- CSV file
- Reduced dataset: 5,000 training, 500 validation

***

### Illustration (simplified 4x4 data)

![Encoding](images/Encoding.png)

***

### What’s a classifier?

- "Give me an unknown data point and I will predict what class it belongs to"
- In this case, classes = 0, 1, 2, … 9
- Unknown data point = scanned digit, without the class it belongs to

***

### The KNN Classifier

- KNN = K Nearest Neighbors
- Given an unknown subject to classify,
- Lookup all the known examples,
- Find the K closest examples,
- Take a majority vote,
- Predict what the majority says

***

### Illustration: 1-nearest neighbor

![1-nearest-neighbort](images/NearestNeighbor.png)

---

### Illustration: 1-nearest neighbor (2)

![1-nearest-neighbort](images/NearestNeighbor2.png)

---

### Illustration: 1-nearest neighbor (3)

![1-nearest-neighbort](images/NearestNeighbor3.png)


***

## Questions so far?

***

### Your mission

- Code a 1-nearest-neighbor classifier

- Guided script available at:
- http://www.github.com/c4fsharp/Dojo-Digits-Recognizer


***

## GuidedSample.fsx
### Task definition

    // 2. EXTRACTING COLUMNS
     
    // Break each line of the file into an array of string,
    // separating by commas, using Array.map

---
    
## GuidedSample.fsx
### A little syntax help
        
    // <F# QUICK-STARTER> 
    // Array.map quick-starter:
    // Array.map takes an array, and transforms it
    // into another array by applying a function to it.
    // Example: starting from an array of strings:
    let strings = [| "Machine"; "Learning"; "with"; "F#"; "is"; "fun" |]
    
    // We can transform it into a new array,
    // containing the length of each string:
    let lengths = Array.map (fun (s:string) -> s.Length) strings
    
    // The exact same operation above can be 
    // done using the forward pipe operator, 
    // which makes it look nicer:
    let lengths2 = strings |> Array.map (fun s -> s.Length)
    // </F# QUICK-STARTER> 
    
---
    
## GuidedSample.fsx
### Now it's your turn
 
    // The following function might help
    let csvToSplit = "1,2,3,4,5"
    let splitResult = csvToSplit.Split(',')
    
    // [ YOUR CODE GOES HERE! ]