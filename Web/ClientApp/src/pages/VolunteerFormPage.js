import React, { useState } from 'react';
import SubmitSuccess from './SubmitSuccess';
import VolunteerForm from '../components/VolunteerForm';

export default function VolunteerFormPage() {
    const [isSubmitted, setIsSubmitted] = useState(false);

    return (
        <>
            {isSubmitted ?
                <SubmitSuccess />
                :
                <VolunteerForm submit={() => setIsSubmitted(true)} />}
        </>
    );
}
