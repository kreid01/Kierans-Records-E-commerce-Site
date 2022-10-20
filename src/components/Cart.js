import React from 'react';
import { Link } from 'react-router-dom'
import PayPal from './PayPal'

export default function Cart(props) {
    
    const cartTitleSummary = props.cart.map((record, i) => {
        return (
        <div id={i} className="cart-record--total">
            <div>{record.quantityInCart}x {record.name}</div>
            <div>£{record.totalPrice.toFixed(2)}</div>
        </div>
        )
    })

    const wishlistData = props.wishlist.map((record, i) => {

        let id = 0

        // eslint-disable-next-line array-callback-return
        props.recordData.map((rec, i) => {
            if(record.name === rec.name) {
                id = i
            }
        })

        return (
            <div className='record' id={id} key={i}>
                <Link to={`/records/${id}`}>
                <img id='featured--record'                        
                src={record.imageUrl}
                alt=''
                />
                </Link>
                <div id='new--record--details'>
                    <h3 className='new--record--name'>{record.name}</h3>
                    <h3 className='new--record--artist'>{record.artist}</h3>
                </div>
                <div className='record--info'>
                <p className='record--details'>{record.releaseYear}  • {record.songCount} songs</p>
                <div className='record--buying'>
                    <p className='record--price'>£{record.price}</p>
                    <button 
                    style={props.inputThemeStyles}
                    onClick={() => props.deleteFromWishlist(i)}>Delete</button>
                    <button 
                    style={props.inputThemeStyles}
                    onClick={() => props.addToCart(record, id)}>Add to Cart</button>
                </div>
                </div>
            </div>
            )
    })

    const cartData = props.cart.map((record, i) => {

        let id = 0

        // eslint-disable-next-line array-callback-return
        props.recordData.map((rec, i) => {
            if(record.name === rec.name) {
                id = i
            }
        })
        return (
            <div className='cart--record' id={id} key={i}>
                <Link to={`/records/${id}`}>
                    <img className='cart--record--img'                        
                    src={record.imageUrl}
                    alt={record.name}
                    />
                </Link>
                <div className='cart--item--info'>
                    <div className='cart--item--details'>
                        <h3 className='cart--record--name'>{record.name}</h3>
                        <h3 className='cart--record--artist'>{record.artist}</h3>
                    </div>
                    <div className='cart--record--buying'>
                        <p className='cart--record--price'>£{record.totalPrice.toFixed(2)}</p>
                        <button 
                        style={props.inputThemeStyles}onClick={() => props.decrement(i, id)}>-</button>
                        <p className='cart--quantity'>{record.quantityInCart}</p>
                        <button style={props.inputThemeStyles} onClick={() => props.increment(i, id)}>+</button>
                        <button style={props.inputThemeStyles} onClick={() => props.deleteFromCart(i, id)}>Delete</button>
                        <button style={props.inputThemeStyles} onClick={() => props.addToWishlist(record, id)}
                         className='wishlist--add'>+ Wishlist</button> 
                    </div>
                </div>
            </div>
        )
    })
    return (
    <div className='cart--page' style={props.themeStyles} >
        { (props.checkout) ? (
        <PayPal 
        emptyCartOnSuccessfulPayment={props.emptyCartOnSuccessfulPayment}
        cartDataFromAPI={props.cartDataFromAPI}
        recordData={props.recordData}
        goToCheckout={props.goToCheckout}
        cart={props.cart}
        totalPrice={props.totalPrice}
        checkout={props.checkout}/> ) :
            <div>
               <div className='cart--header' style={props.themeStyles}>
                 <h1 style={props.themeStyles} className='page--header'>Cart</h1>
               </div>  
            <div  style={props.themeStyles}  className='cart--page--container'>
                <div className='cart--items--container'>
                    {cartData}
                </div>
            <div className='cart--summary'>
                     {cartTitleSummary}
                    <div className='cart--total--price'>
                        Total: {props.totalPrice.toFixed(2)}    
                       {props.cart.length > 0 && <button 
                        style={props.inputThemeStyles}
                        onClick={props.goToCheckout}
                       className='checkout--button'>Checkout</button>}
                    </div>
                </div>
            </div>
                <div className='wishlist--container'>
                <h1 className='page--header'>Wishlist</h1>
                <div className='featured--record--container'>
                    {wishlistData}
                </div>
            </div>
        </div>
        }
    </div>
    )
}