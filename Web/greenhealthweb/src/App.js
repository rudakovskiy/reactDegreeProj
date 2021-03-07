import React from 'react';
import './App.css';
import Header from './components/Header/Header';
import Main from './pages/Main';
import Footer from "./components/Footer/Footer"

class App extends React.Component {
  render() {
    return <div className="master-container">
      <Header />  
      <Main />
      <Footer />
    </div>
  }
  componentDidMount() {
    document.title = "GreenHealth";
  }

}

export default App;
