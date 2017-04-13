- title : React Native with F#
- description : Introduction to React Native with F#
- author : Steffen Forkmann
- theme : night
- transition : default

***

## React Native with F#

<br />
<br />

### Look mum no JavaScript

<br />
<br />
Steffen Forkmann

[@sforkmann](http://www.twitter.com/sforkmann)

***

---

### Modern mobile app development?

* "Native mobile apps"
* Hot loading
* Easy to refactor / Maintainability
* Correctness

---

### "Native mobile apps"

 <img src="images/meter.png" style="background: transparent; border-style: none;"  width=300 />

---

### Hot loading

<img src="images/hotloading.gif" style="background: transparent; border-style: none;"  />

---

### Hot loading

<img src="images/hotloading.gif" style="background: transparent; border-style: none;"  />

***

### React

* Facebook library for UI 
* <code>state => view</code>
* Virtual DOM to calc minimal number of DOM changes necessary

---

### Virtual DOM - Initial

<br />
<br />


 <img src="images/onchange_vdom_initial.svg" style="background: white;" />

<br />
<br />

 <small>http://teropa.info/blog/2015/03/02/change-and-its-detection-in-javascript-frameworks.html</small>

---

### Virtual DOM - Change

<br />
<br />


 <img src="images/onchange_vdom_change.svg" style="background: white;" />

<br />
<br />

 <small>http://teropa.info/blog/2015/03/02/change-and-its-detection-in-javascript-frameworks.html</small>

---

### Virtual DOM - Reuse

<br />
<br />


 <img src="images/onchange_immutable.svg" style="background: white;" />

<br />
<br />

 <small>http://teropa.info/blog/2015/03/02/change-and-its-detection-in-javascript-frameworks.html</small>


*** 

### ReactNative

 <img src="images/ReactNative.png" style="background: white;" />


 <small>http://timbuckley.github.io/react-native-presentation</small>

*** 

### Elm - Architecture

 <img src="images/Elm.png" style="background: white;" width=700 />


 <small>http://danielbachler.de/2016/02/11/berlinjs-talk-about-elm.html</small>

---

### Elm - Architecture - Benefits

* Model is single source of truth
* A pure view function
* Side effects are modelled as Tasks, handled by the runtime
* Apps are well structured. All state modifications happen in the central update

*** 

### Model - View - Update

    // MODEL

    type Model = int

    type Msg =
    | Increment
    | Decrement

    let init() : Model = 0

---

### Model - View - Update

    // VIEW (rendered with React)

    let view count dispatch =

        R.div []
            [ R.button [ OnClick (fun _ -> dispatch Decrement) ] [ R.str "-" ]
              R.div [] [ R.str (string count) ]
              R.button [ OnClick (fun _ -> dispatch Increment) ] [ R.str "+" ] ]

---

### Model - View - Update

    // UPDATE

    let update (msg:Msg) (model:Model) =
        match msg with
        | Increment -> model + 1
        | Decrement -> model - 1

---

### Model - View - Update

    // wiring things up

    Program.mkSimple init update view
    |> Program.withConsoleTrace
    |> Program.withReact "elmish-app"
    |> Program.run

---

### Model - View - Update

# Demo

***

### Sub-Components

    // MODEL

    type Model = {
        Counters : Counter.Model list
    }

    type Msg = 
    | Insert
    | Remove
    | Modify of int * Counter.Msg

    let init() : Model =
        { Counters = [] }

---

### Sub-Components

    // VIEW (rendered with React)

    let view model dispatch =
        let counterDispatch i msg = dispatch (Modify (i, msg))

        let counters =
            model.Counters
            |> List.mapi (fun i c -> Counter.view c (counterDispatch i)) 
        
        R.div [] [ 
            yield R.button [ OnClick (fun _ -> dispatch Remove) ] [ R.str "Remove" ]
            yield R.button [ OnClick (fun _ -> dispatch Insert) ] [ R.str "Add" ] 
            yield! counters ]

---

### Sub-Components

    // UPDATE

    let update (msg:Msg) (model:Model) =
        match msg with
        | Insert ->
            { Counters = Counter.init() :: model.Counters }
        | Remove ->
            { Counters = 
                match model.Counters with
                | [] -> []
                | x :: rest -> rest }
        | Modify (id, counterMsg) ->
            { Counters =
                model.Counters
                |> List.mapi (fun i counterModel -> 
                    if i = id then
                        Counter.update counterMsg counterModel
                    else
                        counterModel) }

---

### Sub-Components

# Demo

*** 

### Show me the code
