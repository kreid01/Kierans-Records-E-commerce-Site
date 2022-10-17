/* eslint-disable array-callback-return */
import React from 'react';
import { Link, Outlet } from 'react-router-dom'
import Filter from './Filter';

export default function RecordsList(props) {
            const emptyArr = Array(7).fill('')

  //  const [searchParams, setSearchParams] = useSearchParams({})
 //   const number = searchParams.get('n')
            const pageNumberData = emptyArr.map((item, i) =>  {
                return (
                    <div className='page--change--item'>
                        <button 
                        name={i+1}
                        className='page--change--button' 
                        onClick={(e) => props.changePage(e)}>{i+1}</button>
                    </div>
                )
            })
                        
            const mappedData = props.recordData.map((record, i) => {
                let id = 0
                // eslint-disable-next-line array-callback-return
                props.allRecords.map((rec, i) => {
                    if(record.name === rec.name) {
                        id = i
                    }
                })
                const newArr = [...props.recordData]
                newArr.splice(i, 1)
                if(newArr.includes(record)) {
                } else 
                return (
                    <div className='record' id={id} key={i}>
                        <Link to={`/records/${id}`}>
                        <img className='record--image'                        
                        src={record.imageUrl}
                        alt=''
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
                            <button style={props.inputThemeStyles} className='record--list--cart--add' onClick={() => props.addToCart(record, id)}>Add to Cart</button>
                        </div>
                    </div>
                )}
            )
        
    return (
        <main className='record--list--page'>
            <header style={props.themeStyles} className='record--list--header'>
                <h1 className='page--header'>Records</h1>
                <p>{props.pageNumber}</p>
                <Link to='/records/new'><button
                style={props.inputThemeStyles}
                 id='new--record--button'
                >New Record</button></Link>
            </header>
                <div style={props.themeStyles} className='record--list--container'>
                <div className='filter--conatiner'>
                    <Filter
                    resetFilters={props.resetFilters}
                    inputThemeStyles={props.inputThemeStyles} 
                    changeSortBy={props.changeSortBy}
                    genreFilter={props.genreFilter}
                    selectGenre={props.selectGenre}
                    changeSearchParams={props.changeSearchParams}
                    />
                </div>
                <div className='record--container'>
                    {mappedData}
                </div>
                <div className='page--number--container'>
                    {pageNumberData}
                </div>            
            </div>
            <Outlet />
        </main>

    )
}