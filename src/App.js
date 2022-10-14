
import './styles.css';
import { Route, Routes, NavLink} from 'react-router-dom'
import  Home  from './components/Home'
import  RecordsList  from './components/RecordsList'
import Record from './components/Record'
import NewRecord from './components/NewRecord'
import NotFoundPage from './components/NotFoundPage'
import Blog from './components/Blog'
import recordImage from './images/record.png'
import Footer from './components/Footer'
import cartImage from './images/cart-shopping-solid.svg'
import Cart from './components/Cart'
import React from 'react'
import Collection from './components/Collection';
import SearchResults from './components/SearchResults.js';
import { useTheme, useThemeUpdate} from './components/ThemeContext'

function App() {

function postData() {
fetch('https://localhost:7143/records', {
smethod: 'POST', 
headers: { 'Content-Type': 'application/json' },  
body:JSON.stringify(newRecord)})
.then(res => 201)
.then(console.log('posted successfully'))
.then(setNewRecord({
  name:'',
  artist:'',
  releaseYear: 0,
  imageUrl:'',
  duration: '',
  songCount: 0,
  price: 0,
  genres: [],
}))
}

const darkTheme = useTheme()
const toggleTheme = useThemeUpdate()
const themeStyles = {
  backgroundColor: darkTheme ? '#333': '#CCC',
  color: darkTheme ? '#CCC' : '#333'
}
const inputThemeStyles = {
  backgroundColor: darkTheme ? '#CCC': '#333',
  color: darkTheme ? '#333': '#CCC',
}
const darkThemeToggle = (darkTheme) ? 'toggle--dark--mode--dark' : 'toggle-dark--mode--light'
const [sortBy, setSortBy] = React.useState('')
const [searchParams, setSearchParams] = React.useState('')
const [genreFilter, setGenreFilter] = React.useState('')
const [wishlist, setWishlist] = React.useState(() => {
  const saved = localStorage.getItem("userWishlist");
  const initialValue = JSON.parse(saved);
  return initialValue || "";
})
const [pageNumber, setPageNumber] = React.useState('1')
const [checkout, setCheckout] = React.useState(false)
const [recordData, setRecordData] = React.useState([])
const [recordDataForPaging, setRecordDataForPaging] = React.useState([])
const [cart, setCart] = React.useState([])
const [collection, setCollection] = React.useState(() => {
  const saved = localStorage.getItem("userWishlist");
  const initialValue = JSON.parse(saved);
  return initialValue || "";
})
const [newRecord, setNewRecord] = React.useState({
  name:'',
  artist:'',
  releaseYear: 0,
  imageUrl:'',
  duration: '',
  songCount: 0,
  price: 0,
  genres: [],
})
const totalPrice = cart.reduce((acc, record) => {
    return acc += (record.price * record.quantityInCart.toFixed(2))
}, 0)
const totalQuantity  = cart.reduce((acc, record) => {
  return acc + record.quantityInCart
}, 0)

React.useEffect(() => {
  localStorage.setItem('userWishlist', JSON.stringify(wishlist));
}, [wishlist]);

React.useEffect(() => {
  localStorage.setItem('userWishlist', JSON.stringify(collection));
}, [collection]);

React.useEffect(() => {
  fetch('https://localhost:7143/records/all')
  .then(res => res.json())
  .then(data => setRecordData(data))}, [])

  React.useEffect(() => {
    fetch(`https://localhost:7143/records?PageNumber=${pageNumber}&PageSize=20`)
    .then(res => res.json())
    .then(data => setRecordDataForPaging(data))}, [pageNumber])
  

function goToCheckout() {
  setCheckout(prevState => !prevState)
}
  
function handleChange(e) {
    setNewRecord(prevObj => ({
      ...prevObj,
      [e.target.name]: (e.target.name === 'genres') ?
       [...prevObj.genres, (e.target.value)]: 
       e.target.value
      }))
    }

 function addToCart(record, id) {
  if(record.quantity >= 1 && recordData[id].quantity >= 1) {
  wishlist.map((rec , i) => {
    if(rec.name === record.name) {
      const newArr = [...wishlist]
      newArr.splice(i, 1)
      setWishlist(newArr)
    }
  })
  if(cart.some(c => c._id === record._id)) {
    const newArr = [...cart]
    let index = 0 
    cart.map((rec, i) => {
      if (rec.name === record.name) {
        index = i
      }
    })
    newArr[index].quantityInCart = newArr[index].quantityInCart + 1
    newArr[index].totalPrice = newArr[index].price * newArr[index].quantityInCart
    setCart(newArr)
  } else {
      setCart(prevArr => [...prevArr, {
      ...recordData[id],
      quantityInCart: 1,
      totalPrice: recordData[id].price}])
    }

  const newArr = [...recordData]
  newArr[id].quantity = newArr[id].quantity - 1
  setRecordData(newArr)
  }
}

  function deleteFromCart(id, i) {
    const newRecordArr = [...recordData]
    newRecordArr[i].quantity = newRecordArr[i].quantity + cart[id].quantityInCart
    setRecordData(newRecordArr)
    const newArr = [...cart]
    newArr.splice(id, 1)
    setCart(newArr)


}

function increment(i, id) {
  if(cart[i].quantityInCart < cart[i].quantity) {
  const newArr = [...cart]
  newArr[i].quantityInCart = newArr[i].quantityInCart + 1
  newArr[i].totalPrice = newArr[i].price * newArr[i].quantityInCart
  setCart(newArr)

  const newRecordArr = [...recordData]
  newRecordArr[id].quantity = newRecordArr[id].quantity - 1
  setRecordData(newRecordArr)  
  }
}
function decrement(i, id) {
  const newRecordArr = [...recordData]
  newRecordArr[id].quantity = newRecordArr[id].quantity + 1
  setRecordData(newRecordArr) 
  const newArr = [...cart]
  if(newArr[i].quantityInCart -1 === 0) {
  newArr.splice(i, 1)
  setCart(newArr)
  } else {
  newArr[i].quantityInCart = newArr[i].quantityInCart - 1
  newArr[i].totalPrice = newArr[i].price * newArr[i].quantityInCart
  setCart(newArr)
  }
}


function addToWishlist(record, id) {

  // eslint-disable-next-line array-callback-return
  cart.map((rec , i) => {
    if(rec.name === record.name) {
      const newArr = [...wishlist]
      newArr.splice(i, 1)
      setCart(newArr)
    }
  })
  let isRecordPresent = false
  console.log(wishlist)
  // eslint-disable-next-line array-callback-return
  wishlist.map(rec => {
    if(rec.name === record.name) {
      isRecordPresent = true
    }
  })
  if(isRecordPresent) {
  } else {
  setWishlist(prevArr => [...prevArr, recordData[id]])
  }
}

function deleteFromWishlist(id) {
  if(wishlist.length > 0){
    const newArr = [...wishlist]
    newArr.splice(id, 1)
    setWishlist(newArr)
  } else {
    setWishlist(wishlist.pop(id))
    setWishlist([])
  }
}
function addToCollection(record, id) {
  let isRecordPresent = false
  // eslint-disable-next-line array-callback-return
  collection.map(rec => {
    if(rec.name === record.name) {
      isRecordPresent = true
    }
  })
  if(isRecordPresent) {
  } else {
  setCollection(prevArr => [...prevArr, recordData[id]])
  }
}

function deleteFromCollection(id) {
  if(collection.length > 0){
    const newArr = [...collection]
    newArr.splice(id, 1)
    setCollection(newArr)
  } else {
    setCollection(collection.pop(id))
    setCollection([])
  }
}

async function selectGenre(e) {
  if(e.target.value === '0') {
    console.log(e.target.value)
  setGenreFilter('')
  } else {
  setGenreFilter(e.target.value)
  }
}

function changeSearchParams(e) {
  setSearchParams(e.target.value)
}

function changePage(e) {
  setPageNumber(e.target.name)
  window.scrollTo(0, 0)
}

function changeSortBy(e) {
    setSortBy(e.target.value)
}

function resetFilters() {
  setSearchParams('')
  setGenreFilter('')
  setSortBy('')
}
  return (
  <>
  <header className='nav--bar'>
    <nav style={themeStyles}>
    <NavLink end to='/' className='home--link'>
      <div className='logo--container'>
        <h1 style={themeStyles}>KR Records</h1>
        <img className='image--record' src={recordImage} alt='record'></img>
      </div>
    </NavLink>
      <ul>
        <li>
          <NavLink
            className='nav--item'
            style = {({ isActive }) => {
              return isActive ? { background : 'rgb(56, 56, 56)'} : {}
            }} end to='/records'>
            Records
        </NavLink>
        </li>
      <li>
          <NavLink
          className='nav--item'
            style = {({ isActive }) => {
              return isActive ? { background : 'rgb(56, 56, 56)'} : {}
            }} end to='/blog'>Blog
        </NavLink>
        </li>
        <li>
          <NavLink
          className='nav--item'
            style = {({ isActive }) => {
              return isActive ? { background : 'rgb(56, 56, 56)'} : {}
            }} end to='/collection'>Collection
        </NavLink>
        </li>
        <li className='nav--buttons'>
          <NavLink
          onClick={(checkout)? goToCheckout : ''}
          className='cart--container'
          end to='/cart'><img className='cart' src={cartImage} alt='cart'/>
          <div className='counter'>{totalQuantity}</div>
        </NavLink>
        <button onClick={toggleTheme} style={inputThemeStyles} className={darkThemeToggle}></button>
        </li>
      </ul>                                              
    </nav>
  </header>
  <div className='page--container' style={themeStyles}>
  <div className='page'>
  <Routes>
    <Route exact path='/' element={<Home
    themeStyles={themeStyles}
    inputThemeStyles={inputThemeStyles}
    addToCart={addToCart}
    recordData={recordData} />}></Route>
    <Route path='/records'>
      <Route index element={<RecordsList
      resetFilters={resetFilters}
      themeStyles={themeStyles}
      changePage={changePage}
      inputThemeStyles={inputThemeStyles}
      changeSortBy={changeSortBy}
      changeSearchParams={changeSearchParams}
      searchData={searchParams} 
      genreFilter={genreFilter}
      selectGenre={selectGenre}
      allRecords={recordData}
      pageNumber={pageNumber}
      recordData={(genreFilter.length > 5 || genreFilter !== '' ? recordData.filter(
      // eslint-disable-next-line array-callback-return
      record => record.genres.includes(genreFilter)): (searchParams.length < 2) ? recordDataForPaging : recordData).filter(record => {
        if(record.name.includes(searchParams) || record.artist.includes(searchParams)) {
          return record
        }
      }).sort(function(a, b) {
        if(sortBy === 'Price >') {
          return parseFloat(a.price) - parseFloat(b.price)
        } else if(sortBy === 'Price <') {
          return parseFloat(b.price) - parseFloat(a.price)
        } else if(sortBy === 'ReleaseYear >') {
          return parseFloat(a.releaseYear) - parseFloat(b.releaseYear)
        } else if(sortBy === 'ReleaseYear <') {
          return parseFloat(b.releaseYear) - parseFloat(a.releaseYear)
        }else if(sortBy === 'Artist >') {
          if (a.artist < b.artist) {
            return -1;
          } if (a.artist > b.artist) {
            return 1;
          }
          return -0
        } else if(sortBy === 'Artist <') {
          if (a.artist > b.artist) {
            return -1;
          } if (a.artist <  b.artist) {
            return 1;
          }
          return -0
        } else if(sortBy === 'Record Name >') {
          if (a.name < b.name) {
            return -1;
          } if (a.name > b.name) {
            return 1;
          }
          return -0
        } else if(sortBy === 'Record Name <') {
          if (a.name > b.name) {
            return -1;
          } if (a.name <  b.name) {
            return 1;
          }
          return -0
        }   return recordDataForPaging
    })}
      addToCart={addToCart}/>}></Route>
      <Route path=':id' element={<Record 
      themeStyles={themeStyles}
      inputThemeStyles={inputThemeStyles}
      changePage={changePage}
      addToCollection={addToCollection}
      addToWishlist={addToWishlist}
      recordData={recordData}
      addToCart={addToCart}/>}></Route>
      <Route path='new' element={<NewRecord 
      inputThemeStyles={inputThemeStyles}
      themeStyles={themeStyles}
      handleChange={handleChange}
      newRecord={newRecord}
      postData={postData}
      />}></Route>
      </Route>
      <Route path='/blog' element={<Blog 
      recordData={recordData}
      themeStyles={themeStyles}/>}></Route>
      <Route path='/searchresults' element={<SearchResults
      themeStyles={themeStyles}
      inputThemeStyles={inputThemeStyles}
      // eslint-disable-next-line array-callback-return
      recordData={recordData.filter(record => {
        if(record.name.includes(searchParams) || record.artist.includes(searchParams)) {
          return record
        }
      })} />}></Route>
      <Route path='/collection' element={<Collection
      inputThemeStyles={inputThemeStyles}
      themeStyles={themeStyles}
      deleteFromCollection={deleteFromCollection}
      recordData={recordData}
      addToCart={addToCart}
      collection={collection}
      />}></Route>
      <Route path='/cart' 
      element={<Cart
      themeStyles={themeStyles}
      inputThemeStyles={inputThemeStyles}
      checkout={checkout}
      goToCheckout={goToCheckout}
      deleteFromWishlist={deleteFromWishlist}
      addToWishlist={addToWishlist}
      increment={increment}
      decrement={decrement}
      addToCart={addToCart}
      recordData={recordData} 
      wishlist={wishlist}
      deleteFromCart={deleteFromCart} 
      totalPrice={totalPrice}
      cart={cart}/>}>
      </Route>
      <Route path='*' element={<NotFoundPage />}></Route>
  </Routes>
  </div>
  </div>
  <Footer 
  inputThemeStyles={inputThemeStyles}
  themeStyles={themeStyles} />
  </>
  )
}

export default App;
