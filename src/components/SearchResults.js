import React from "react";
import { Link } from 'react-router-dom'

export default function SearchResults(props) {

    const mappedData = props.recordData.map((record, i) => {
        return (
            <div className='record' id={i} key={i}>
                <Link to={`/records/${i}`}>
                <img className='record--image'                        
                src={record.imageUrl}
                alt='radiohead'
                />
                </Link>
                <div className='record--details'>
                    <h3 className='record--name'>{record.name}</h3>
                    <h3 className='record--artist'>{record.artist}</h3>
                </div>
                <p className='record--info'>{record.releaseYear}  • 
                {record.songCount} songs</p>
                <div className='record--buying'>
                    <p className='record--price'>£{record.price}</p>
                    <button className='search--record--button' onClick={() => props.addToCart(record, i)}>Add to Cart</button>
                </div>
            </div>
        )
    })

    return (
        <>
        <h1 className='page--header'>Search Page</h1>
        <div className='record--container'>
            {mappedData}
        </div>
        </>
    )
}