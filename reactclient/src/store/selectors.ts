import {CommonState} from "./reducers";

export const isChanged = ({changed}: CommonState) => changed;
export const hasError = ({error}: CommonState) => error !== null;