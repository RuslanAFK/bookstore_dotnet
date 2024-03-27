import React from "react";
import MainLabel from "../../shared/components/MainLabel";


const Info = () => {

    return (
        <div>
            <MainLabel text={"About us"}/>
            <div className="container mt-4">
                <div className="row">
                    <div className="col-lg-12">
                        <p className="text-justify">This innovative application is designed to streamline the management
                            and accessibility of digital libraries, catering to a diverse audience ranging from library
                            administrators to general readers. At its core, the app features a robust three-tier user
                            system, ensuring a customized experience for each user category.</p>

                        <p className="text-justify">Superusers wield the utmost control within the application, holding
                            the unique capabilities to onboard new administrators and manage the user base by executing
                            deletions where necessary. This level of control is essential for maintaining the integrity
                            and security of the application, ensuring that only authorized personnel have access to
                            critical functionalities.</p>

                        <p className="text-justify">Administrators, appointed by superusers, are entrusted with the
                            day-to-day management of the library's content. Their responsibilities encompass a wide
                            range of tasks, including the addition, removal, and editing of book listings, ensuring the
                            library's catalog remains both current and comprehensive. Furthermore, administrators play a
                            crucial role in managing the digital assets of the library, including the handling of PDF
                            files for each book, making sure they are easily accessible and up to date.</p>

                        <p className="text-justify">At the user level, the app offers a streamlined and intuitive
                            interface for regular users to explore the library's offerings. Users can effortlessly
                            browse through the vast collection of books, gaining insights into book details and
                            availabilities. The app not only facilitates easy viewing but also enables users to download
                            books for offline reading, ensuring that knowledge and entertainment are just a click away,
                            regardless of internet connectivity.</p>

                        <p className="text-justify">This application stands out by balancing administrative control with
                            user freedom, fostering a dynamic digital library environment that is both organized and
                            user-friendly. Whether it's a superuser managing the ecosystem, an administrator curating
                            the library, or a regular user exploring the world of books, the app delivers a seamless and
                            engaging experience for all.</p>
                    </div>
                </div>
            </div>
        </div>
    )
}

export default Info;
