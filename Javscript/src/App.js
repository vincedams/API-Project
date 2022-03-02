import { Component } from "react/cjs/react.production.min";
import axios from "axios";

class App extends Component {
  constructor(props){
    super(props)

    this.state = {
      Persons: [],
      Age:0,
      Name:'',
      Filler: []
    }
  }
  getPeople(){
    this.setState({
      loading: true
    })
    axios('https://localhost:44345/api/Persons')
    .then(response => this.setState({
      Persons: response.data,
      loading: false
      })
    );
  }
  LogPeople(){
    fetch('https://localhost:44345/api/Persons')
    .then(res => res.json())
    .then(final => this.setState({Filler: final}))
  }
  componentDidMount(){
    this.getPeople()
  }
  PrintData = () => {
    console.log(this.state.Age)
  }
  ChangeAge = () =>{
    const news = document.getElementById("year");
    this.setState({Age: news.value});
  }
  ChangeName = () =>{
    const ye = document.getElementById("nombre");
    this.setState({Name: ye.value})
  }
  PosttoApi = () =>{
    fetch('https://localhost:44345/api/Persons', {
      method: 'POST',
      headers: {
        'Content-Type': 'application/json'
      },
      body: JSON.stringify({
        Name: this.state.Name,
        Age: this.state.Age
      })
    })
  }
  render(){
    //const {//Persons,
          //Age}=this.state
    return (
      <div className="App">
        <label>Name - Age</label>
        {!this.state.loading ?
        this.state.Persons.map(Person => <div>{Person.Name}              {Person.Age}</div>): 'loading'}
        <div>
          <form>
            <label id="Age" for="Age">Age</label>
            <input type="text" id="year" onChange={this.ChangeAge}></input>
            <label for="Name">Name</label>
            <input type="text" id="nombre" onChange={this.ChangeName}></input>
            {/* <button id="btn" onClick={this.PrintData}>Click to Post</button> */}
            <button id="log btn" onClick={this.LogPeople}>Log People</button>
            <button id="POST" onClick={this.PosttoApi}>POST</button>
          </form>
          {/* <input type="text" id="head">{this.Filler}</input> */}
        </div>
      </div>
    );
  }
}

export default App;