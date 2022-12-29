import React, { useEffect, useState } from "react";
import { Container, Row, Col, Form } from "react-bootstrap";
import useToggle from "hooks/useToggle";
import PricingCard from "./ProductCard";
import { essential, standard, premium } from "./pricingPlansData";
import ProductFeatures from "./productFeatures";
import PropTypes from "prop-types";
import userService from "../../services/userService";
import * as subscriptionService from "../../services/stripeSubscriptionService";
import debug from "sabio-debug";

const Products = ({ currentUser }) => {
  const _logger = debug.extend("Products");
  const [pricing, togglePricing] = useToggle(true);
  const [customer, setCustomer] = useState({
    firstName: "",
    lastName: "",
  });
  const [invoicePeriod, setInvoicePeriod] = useState({
    start: "",
    end: "",
    term: 0,
  });

  useEffect(() => {
    userService
      .getUserById(currentUser.id)
      .then(onGetUserByIdSuccess)
      .catch(onGetUserByIdError);
    subscriptionService
      .GetInvoicePeriods(currentUser.id)
      .then(onGetInvoicePeriodSuccess)
      .catch(onGetInvoicePeriodError);
  }, []);

  const onGetInvoicePeriodSuccess = (response) => {
    _logger("invoice periods", response);
    const invoiceStartRaw = new Date(response.data.item.invoiceStart);
    const invoiceEndRaw = new Date(response.data.item.invoiceEnd);
    const invoiceStart = invoiceStartRaw
      .toDateString()
      .substring(invoiceStartRaw.toDateString().indexOf(" ") + 1);

    const invoiceEnd = invoiceEndRaw
      .toDateString()
      .substring(invoiceEndRaw.toDateString().indexOf(" ") + 1);

    const term =
      (invoiceEndRaw.getTime() - invoiceStartRaw.getTime()) /
      (1000 * 3600 * 24);

    setInvoicePeriod((prevState) => {
      const pd = { ...prevState };
      pd.start = invoiceStart;
      pd.end = invoiceEnd;
      pd.term = term;
      return pd;
    });
  };

  const onGetInvoicePeriodError = (error) => {
    _logger("invoice periods error", error);
  };

  const onGetUserByIdSuccess = (response) => {
    const userObj = response.data.item;
    _logger(userObj);
    setCustomer((prevState) => {
      const pd = { ...prevState };
      pd.firstName = userObj.firstName;
      pd.lastName = userObj.lastName;
      return pd;
    });
  };

  const onGetUserByIdError = (error) => {
    _logger(error);
  };

  const mapFeatures = (aProduct) => {
    return (
      <Col lg={4} md={6} sm={12} className="mb-3" key={aProduct.id}>
        <img
          className="mx-auto d-block"
          src={aProduct.image}
          alt=""
          width={50}
          height={50}
        />
        <h4 className="text-center text-primary">{aProduct.title}</h4>
        <p className="text-center ">{aProduct.description}</p>
      </Col>
    );
  };

  return (
    <>
      <div className="py-lg-13 py-8 bg-primary">
        <Container>
          {/* Page Header */}
          <Row className="align-items-center">
            <Col xl={{ span: 8, offset: 2 }} lg={12} md={12} sm={12}>
              <div className="text-center mb-6 px-md-8">
                <h1 className="text-white display-3 fw-bold">
                  Choose the plan that&apos;s right for you!
                </h1>
                <p className="text-white lead mb-4">
                  The only digital platform you need for your U.S. travel, visa,
                  or immigration journey.
                </p>
                {/* Switch Toggle */}
                <div
                  id="pricing-switch"
                  className="d-flex justify-content-center align-items-center"
                >
                  <span className="text-white me-2"> Monthly</span>
                  <Form>
                    <Form.Check
                      name="radios"
                      type="checkbox"
                      className="form-switch form-switch-price"
                      id="pricingSwitch"
                      checked={pricing}
                      onChange={togglePricing}
                    />
                  </Form>
                  <span className="text-white me-2">Yearly</span>
                </div>
                <div className="mt-4">
                  {invoicePeriod?.term === 0 ? (
                    ""
                  ) : invoicePeriod?.term > 31 ? (
                    <div>
                      <h4 className="text-white me-2">{`Next yearly plan will be on ${invoicePeriod.end}`}</h4>
                    </div>
                  ) : (
                    <div>
                      <h4 className="text-white me-2">{`Next monthly plan payment will be on ${invoicePeriod.end}`}</h4>
                    </div>
                  )}
                </div>
              </div>
            </Col>
          </Row>
        </Container>
      </div>
      {/* Content */}
      <div className="mt-n8 pb-8">
        <Container>
          <Row>
            <Col lg={4} md={12} sm={12}>
              <PricingCard
                currentUser={currentUser}
                customer={customer}
                content={essential}
                isPricingMode={pricing}
              ></PricingCard>
            </Col>
            <Col lg={4} md={12} sm={12}>
              <PricingCard
                currentUser={currentUser}
                customer={customer}
                content={standard}
                isPricingMode={pricing}
              ></PricingCard>
            </Col>
            <Col lg={4} md={12} sm={12}>
              <PricingCard
                currentUser={currentUser}
                customer={customer}
                content={premium}
                isPricingMode={pricing}
              ></PricingCard>
            </Col>
          </Row>
        </Container>
      </div>
      {/* Product Features */}
      <div className="py-lg-10 py-5">
        <Container>
          <Row>
            {/* Row */}
            <Col md={12} sm={12}>
              <div className="mb-8 text-center">
                <h2 className="h1">Product Features</h2>
              </div>
            </Col>
          </Row>
          {/* Row */}
          <Row>{ProductFeatures.map(mapFeatures)}</Row>
        </Container>
      </div>
    </>
  );
};

Products.propTypes = {
  currentUser: PropTypes.shape({
    id: PropTypes.number,
    email: PropTypes.string,
    isLoggedIn: PropTypes.bool,
  }).isRequired,
};

export default Products;
