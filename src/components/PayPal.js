/* eslint-disable array-callback-return */
import React from 'react';

export default function PayPal(props) {
    
    const recordData = props.recordData
    const paypal = React.useRef()

    const updateDatabase = () => props.cartWithUniqeIds.map(record => {

        const recordDataForDatabase = {
                 "_id": `${record._id}`,
                "name":`${record.name}`,
                "artist": `${record.artist}`,
                "releaseYear": record.releaseYear,
                "songCount": record.songCount,
                "imageUrl": `${record.imageUrl}`,
                "genres": 
                    [`${record.genres[0]}`, `${record.genres[1]}`, `${record.genres[2]}`]
                ,
                "quantity": record.quantity,
                "price": record.price,
                "isAvailable": false
        }
            console.log(JSON.stringify(recordDataForDatabase))
            fetch(`https://localhost:7143/records?id=${record._id}`, { 
            method: 'PUT',headers: { 'Content-Type': 'application/json' },  
            body:JSON.stringify(recordDataForDatabase)})
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
                        description: cartDescription,
                        item_list: {
                            items: [
                                {
                                name: "record"
                                }
                            ]
                        }
                        }],
                });
            },
            onApprove: async (data, action) => {
                const order = await action.order.capture()
                updateDatabase() 
                props.goToCheckout()
                props.emptyCartOnSuccessfulPayment()
                props.goToCheckout()
                console.log(order)
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