import React, { useState } from 'react';
import SubmitSuccess from './SubmitSuccess';
import VolunteerDocumentForm from '../components/VolunteerDocumentForm';

export default function VolunteerDocumentFormPage() {
    const [isSubmitted, setIsSubmitted] = useState(false);

    return (
        <>
            {isSubmitted ?
                <SubmitSuccess />
                :
                <VolunteerDocumentForm submit={() => setIsSubmitted(true)} />}
        </>
    );
}
