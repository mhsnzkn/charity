import React from 'react';
import { useState } from 'react/cjs/react.development';
import SubmitSuccess from './SubmitSuccess';
import VolunteerForm from '../components/VolunteerForm';

export default function VolunteerFormPage() {
    const [isSubmitted, setIsSubmitted] = useState(false);
    
    return (
        <>
            {isSubmitted ?
            <SubmitSuccess/>
        :
        <VolunteerForm/>}
        </>
    );
}
