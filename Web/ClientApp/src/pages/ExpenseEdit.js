import { getUserRole } from '../helpers/helpers';
import { Admin } from '../constants/userRoles';
import ExpenseEditAdminForm from '../components/ExpenseEditAdminForm';
import ExpenseEditForm from '../components/ExpenseEditForm';

export default function ExpenseEdit() {

    return (
        <>
            {getUserRole() === Admin ?
                <ExpenseEditAdminForm />
                :
                <ExpenseEditForm />}
        </>
    );
}
