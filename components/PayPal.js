import React from 'react';

export default function PayPal(props) {
    
    const paypal = React.useRef()

    const updateDatabase = () => props.cart.map(record => {
        console.log(record._id)
        const recordData = {
                 "_id": `${record._id}`,
                "name":`${record.name}`,
                "artist": `${record.artist}`,
                "releaseYear": record.releaseYear,
                "songCount": record.songCount,
                "imageUrl": `${record.imageUrl}`,
                "genres": 
                    [`${record.genres[0]}`, `${record.genres[1]}`, `${record.genres[2]}`]
                ,
                "quantity": record.quantity - record.quantityInCart,
                "price": record.price
        }
            console.log(JSON.stringify(recordData))
            fetch(`https://localhost:7143/records?id=${record._id}`, { 
            method: 'PUT',headers: { 'Content-Type': 'application/json' },  
            body:JSON.stringify(recordData)})
     })
     updateDatabase()

    // eslint-disable-next-line no-unused-vars
    const cartDescription = props.cart.map(record => {
       return `${record.name} by ${record.artist} x ${record.quatity}`
    })

    React.useEffect(() => {
        window.paypal.Buttons({
            createOrder: (data, action) => {
                return action.order.create({
                    purchase_units: [
                        {
                            amount: {
                                value: props.totalPrice
                            },
                        }],
                });
            },
            onApprove: async (data, action) => {
                const order = await action.order.capture()
                updateDatabase()
                console.log(order)
               (props.goToCheckout)
            },
            onError : (err) => {
                console.log(err)
            }
        }).render(paypal.current)
    // eslint-disable-next-line react-hooks/exhaustive-deps
    }, [])
    return (
        <div style={props.themeStyles}className='paypal--container'>
            <div className="paypal--button" ref={paypal}></div>
        </div>
    )
}