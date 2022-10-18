import React from "react"
import { redirect } from "react-router-dom";
import { useAuth0 } from '@auth0/auth0-react'

export default function ProfilePage() {
    const { user, isAuthenticated, getAccessTokenSilently, Red} = useAuth0();

    if (!isAuthenticated) {redirect('/')} else {
    
        return (
    <h1>{user.name}</h1>
    )
}}